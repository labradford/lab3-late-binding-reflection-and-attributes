using System;
using System.IO;
using System.Reflection;

namespace Tester
{

    class Program
    {

        static void Main(string[] args)
        {

            ReflectionTest();
            PersonTest();
            MathStuffTest();
            CustomAttributeTest();

            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to quit...");
            Console.ReadLine();
        }
        public static void ReflectionTest()
        {
            Console.WriteLine();
            Console.WriteLine("{0} ReflectionTest {0}", new string('=', 20));
            Console.WriteLine();

            var parentFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var absolutePath = Path.Combine(parentFolder, "SharedLib.dll");
            Assembly asm = Assembly.LoadFile(absolutePath);

            Console.WriteLine("Assembly: {0}", asm.FullName);
            PrintModules(asm);
        }
    

        private static void PrintModules(Assembly asm)
        {
            foreach (Module m in asm.GetModules())
            {
                Console.WriteLine("  Module: {0}", m.Name);
                PrintTypes(m);
            }
        }

        private static void PrintTypes(Module module)
        {
            foreach (Type t in module.GetTypes())
            {
                if (t.IsClass)
                {
                    Console.WriteLine("    Type: {0, 4} class", t.Name);

                }
                if (t.IsEnum)
                {
                    Console.WriteLine("    Type: {0} struct enum", t.Name);
                }
                PrintConstructors(t);
                PrintEvents(t);
                PrintFields(t);
                PrintProperties(t);
                PrintMethods(t);
            }
        }

        private static void PrintConstructors(Type type)
        {
            foreach (ConstructorInfo ci in type.GetConstructors())
            {
                Console.WriteLine("      Ctor: {0}", ci.Name);
                PrintConstructorParameters(ci);
            }
        }

        private static void PrintEvents(Type type)
        {
            foreach (EventInfo ei in type.GetEvents())
            {
                Console.WriteLine("        Event: {0} {1}", ei.EventHandlerType, ei.Name);
            }
        }

        private static void PrintFields(Type type)
        {
            foreach (FieldInfo fi in type.GetFields())
            {
                Console.WriteLine("        Field: {0} {1}", fi.FieldType, fi.Name);
            }
        }

        private static void PrintProperties(Type type)
        {
            foreach (PropertyInfo pi in type.GetProperties())
            {
                Console.WriteLine("        Property: {0} {1}", pi.PropertyType, pi.Name);
            }
        }

        private static void PrintMethods(Type type)
        {
            foreach (MethodInfo mi in type.GetMethods())
            {
                Console.WriteLine("        Method: {0}() returns {1}", mi.Name, mi.ReturnType);
                PrintMethodParameters(mi);
            }
        }

        private static void PrintConstructorParameters(ConstructorInfo ci)
        {
            Console.WriteLine("          Parameters:");
            foreach (ParameterInfo pi in ci.GetParameters())
            {
                Console.WriteLine($"            {pi.ParameterType} {pi.Name}");
            }
        }

        private static void PrintMethodParameters(MethodInfo mi)
        {
            Console.WriteLine("          Parameters:");
            foreach (ParameterInfo pi in mi.GetParameters())
            {
                Console.WriteLine($"            {pi.ParameterType} {pi.Name}");
            }
        }

        public static void PersonTest()
        {
            Console.WriteLine();
            Console.WriteLine("{0} PersonTest {0}", new string('=', 20));
            Console.WriteLine();

            var parentFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var absolutePath = Path.Combine(parentFolder, "SharedLib.dll");
            Assembly asm = Assembly.LoadFile(absolutePath); Console.WriteLine(asm.GetType());

            dynamic p1 = asm.CreateInstance("SharedLib.Person");
            p1.LastName = "Smith";
            p1.FirstName = "Jane";
            p1.DOB = DateTime.Parse("12/1/2000");
            Type enumType = asm.GetType("SharedLib.Person+Genders");
            p1.Gender = (dynamic)Enum.Parse(enumType, "Female");

            Console.WriteLine(p1);

            dynamic p2 = asm.CreateInstance("SharedLib.Person", false, BindingFlags.CreateInstance, null, new object[] { "Smith", "John", DateTime.Parse("1/1/2000"), (dynamic)Enum.Parse(enumType, "Male") }, null, null);
            Console.WriteLine(p2);
        }

        public static void MathStuffTest()
        {
            Console.WriteLine();
            Console.WriteLine("{0} MathStuffTest {0}", new string('=', 20));
            Console.WriteLine();

            var parentFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var absolutePath = Path.Combine(parentFolder, "SharedLib.dll");
            Assembly asm = Assembly.LoadFile(absolutePath);
            
            Type mathType = asm.GetType("SharedLib.MathStuff");
            var circleAreaMethod = mathType.GetMethod("CircleArea", BindingFlags.Public | BindingFlags.Static);
            double circleArea = (double)circleAreaMethod.Invoke(null, new object[] { 12.34 });
            Console.WriteLine($"Circle Area with a radius of 12.34: {circleArea:0.00}");
        }

        public static void CustomAttributeTest()
        {
            Console.WriteLine();
            Console.WriteLine("{0} CustomAttributeTest {0}", new string('=', 20));
            Console.WriteLine();

            var parentFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var absolutePath = Path.Combine(parentFolder, "SharedLib.dll");
            Assembly asm = Assembly.LoadFile(absolutePath);
            
            Type mathType = asm.GetType("SharedLib.MathStuff");
            Type personType = asm.GetType("SharedLib.Person");
            Type specialType = asm.GetType("SharedLib.SpecialClassAttribute");
            Type statsType = asm.GetType("SharedLib.StatisticsStuff");
            Type recursiveType = asm.GetType("SharedLib.RecursiveStuff");
            
            var attrs = mathType.GetCustomAttributes(specialType);
            foreach (dynamic attr in attrs)
            {
                Console.WriteLine($"{mathType.Name} has the special class ID of {attr.ID}");
            }

            var pattrs = personType.GetCustomAttributes(specialType);
            foreach (dynamic attr in pattrs)
            {
                Console.WriteLine($"{personType.Name} has the special class ID of {attr.ID}");
            }

            var stattrs = statsType.GetCustomAttributes(specialType);
            foreach (dynamic attr in stattrs)
            {
                Console.WriteLine($"{statsType.Name} has the special class ID of {attr.ID}");
            }

            var rattrs = recursiveType.GetCustomAttributes(specialType);
            foreach (dynamic attr in rattrs)
            {
                Console.WriteLine($"{recursiveType.Name} has the special class ID of {attr.ID}");
            }
        }

    }
}
