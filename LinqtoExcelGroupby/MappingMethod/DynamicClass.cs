using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace LinqtoExcelGroupby.MappingMethod
{
    class DynamicClass: ExtendedDataLevel
    {
        public  void createTempField(int num)
        {
            if (num != 0)
            {
                for (int i = 0; i < num; i++)
                    buildTheField("fiedl" + Convert.ToString(i + 1), typeof(string));

            }
        }

        private void buildTheField(string fieldName, Type fieldType)
        {
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "DynamicAssembly";
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("module");
            TypeBuilder myTypeBuilder = moduleBuilder.DefineType("DynamicAssembly", TypeAttributes.Public);
            FieldBuilder fieldBuilder = myTypeBuilder.DefineField(fieldName, fieldType, FieldAttributes.Public);

            PropertyBuilder propertyBuilder = myTypeBuilder.DefineProperty(fieldName, PropertyAttributes.HasDefault, fieldType, null);
            MethodBuilder getPropMthdBldr = myTypeBuilder.DefineMethod(fieldName, MethodAttributes.Public);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr = myTypeBuilder.DefineMethod(fieldName, MethodAttributes.Public);

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);

        }
    }
}
