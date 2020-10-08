﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace IniParser
{
    class IniCollection
    {
        public Dictionary<string, Dictionary<string, string>> iniCollection = new Dictionary<string, Dictionary<string, string>>();
    }
    
    
    
    class IniParser
    {
        private string _filePath;
        private string _sectionPattern = @"\[(.*?)\]";
        public IniCollection data = new IniCollection();

        private string _WhtiespaceRemover(string line)
        {
            line = string.Join("", line.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            return line;
        }
        
        private string _CommentRemover(string line)
        {
            if (line.Contains(";"))
            {
                line = line.Remove(line.IndexOf(";", StringComparison.Ordinal));
            }
            
            return line;
        }

        private bool _CheckIfSectionOrParameterExists(string sectionName, string parameterName)
        {
            if (!data.iniCollection.ContainsKey(sectionName))
            {
                return false;
            }

            if (!data.iniCollection[sectionName].ContainsKey(parameterName))
            {
                return false;
            }
            
            return true;
        }
        
        public void TryGetInt(string sectionName, string parameterName, out int value)
        {
            if (!_CheckIfSectionOrParameterExists(sectionName, parameterName))
            {
                throw new ArgumentException("Incorrect Section or Parameter name");
            }
            
            try
            {
                value = 
                    // ReSharper disable once PossibleNullReferenceException
                    (int) Convert.ChangeType(data.iniCollection[sectionName][parameterName], typeof(int));
            }
            catch (FormatException)
            {
                throw new FormatException("Cannot convert this string to int");
            }
        }

        public void TryGetFloat(string sectionName, string parameterName, out float value)
        {
            if (!_CheckIfSectionOrParameterExists(sectionName, parameterName))
            {
                throw new ArgumentException("Incorrect Section or Parameter name");
            }
            
            try
            {
                value =
                    // ReSharper disable once PossibleNullReferenceException
                    (float) Convert.ChangeType(data.iniCollection[sectionName][parameterName], typeof(float));
            }
            catch (FormatException)
            {
                throw new FormatException("Cannot convert this string to float");
            }
             
        }

        public void TryGetString(string sectionName, string parameterName, out string value)
        {
            value = data.iniCollection[sectionName][parameterName];
        }
        
        public int SetFilePath(string path, bool debug)
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
            switch (debug) //Todo: should prolly remove that later
            {
                case true:
                    Console.WriteLine("File path's set");
                    break;
            }

            return 0; // All good, path is set
        }
        public void PrintIniCollection()
        {
            foreach (var section in data.iniCollection)
            {
                Console.WriteLine($"[{section.Key}]");
                foreach (var parval in data.iniCollection[section.Key])
                {
                    Console.WriteLine($"{parval.Key} = {parval.Value}");
                }
            }
        }
        public Dictionary<string, Dictionary<string, string>> IniParse()
        {
            string[] allLines = File.ReadAllLines(_filePath);
            Regex rgxSectionPattern = new Regex(_sectionPattern);
            string lastSection = "NO_SECTION";
            int counter = 1;

            foreach (string s in allLines)
            { 
                var commentlessString = _CommentRemover(s);
                
                if (String.IsNullOrWhiteSpace(commentlessString))
                {
                    counter += 1;
                    continue;
                }
                if (rgxSectionPattern.IsMatch(commentlessString))
                {
                    string trimmedSectionName = s.Trim(new Char[] {'[', ']', ' '});
                    lastSection = trimmedSectionName;
                    data.iniCollection.Add(lastSection, new Dictionary<string, string>());
                    counter += 1;
                }
                else if (commentlessString.Contains("="))
                {
                    var spacelessString = _WhtiespaceRemover(commentlessString);
                    if (spacelessString.Split("=").Length < 1)
                    {
                        string parameter = spacelessString;
                        data.iniCollection[lastSection].Add(parameter, String.Empty);
                    }
                    else
                    {
                        var parameter = spacelessString.Split("=")[0];
                        var value = spacelessString.Split("=")[1];
                        data.iniCollection[lastSection].Add(parameter, value);
                    }

                    counter += 1;
                }
                else
                {
                    throw new InvalidDataException($"Error reading line {counter} in the .ini file. " +
                                                   $"Check if it is correct.");
                }

            }
            // Console.WriteLine($"\nRead total of {counter} lines.");
            return data.iniCollection;
        }


    }
}
