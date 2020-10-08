using System;
using System.Security.Cryptography;

namespace IniParser
{
    class Program
    {
        static void Main(string[] args)
        {
            IniParser parser = new IniParser();
            parser.SetFilePath(@"C:\Users\nelii\OneDrive\Repos\OOPLabs\IniParser\info.ini", true); // Todo: Make it user input
            var collection = parser.IniParse();
            parser.PrintIniCollection();
            parser.TryGetString("NCMD", "Test", out var str);
            Console.WriteLine($"\n{str}");
            parser.TryGetFloat("ADC_DEV", "BufferLenSecons", out var flt);
            Console.WriteLine($"\n{flt}");

        }

        
    }
}