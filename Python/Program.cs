/*
using Python.Runtime;

namespace Python
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize the Python engine
            PythonEngine.Initialize();

            // Load the Python script
            dynamic multiplyNumbersModule = PythonEngine.ModuleFromString("multiply_numbers", File.ReadAllText("multiply_numbers.py"));
            // We call the Python function
            dynamic output = multiplyNumbersModule.multiply_numbers(1, 2, 3);

            // Print the result in the console.
            Console.WriteLine(output);  // Output: 6    
            
            PythonEngine.Initialize();
`
            using (Py.GIL())
            {
                PythonEngine.Exec("doStuff()");
            }
            */
using System;
using Python.Runtime;

namespace PythonIntegration
{

        class Program
        {
            static void Main(string[] args)
            {
            string str2 = "hello";
            string str1 = "world";

            var uniqueChars = str1.Except(str2).Union(str2.Except(str1));

            foreach (var ch in uniqueChars)
            {
                Console.WriteLine(ch);
            }
        
        Runtime.PythonDLL = @"C:\ProgramData\anaconda3\python3.dll";

            try
            {

                using (Py.GIL())
                {
                    PythonEngine.BeginAllowThreads();

                    dynamic addNumbersModule = Py.Import("multiply_numbers");
                    dynamic addNumbersFunction = addNumbersModule.multiply_numbers;

                    int a = 5;
                    int b = 3;
                    int c = 4;
                    dynamic result = addNumbersFunction(a, b, c);

                    Console.WriteLine($"Result of adding {a} and {b} is: {result}");
                    PythonEngine.EndAllowThreads(1);

                }
            }
              catch (Exception ex)
              {
                Console.WriteLine("An error occurred: " + ex.Message);
                // Handle the exception accordingly
              }

            }
        }
}


