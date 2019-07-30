using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberToWordApp
{
    class Program
    {
        static Dictionary<string, string> numberDict = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter a Number to convert:");
                string number = Console.ReadLine();
                number = Convert.ToDouble(number).ToString();
                PopulateNumberDict();
                string word = ConvertWholeNumber(number).Trim();
                string output = RemoveUnnecessaryAnd(word);              
                Console.WriteLine("The number in British English format is \n{0}", output);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string RemoveUnnecessaryAnd(string word)
        {
            string[] words = word.Split(' ');
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != "hundred")
                    output.Append(words[i] + " ");
                else
                {
                    output.Append(words[i] + " ");
                    string currentword = words[++i];
                    if (i == words.Length-1) return output.ToString();
                    string nextword = words[++i];
                    if (!numberDict.Values.Contains(nextword + " "))
                        output.Append(nextword + " ");
                    else
                        output.Append(currentword + " " + nextword + " ");
                }
            }
            return output.ToString();
        }

        private static void PopulateNumberDict()
        {
            numberDict.Add("1", "one ");
            numberDict.Add("2", "two ");
            numberDict.Add("3", "three ");
            numberDict.Add("4", "four ");
            numberDict.Add("5", "five ");
            numberDict.Add("6", "six ");
            numberDict.Add("7", "seven ");
            numberDict.Add("8", "eight ");
            numberDict.Add("9", "nine ");
            numberDict.Add("10", "ten ");
            numberDict.Add("11", "eleven ");
            numberDict.Add("12", "twelve ");
            numberDict.Add("13", "thirteen ");
            numberDict.Add("14", "fourteen ");
            numberDict.Add("15", "fifteen ");
            numberDict.Add("16", "sixteen ");
            numberDict.Add("17", "seventeen ");
            numberDict.Add("18", "eighteen ");
            numberDict.Add("19", "nineteen ");
            numberDict.Add("20", "twenty ");
            numberDict.Add("30", "thirty ");
            numberDict.Add("40", "forty ");
            numberDict.Add("50", "fifty ");
            numberDict.Add("60", "sixty ");
            numberDict.Add("70", "seventy ");
            numberDict.Add("80", "eighty ");
            numberDict.Add("90", "ninety ");


        }

        private static String ConvertWholeNumber(string number)
        {
            string word = "";

            bool isDone = false;
            int numDigits = number.Length;
            double value = Convert.ToDouble(number);
            if (value == 0)
            {
                isDone = true;
                return word;
            }
            int pos = 0;
            String place = "";
            switch (numDigits)
            {
                case 1://ones' range  

                    word = numberDict[number];
                    isDone = true;
                    break;
                case 2://tens' range  
                    if (number.StartsWith("0"))
                        word = numberDict[number.Substring(1)];
                    else if(!numberDict.Keys.Contains(number))
                        word = numberDict[number.Substring(0, 1) + "0"] + " " + numberDict[number.Substring(1)];
                    else
                        word = numberDict[number];
                    isDone = true;
                    break;
                case 3://hundreds' range    
                    pos = (numDigits % 3) + 1;
                    place = " hundred and ";
                    break;
                case 4://thousands' range    
                case 5:
                case 6:
                    pos = (numDigits % 4) + 1;
                    place = " thousand ";
                    break;
                case 7://millions' range    
                case 8:
                case 9:
                    pos = (numDigits % 7) + 1;
                    place = " million ";
                    break;
                default:
                    isDone = true;
                    break;
            }
            if (!isDone)
            {
                if (number.Substring(0, pos) != "0" && number.Substring(pos) != "0")
                {
                    word = ConvertWholeNumber(number.Substring(0, pos)) + place + ConvertWholeNumber(number.Substring(pos));
                }
                else
                {
                    word = ConvertWholeNumber(number.Substring(0, pos)) + ConvertWholeNumber(number.Substring(pos));
                }

            }
            if (word.Trim().Equals(place.Trim())) word = "";


            return word.Trim();
        }
    }
}

