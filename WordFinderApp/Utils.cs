using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinderApp
{
    internal class Utils
    {
        public static IEnumerable<string> GetFileFromConsole()
        {
            bool valid;
            string filePath;
            do
            {
                valid = true;
                filePath = Console.ReadLine();
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    Console.WriteLine("Error: The file could not be found. Try Again");
                    valid = false;
                }
            } while (!valid);

            return File.ReadLines(filePath);
        }

        public static IEnumerable<string> GetFileFromConfig(IConfiguration config, string configParameter)
        {
            var filePath = config.GetValue<string>(configParameter);
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                Console.WriteLine($"Error: The file could not be found. Fix the {configParameter} setting.");
            }
            return File.ReadLines(filePath);
        }
    }
}

