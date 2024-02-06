using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace p5lab
{
    class Program
    {
        //constants
        const string Cfd = "Text.txt";
        const string Cfrez = "..\\..\\Results.txt";
        const string Cfana = "..\\..\\Analysis.txt";
        
        static void Main(string[] args)
        {
            //deletes results file if it exists
            if (File.Exists(Cfrez))
                File.Delete(Cfrez);
            //deletes analysis file if it exists
            if (File.Exists(Cfana))
                File.Delete(Cfana);
            char[] separators = { ' ', '.', ',', '!', '?', ':', ';', '(', ')', '\t' };
            ReadWrite(Cfd, Cfrez, Cfana, separators);
        }
        /// <summary>
        /// Reads the data file and write in the results and analysis files
        /// </summary>
        /// <param name="fv">data file</param>
        /// <param name="rfv">results file</param>
        /// <param name="afv">analysis file</param>
        /// <param name="separators">separators</param>
        static void ReadWrite(string fv,string rfv,string afv, char[] separators)
        {
            int linecount = 0; // number of lines with odd numbers
            int n = 0; //number of words in a line
            string word = "";
            string line;
            using (var fra = File.CreateText(afv))
            {
               using (var fr = File.CreateText(rfv))
               {
                    using (StreamReader reader = new StreamReader(fv, Encoding.GetEncoding(1257)))
                    {
                      fr.WriteLine("------------------------------------------------");
                        fr.WriteLine("Text before editing");
                        fr.WriteLine("------------------------------------------------");
                        while ((line = reader.ReadLine()) != null)
                        {
                        fr.WriteLine(line);
                        }
                        fr.WriteLine("================================================");
                    }
                    using (StreamReader reader = new StreamReader(fv, Encoding.GetEncoding(1257)))
                    {
                       fr.WriteLine("Text after editing");
                       fr.WriteLine("------------------------------------------------");
                       while ((line = reader.ReadLine()) != null)
                       {
                          string[] parts = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                          WordsCount(parts, line, separators, out n);
                          //checks if lines have even or odd number of words
                          if (n % 2 !=0)
                          {
                                linecount++;
                                WordFinder(line, separators, parts, n, out word);
                                fra.WriteLine("{0} ------> XXOOXX", word);
                                fr.WriteLine(line.Replace(word, "XX00XX"));
                          }
                          else
                          fr.WriteLine(line);
                          n = 0;
                       }
                       //if there is no lines with odd number of words
                       if (linecount == 0)
                       {
                          fr.WriteLine("\nthere is no lines with odd number of words");
                          fr.WriteLine("------------------------------------------------");
                          fra.WriteLine("there is no lines with odd number of words");
                       }
                    }
               }
            }
        }
        /// <summary>
        /// finds out how many words are in a line of text
        /// </summary>
        /// <param name="parts"> word array</param>
        /// <param name="line">text line</param>
        /// <param name="separators">separators</param>
        /// <param name="n">number of words in a line</param>
        static int WordsCount(string[] parts, string line, char[] separators, out int n)
        {
            n = 0;
            foreach (string word in parts)
            {
                n++;
            }
            return n;
        }
        /// <summary>
        /// finds a certain word in a line
        /// </summary>
        /// <param name="line">text line</param>
        /// <param name="separators">separators</param>
        /// <param name="parts"> word array</param>
        /// <param name="n">number of words in a line</param>
        /// <param name="word">the word that was found</param>
        static string WordFinder(string line, char[] separators, string[] parts, int n, out string word)
        {
            word = "";
            int wordnr = 0;
            foreach (string word1 in parts)
            {
                wordnr++;
                if (wordnr == n / 2 + 1)
                {
                    word = word1;
                    break;
                }
            }
            return word;
        }
    }
}



