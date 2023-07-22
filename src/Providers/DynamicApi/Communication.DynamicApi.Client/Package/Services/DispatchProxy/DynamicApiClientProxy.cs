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

namespace Communication.DynamicApi.Client
{

    internal class DynamicApiClientProxy : DispatchProxy, IDisposable
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        private ISchemaApi _schema;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        internal void Attach(ISchemaApi schema)
        {
            _schema = schema;
        }

        /// <inheritdoc />
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {

            var matchAction = _schema[targetMethod];
            var route = _schema.RouteProvider.GetTemplate(_schema);
            var uri = ((IDynamicApiClientFeature)_schema.Feature).BaseAdressProvider.BaseAbdress;
            var request = new ExpandoObject() as IDictionary<string, Object>;
            int idx = 0;
            foreach (var param in targetMethod.GetParameters())
            {
                request.Add(param.Name, args[idx]);
                idx++;
            }

            return new HttpCaller().PostAsync(uri, targetMethod.ReturnType, route, matchAction.ActionName, request).GetAwaiter().GetResult();

        }


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

        public async Task<object> PostAsync(Uri baseAdress, Type resultType, string controller, string path, object request)
        {

            var requestPath = $"{baseAdress.LocalPath}{controller}/{path}".Replace("//", "/");
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAdress;

                // Setting content type.  
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                //client.MaxResponseContentBufferSize=??
                //client.DefaultVersionPolicy
                //client.DefaultRequestHeaders


                var res = await client.PostAsJsonAsync(requestPath, request, new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                });


                if (res.IsSuccessStatusCode)
                {
                    try
                    {
                        return ReadResponse(res, resultType);
                    }
                    catch (Exception ex)
                    {
                        throw await ThrowException(baseAdress, requestPath, res, ex);
                    }

                }
                else
                {
                    throw await ThrowException(baseAdress, requestPath, res);
                }
            }
        }

        private static async Task<Exception> ThrowException(Uri baseAdress, string requestPath, HttpResponseMessage res, Exception inner = null)
        {
            StringBuilder message = new StringBuilder();

            Uri full = new Uri(baseAdress, requestPath);
            if (!string.IsNullOrEmpty(res.ReasonPhrase))
                message.AppendLine($"Reason: {full} {res.ReasonPhrase}");
            string msg = await res.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(msg))
                message.AppendLine($"Details: {msg}");

            throw new Exception(message.ToString(), inner);
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
