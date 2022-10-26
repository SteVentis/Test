using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [MemoryDiagnoser]
    public class BenchMarks
    {
        public List<ReflectionClass<MyClass>> reflectors { get; set; }

        public BenchMarks()
        {

        }

        public void CreateObjectsAndReflectors()
        {
            reflectors = new List<ReflectionClass<MyClass>>();
            for (int i = 0; i < 1000; i++)
            {
                MyClass myClass = new MyClass();
                ReflectionClass<MyClass> reflectionClass = new ReflectionClass<MyClass>(myClass);
                reflectors.Add(reflectionClass);
            }
        }


        [Benchmark]
        public void FillObjects()
        {
            CreateObjectsAndReflectors();

            for (int i = 0; i < 1000; i++)
            {
                for (int k = 0; k < reflectors.Count; k++)
                {
                    int width = (int)reflectors[k].GetValue("width");
                    reflectors[k].SetValue("width", width + new Random().Next());

                    int height = (int)reflectors[k].GetValue("height");
                    reflectors[k].SetValue("height", height + new Random().Next());

                    Vector3 position = (Vector3)reflectors[k].GetValue("position");
                    reflectors[k].SetValue("position", position + new Vector3(10, 0, 0));

                    Quaternion quaternion = (Quaternion)reflectors[k].GetValue("rotation");
                    reflectors[k].SetValue("rotation", quaternion + new Quaternion(10, 0, 10, 0));
                }
            }
        }

        [Benchmark]
        public void CreateObjects()
        {
            
            MyClass myClass;
            ReflectionClassOptimized<MyClass> reflector;
            Random random = new Random();
            int width;
            int height;
            Vector3 position;
            Quaternion quaternion;

            for (int i = 0; i < 1000; i++)
            {
                myClass = new MyClass();
                reflector = new ReflectionClassOptimized<MyClass>(myClass);

                width = (int)reflector.GetValue("width");
                reflector.SetValue("width", width + random.Next());

                height = (int)reflector.GetValue("height");
                reflector.SetValue("height", height + random.Next());

                position = (Vector3)reflector.GetValue("position");
                reflector.SetValue("position", position + new Vector3(10, 0, 0));

                quaternion = (Quaternion)reflector.GetValue("rotation");
                reflector.SetValue("rotation", quaternion + new Quaternion(10, 0, 10, 0));
            }
        }

        [Benchmark]
        public void CreateObjectsAsync()
        {
            MyClass myClass;
            ReflectionClassOptimized<MyClass> reflector;
            Random random = new Random();
            int width;
            int height;
            Vector3 position;
            Quaternion quaternion;

            Parallel.For(0, 1000, (i) =>
            {
                myClass = new MyClass();
                reflector = new ReflectionClassOptimized<MyClass>(myClass);

                width = (int)reflector.GetValue("width");
                reflector.SetValue("width", width + random.Next());

                height = (int)reflector.GetValue("height");
                reflector.SetValue("height", height + random.Next());

                position = (Vector3)reflector.GetValue("position");
                reflector.SetValue("position", position + new Vector3(10, 0, 0));

                quaternion = (Quaternion)reflector.GetValue("rotation");
                reflector.SetValue("rotation", quaternion + new Quaternion(10, 0, 10, 0));

            });
        }

        
        
    }
}
