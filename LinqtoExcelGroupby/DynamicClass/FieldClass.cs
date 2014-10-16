using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LinqtoExcelGroupby.DynamicClass
{
    class FieldClass
    {
        public string FieldName { set; get; }
        public Type FieldType { set; get; }

        public FieldClass(string name, Type type)
        {
            this.FieldName = name;
            this.FieldType = type;
        }
    }
}
