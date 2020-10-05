using System;

namespace ALCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ALTableGenerator aLTableGenerator = new ALTableGenerator
            {
                Path = ".\\Teszt.csv"
            };
            aLTableGenerator.ImportCSVFile();
            Console.WriteLine("CSV Imported.");

            aLTableGenerator.GenerateALTableCode();
            Console.WriteLine("AL Code Generated.");

            aLTableGenerator.ExportALTableCode();
            Console.WriteLine("AL Code exported.");

            Console.ReadKey();
        }
    }
}
