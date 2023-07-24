using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Net.Core.Communication.DynamicApi.Hosting
{

    /// <summary>
    /// A factory to create dynamic classes, based on <see href="http://stackoverflow.com/questions/29413942/c-sharp-anonymous-object-with-properties-from-dictionary" />.
    /// </summary>
    internal static class DynamicClassFactory
    {
        private static readonly ConcurrentDictionary<string, Type> GeneratedTypes = new();
        private static readonly ModuleBuilder ModuleBuilder;

        // Some objects we cache
        //private static readonly CustomAttributeBuilder CompilerGeneratedAttributeBuilder = new(typeof(CompilerGeneratedAttribute).GetConstructor(Type.EmptyTypes), new object[0]);
        //private static readonly CustomAttributeBuilder DebuggerBrowsableAttributeBuilder = new(typeof(DebuggerBrowsableAttribute).GetConstructor(new[] { typeof(DebuggerBrowsableState) }), new object[] { DebuggerBrowsableState.Never });
        //private static readonly CustomAttributeBuilder DebuggerHiddenAttributeBuilder = new(typeof(DebuggerHiddenAttribute).GetConstructor(Type.EmptyTypes), new object[0]);

        private static readonly ConstructorInfo ObjectCtor = typeof(object).GetConstructor(Type.EmptyTypes)!;

        private static readonly MethodInfo ObjectToString = typeof(object).GetMethod("ToString", BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null)!;


        private static readonly ConstructorInfo StringBuilderCtor = typeof(StringBuilder).GetConstructor(Type.EmptyTypes);

        private static readonly MethodInfo StringBuilderAppendString = typeof(StringBuilder).GetMethod("Append", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(string) }, null)!;
        private static readonly MethodInfo StringBuilderAppendObject = typeof(StringBuilder).GetMethod("Append", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(object) }, null)!;




        static DynamicClassFactory()
        {
            var assemblyName = new AssemblyName($"{typeof(DynamicClassFactory).Assembly.GetName().Name}.{Guid.NewGuid()}");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);

            ModuleBuilder = assemblyBuilder.DefineDynamicModule(Guid.NewGuid().ToString());
        }


        public static Type CreateType(IReadOnlyDictionary<string, Type> properties, Type serviceApiType, string typeName)
        {

            List<Type> types = properties.Values.ToList();

            foreach (var item in types.ToList())
            {
                if (item.IsByRef)
                {
                    var idx = types.IndexOf(item);
                    types.Remove(item);
                    types.Insert(idx, item.GetElementType());    
                }
            }

            string[] names = properties.Keys.ToArray();


            Type type;
            string ns = serviceApiType.FullName;

            string name = $"{ns}.{typeName}Base";

            TypeBuilder tb = ModuleBuilder.DefineType(name, TypeAttributes.AnsiClass | TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoLayout | TypeAttributes.BeforeFieldInit, typeof(DynamicClass));
            //tb.SetCustomAttribute(CompilerGeneratedAttributeBuilder);

            GenericTypeParameterBuilder[] generics;

            if (names.Length != 0)
            {
                string[] genericNames = names.Select(genericName => $"<{genericName}>j__TPar").ToArray();
                generics = tb.DefineGenericParameters(genericNames);
                //foreach (GenericTypeParameterBuilder b in generics)
                //    b.SetCustomAttribute(CompilerGeneratedAttributeBuilder);
            }
            else
            {
                generics = new GenericTypeParameterBuilder[0];
            }

            var fields = new FieldBuilder[names.Length];

            // There are two for cycles because we want to have all the getter methods before all the other methods
            for (int i = 0; i < names.Length; i++)
            {
                // field
                fields[i] = tb.DefineField($"<{names[i]}>i__Field", generics[i].AsType(), FieldAttributes.Private | FieldAttributes.InitOnly);
                //fields[i].SetCustomAttribute(DebuggerBrowsableAttributeBuilder);

                PropertyBuilder property = tb.DefineProperty(names[i], PropertyAttributes.None, CallingConventions.HasThis, generics[i].AsType(), Type.EmptyTypes);

                // getter
                MethodBuilder getter = tb.DefineMethod($"get_{names[i]}", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, CallingConventions.HasThis, generics[i].AsType(), null);
                //getter.SetCustomAttribute(CompilerGeneratedAttributeBuilder);
                ILGenerator ilgeneratorGetter = getter.GetILGenerator();
                ilgeneratorGetter.Emit(OpCodes.Ldarg_0);
                ilgeneratorGetter.Emit(OpCodes.Ldfld, fields[i]);
                ilgeneratorGetter.Emit(OpCodes.Ret);
                property.SetGetMethod(getter);

                // setter
                MethodBuilder setter = tb.DefineMethod($"set_{names[i]}", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, CallingConventions.HasThis, null, new[] { generics[i].AsType() });
                //setter.SetCustomAttribute(CompilerGeneratedAttributeBuilder);

                // workaround for https://github.com/dotnet/corefx/issues/7792
                setter.DefineParameter(1, ParameterAttributes.In, generics[i].Name);

                ILGenerator ilgeneratorSetter = setter.GetILGenerator();
                ilgeneratorSetter.Emit(OpCodes.Ldarg_0);
                ilgeneratorSetter.Emit(OpCodes.Ldarg_1);
                ilgeneratorSetter.Emit(OpCodes.Stfld, fields[i]);
                ilgeneratorSetter.Emit(OpCodes.Ret);
                property.SetSetMethod(setter);
            }

            // ToString()
            MethodBuilder toString = tb.DefineMethod("ToString", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, CallingConventions.HasThis, typeof(string), Type.EmptyTypes);
            //toString.SetCustomAttribute(DebuggerHiddenAttributeBuilder);
            ILGenerator ilgeneratorToString = toString.GetILGenerator();
            ilgeneratorToString.DeclareLocal(typeof(StringBuilder));
            ilgeneratorToString.Emit(OpCodes.Newobj, StringBuilderCtor);
            ilgeneratorToString.Emit(OpCodes.Stloc_0);

            // Equals
            MethodBuilder equals = tb.DefineMethod("Equals", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, CallingConventions.HasThis, typeof(bool), new[] { typeof(object) });
            //equals.SetCustomAttribute(DebuggerHiddenAttributeBuilder);
            equals.DefineParameter(1, ParameterAttributes.In, "value");

            ILGenerator ilgeneratorEquals = equals.GetILGenerator();
            ilgeneratorEquals.DeclareLocal(tb.AsType());
            ilgeneratorEquals.Emit(OpCodes.Ldarg_1);
            ilgeneratorEquals.Emit(OpCodes.Isinst, tb.AsType());
            ilgeneratorEquals.Emit(OpCodes.Stloc_0);
            ilgeneratorEquals.Emit(OpCodes.Ldloc_0);

            Label equalsLabel = ilgeneratorEquals.DefineLabel();

            // GetHashCode()
            MethodBuilder getHashCode = tb.DefineMethod("GetHashCode", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, CallingConventions.HasThis, typeof(int), Type.EmptyTypes);
            //getHashCode.SetCustomAttribute(DebuggerHiddenAttributeBuilder);
            ILGenerator ilgeneratorGetHashCode = getHashCode.GetILGenerator();
            ilgeneratorGetHashCode.DeclareLocal(typeof(int));

            if (names.Length == 0)
            {
                ilgeneratorGetHashCode.Emit(OpCodes.Ldc_I4_0);
            }
            else
            {
                // As done by Roslyn
                // Note that initHash can vary, because string.GetHashCode() isn't "stable" for different compilation of the code
                int initHash = 0;

                for (int i = 0; i < names.Length; i++)
                {
                    initHash = unchecked(initHash * (-1521134295) + fields[i].Name.GetHashCode());
                }

                // Note that the CSC seems to generate a different seed for every anonymous class
                ilgeneratorGetHashCode.Emit(OpCodes.Ldc_I4, initHash);
            }

            for (int i = 0; i < names.Length; i++)
            {

                // ToString();
                ilgeneratorToString.Emit(OpCodes.Ldloc_0);
                ilgeneratorToString.Emit(OpCodes.Ldstr, i == 0 ? $"{{ {names[i]} = " : $", {names[i]} = ");
                ilgeneratorToString.Emit(OpCodes.Callvirt, StringBuilderAppendString);
                ilgeneratorToString.Emit(OpCodes.Pop);
                ilgeneratorToString.Emit(OpCodes.Ldloc_0);
                ilgeneratorToString.Emit(OpCodes.Ldarg_0);
                ilgeneratorToString.Emit(OpCodes.Ldfld, fields[i]);
                ilgeneratorToString.Emit(OpCodes.Box, generics[i].AsType());
                ilgeneratorToString.Emit(OpCodes.Callvirt, StringBuilderAppendObject);
                ilgeneratorToString.Emit(OpCodes.Pop);
            }



            // Equals()
            if (names.Length == 0)
            {
                ilgeneratorEquals.Emit(OpCodes.Ldnull);
                ilgeneratorEquals.Emit(OpCodes.Ceq);
                ilgeneratorEquals.Emit(OpCodes.Ldc_I4_0);
                ilgeneratorEquals.Emit(OpCodes.Ceq);
            }
            else
            {
                ilgeneratorEquals.Emit(OpCodes.Ret);
                ilgeneratorEquals.MarkLabel(equalsLabel);
                ilgeneratorEquals.Emit(OpCodes.Ldc_I4_0);
            }

            ilgeneratorEquals.Emit(OpCodes.Ret);

            // GetHashCode()
            ilgeneratorGetHashCode.Emit(OpCodes.Stloc_0);
            ilgeneratorGetHashCode.Emit(OpCodes.Ldloc_0);
            ilgeneratorGetHashCode.Emit(OpCodes.Ret);

            // ToString()
            ilgeneratorToString.Emit(OpCodes.Ldloc_0);
            ilgeneratorToString.Emit(OpCodes.Ldstr, names.Length == 0 ? "{ }" : " }");
            ilgeneratorToString.Emit(OpCodes.Callvirt, StringBuilderAppendString);
            ilgeneratorToString.Emit(OpCodes.Pop);
            ilgeneratorToString.Emit(OpCodes.Ldloc_0);
            ilgeneratorToString.Emit(OpCodes.Callvirt, ObjectToString);
            ilgeneratorToString.Emit(OpCodes.Ret);

            type = tb.CreateType();
            if (types.Count != 0)
            {
                type = type.MakeGenericType(types.ToArray());
            }

            TypeBuilder tb2 = ModuleBuilder.DefineType($"{ns}.{typeName}", TypeAttributes.AnsiClass | TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoLayout | TypeAttributes.BeforeFieldInit, type);
            type = tb2.CreateType();



            return type;
        }
    }
}
