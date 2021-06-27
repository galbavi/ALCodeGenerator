using System;
using ALGenerator;

namespace ALCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ALTableGenerator aLTableGenerator = new ALTableGenerator
            {
                Path = "E:\\Downloads\\tesz.csv"
            };
            aLTableGenerator.ImportTableCSVFile();
            Console.WriteLine("CSV Imported.");

            aLTableGenerator.GenerateALTableCode();
            Console.WriteLine("AL Code Generated.");

            aLTableGenerator.ExportALTableCode();
            Console.WriteLine("AL Code exported.");

            Console.ReadKey();
        }
    }
}
