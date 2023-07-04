using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EsotericDevZone.Core
{
    public static partial class Extensions
    {
        public static string Indent(this string str, string beforeLine = "    ")
        {
            return str.Split('\n').Select(line => beforeLine + line).JoinToString("\n");
        }

        public static string JoinToString<T>(this IEnumerable<T> values, string separator)
            => string.Join(separator, values);

        private static readonly Dictionary<char, string> CharsToEscape = new Dictionary<char, string>
        {
            { '\"', "\\\""},
            { '\\', @"\\"},
            { '\0', @"\0"},
            { '\a', @"\a"},
            { '\b', @"\b"},
            { '\f', @"\f"},
            { '\n', @"\n"},
            { '\r', @"\r"},
            { '\t', @"\t"},
            { '\v', @"\v"},
        };

        /// <summary>
        /// Converts a string value to its literal constant string form (e.g "my    string" => "\"my\tstring\"")
        /// </summary>                                
        public static string ToLiteral(this string input)
        {
            StringBuilder literal = new StringBuilder(input.Length + 2);
            literal.Append("\"");
            foreach (var c in input)
            {
                if (CharsToEscape.ContainsKey(c))
                    literal.Append(CharsToEscape[c]);
                else if (c >= 0x20 && c <= 0x7e)
                    literal.Append(c);
                else
                    literal.Append($@"\u{(int)c:x4}");
            }
            literal.Append("\"");
            return literal.ToString();
        }

        public static string FromLiteral(this string input)
        {
            Validation.Assert(input.StartsWith("\"") && input.EndsWith("\""));

            string result = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\')
                {                    
                    i++;
                    if (i >= input.Length)
                        throw new ArgumentException("Unfinished escape sequence");
                    if (input[i] == 'u')
                    {
                        i--;
                        result += (input[i]);
                        continue;
                    }
                    else
                    {
                        switch (input[i])
                        {
                            case '\\': result += ("\\"); break;
                            case '"': result += ("\""); break;
                            case '0': result += ("\0"); break;
                            case 'a': result += ("\a"); break;
                            case 'b': result += ("\b"); break;
                            case 'f': result += ("\f"); break;
                            case 'n': result += ("\n"); break;
                            case 'r': result += ("\r"); break;
                            case 't': result += ("\t"); break;
                            case 'v': result += ("\v"); break;
                            default: result += (input[i]); break;
                        }
                    }
                }
                else
                    result += (input[i]);
            }

            result = Regex.Replace(result, @"\\u[A-Fa-f0-9]{4,6}", m =>
            {
                int code = int.Parse(m.Value.Substring(2), System.Globalization.NumberStyles.HexNumber);
                return char.ConvertFromUtf32(code).ToString();
            });
            return Regex.Replace(result, "\\\\u", "u");
        }
    }
}
