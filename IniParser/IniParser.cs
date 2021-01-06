using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace IniParser
{
    class IniParser
    {
        private string _filePath;
        private readonly string _sectionPattern = @"\[(.*?)\]";

        private readonly Dictionary<string, Dictionary<string, string>> _iniCollection =
            new Dictionary<string, Dictionary<string, string>>();

        private static string _WhitespaceRemover(string line)
        {
            line = string.Join("", line.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            return line;
        }

        private static string _CommentRemover(string line)
        {
            if (line.Contains(";"))
            {
                line = line.Remove(line.IndexOf(";", StringComparison.Ordinal));
            }

            return line;
        }

        private bool _CheckIfSectionOrParameterExists(string sectionName, string parameterName) =>
            _iniCollection.ContainsKey(sectionName) && _iniCollection[sectionName].ContainsKey(parameterName);

        public void TryGet<T>(string sectionName, string parameterName, out T value)
        {
            if (!_CheckIfSectionOrParameterExists(sectionName, parameterName))
            {
                throw new ArgumentException("Incorrect Section or Parameter name");
            }

            try
            {
                value = (T) Convert.ChangeType(_iniCollection[sectionName][parameterName], typeof(T));
            }
            catch (FormatException)
            {
                throw new FormatException($"Cannot convert this string to {typeof(T)}");
            }
        }

        public void SetFilePath(string path, bool debug)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File doesn't exist", path);
            }

            if (!path.Contains(".ini"))
            {
                throw new FileNotFoundException("Incorrect file name", path);
            }

            _filePath = path;
            switch (debug)
            {
                case true:
                    Console.WriteLine("File path's set");
                    break;
            }
        }
        public void PrintIniCollection()
        {
            foreach (var section in _iniCollection)
            {
                Console.WriteLine($"[{section.Key}]");
                foreach (var parval in _iniCollection[section.Key])
                {
                    Console.WriteLine($"{parval.Key} = {parval.Value}");
                }
            }
        }
        public Dictionary<string, Dictionary<string, string>> IniParse()
        {
            var allLines = File.ReadAllLines(_filePath);
            var rgxSectionPattern = new Regex(_sectionPattern);
            string lastSection = "NO_SECTION_ENTRY";
            int counter = 1;

            if (!rgxSectionPattern.IsMatch(allLines[0]))
            {
                _iniCollection.Add(lastSection, new Dictionary<string, string>());
            }
            
            foreach (string s in allLines)
            { 
                var commentlessString = _CommentRemover(s);
                
                if (string.IsNullOrWhiteSpace(commentlessString))
                {
                    counter++;
                    continue;
                }
                if (rgxSectionPattern.IsMatch(commentlessString))
                {
                    string trimmedSectionName = s.Trim('[', ']', ' ');
                    lastSection = trimmedSectionName;
                    _iniCollection.Add(lastSection, new Dictionary<string, string>());
                    counter++;
                }
                else if (commentlessString.Contains("="))
                {
                    var spacelessString = _WhitespaceRemover(commentlessString);
                    if (spacelessString.Split("=").Length < 1)
                    {
                        string parameter = spacelessString;
                        _iniCollection[lastSection].Add(parameter, string.Empty);
                    }
                    else
                    {
                        var parameter = spacelessString.Split("=")[0];
                        var value = spacelessString.Split("=")[1];
                        _iniCollection[lastSection].Add(parameter, value);
                    }

                    counter++;
                }
                else
                {
                    throw new InvalidDataException($"Error reading line {counter} in the .ini file. " +
                                                   $"Check if it is correct.");
                }

            }
            return _iniCollection;
        }


    }
}
