using System;
using System.Security.Cryptography;

namespace IniParser
{
    class Program
    {
        static void Main(string[] args)
        {
            IniParser parser = new IniParser();
            parser.SetFilePath(@"C:\Users\nelii\OneDrive\Repos\OOPLabs\IniParser\info.ini", true);
            var collection = parser.IniParse();
            parser.PrintIniCollection();
            parser.TryGet<string>("NCMD", "Test", out var str);
            Console.WriteLine($"\n{str}");
            parser.TryGet<double>("ADC_DEV", "BufferLenSecons", out var flt);
            Console.WriteLine($"{flt}");
            parser.TryGet<double>("FUN", "HelloFrediKats", out var dbl);
            Console.WriteLine($"{dbl}");

        }
    }
}