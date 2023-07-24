using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Net.Core.Communication.DynamicApi.Client
{

    public class DynamicApiClientProxy : DispatchProxy, IDisposable
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        private ISchemaApi _schema;
        private Uri _BaseAddress;
        private string _route;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        internal void Attach(ISchemaApi schema)
        {
            _schema = schema;
            _route = _schema.RouteProvider.GetTemplate(_schema);
            _BaseAddress = ((IDynamicApiClientFeature)_schema.Feature).BaseAddressProvider.BaseAddress;
            Address = new Uri(_BaseAddress, _route); 
        }

        /// <inheritdoc />
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {

            var matchAction = _schema[targetMethod];

            var uri = _BaseAddress;
            var request = new ExpandoObject() as IDictionary<string, Object>;
            int idx = 0;
            foreach (var param in targetMethod.GetParameters())
            {
                request.Add(param.Name, args[idx]);
                idx++;
            }

            return new HttpCaller().Post(uri, targetMethod.ReturnType, _route, matchAction.ActionName, request);

        }


        public Uri Address { get; private set; } 
        public void Dispose()
        {

        }
    }



    internal class HttpCaller
    {
        private readonly static MethodInfo FromResult;
        static HttpCaller()
        {
            FromResult = typeof(Task<>).GetMethod(nameof(Task.FromResult), BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        }

        public object Post(Uri baseAddress, Type resultType, string controller, string path, object request)
        {

            var requestPath = $"{controller}/{path}";
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;

                // Setting content type.  
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                //client.MaxResponseContentBufferSize=??
                //client.DefaultVersionPolicy
                //client.DefaultRequestHeaders


                var res = client.PostAsJsonAsync(requestPath, request, new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
                    PropertyNameCaseInsensitive = true,

                }).GetAwaiter().GetResult();


                if (res.IsSuccessStatusCode)
                {
                    return ReadResponse(res, resultType);
                }
                else
                {
                    StringBuilder message = new StringBuilder();

                    Uri full = new Uri(baseAddress, requestPath);
                    if (!string.IsNullOrEmpty(res.ReasonPhrase))
                        message.AppendLine($"Reason: {full} {res.ReasonPhrase}");
                    string msg = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (!string.IsNullOrEmpty(msg))
                        message.AppendLine($"Details: {msg}");

                    throw new Exception(message.ToString());
                }
            }
        }


        private static object ReadResponse(HttpResponseMessage message, Type resultType)
        {
            if (resultType == typeof(void))
                return null;

            if (resultType == typeof(Task))
                return Task.CompletedTask;

            if (resultType == typeof(IAsyncResult))
            {
                try
                {
                    var jo = message.Content.ReadFromJsonAsync<JsonObject>().GetAwaiter().GetResult();
                    JsonNode jn = null;
                    if (jo != null && jo.TryGetPropertyValue("result", out jn))
                        return Task.FromResult(jn);

                    return Task.FromResult(jo);
                }
                catch (Exception ex)
                {
                    return Task.FromResult(ex);
                }
            }


            if (resultType.BaseType == typeof(Task) && resultType.IsGenericType && resultType.GenericTypeArguments.Count() == 1)
            {
                var data = message.Content.ReadFromJsonAsync(resultType.GenericTypeArguments[0]).GetAwaiter().GetResult();
                return FromResult.MakeGenericMethod(resultType.GenericTypeArguments[0]).Invoke(null, new[] { data });
            }


            return message.Content.ReadFromJsonAsync(resultType).GetAwaiter().GetResult();
        }


    }
}
