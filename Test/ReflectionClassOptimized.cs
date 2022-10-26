using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    
    public class ReflectionClassOptimized <T>
    {
        Type type;
        T instance;


        public ReflectionClassOptimized(T instance)
        {
            this.instance = instance;
            this.type = instance.GetType();
        }

        public object GetValue(string name)
        {
            object _object = null;
            FieldInfo[] fields = type.GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                if(fields[i].Name == name)
                {
                    _object = fields[i].GetValue(instance);
                    break;
                } 
            }
            return _object;
        }
        
        public void SetValue(string name, object val)
        {
            FieldInfo[] fields = type.GetFields();
            for(int i = 0; i < fields.Length; i++)
            {
                if (fields[i].Name == name)
                {
                    fields[i].SetValue(instance, val);
                }
            }
        }

    }
}
