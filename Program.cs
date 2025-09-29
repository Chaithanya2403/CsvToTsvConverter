using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertCsvToTsv
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Enter the input and output paths to read and write the data
            Console.Write("Enter the input CSV file path: ");
            string inputFilePath = Console.ReadLine();
            Console.Write("Enter the output TSV file path: ");
            string outputFilePath = Console.ReadLine();

            Console.Write("Do you want to convert the first X lines or last Y lines? (Enter 'first' or 'last'): ");
            string option = Console.ReadLine()?.ToLower();

            int? firstXLines = null;
            int? lastYLines = null;

            if (option == "first")
            {
                Console.Write("Enter the number of first X lines to convert: ");
                if (int.TryParse(Console.ReadLine(), out int x))
                {
                    firstXLines = x;
                }
            }
            else if (option == "last")
            {
                Console.Write("Enter the number of last Y lines to convert: ");
                if (int.TryParse(Console.ReadLine(), out int y))
                {
                    lastYLines = y;
                }
            }
            try
            {
                if (!File.Exists(inputFilePath))
                {
                    Console.WriteLine("The input file does not exist. Please check the path and try again.");
                    return;
                }

                var lines = File.ReadAllLines(inputFilePath);
                IEnumerable<string> selectedLines = lines;

                if (firstXLines.HasValue)
                {
                    selectedLines = selectedLines.Take(firstXLines.Value);
                }
                else if (lastYLines.HasValue)
                {
                    selectedLines = selectedLines.Skip(Math.Max(0, lines.Length - lastYLines.Value));
                }
                var tsvLines = selectedLines.Select(line => line.Replace(",", "\t"));
                File.WriteAllLines(outputFilePath, tsvLines);
                Console.WriteLine($"Successfully converted {inputFilePath} to {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}
