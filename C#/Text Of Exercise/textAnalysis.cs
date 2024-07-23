using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using IronXL;
using System.Collections;
using MyProject.Classes;
using SixLabors.ImageSharp.Formats.Tga;
using System.Reflection;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.ColorSpaces;
using static IronPython.Modules._ast;
using System.Collections.Specialized;
using Microsoft.Scripting.Ast;
using System.Reflection.Metadata;
using Microsoft.CSharp.RuntimeBinder;
//using static IronOcr.OcrResult;


namespace MyProject.Text_Of_Exercise
{

    internal class TextAnalysis :Sentence
    {
        public static string textOfExercise;
        public static string data;
        public static string proofs;
        public TextAnalysis()
        {
        }

        /// <summary>
        /// העלאת קובץ אקסל של מילות המפתח
        /// </summary>
        /// <param name="textOfExercise"></param>
        /// <returns></returns>
        public WorkSheet Uploading_an_Excel_file()
        {
            WorkBook wb = WorkBook.Load("C:\\Users\\win 10\\Desktop\\my-project\\C#\\Text Of Exercise\\key_words.xlsx");
            WorkSheet ws = wb.GetWorkSheet("sheet1");
            return ws;
        }

        /// <summary>
        /// מחלקת את הטקסט לנתונים והוכחות
        /// </summary>
        public void Divides_the_text_into_facts_and_proofs()
        {
            #region a
            string pattern1 = @"\bא\.\b";
            #endregion
            Regex regex1 = new Regex(pattern1);
            MatchCollection matches1 = regex1.Matches(textOfExercise);
            if (matches1 != null)
            {
                int index = -1;
                if (matches1.Count > 0)
                    index = matches1[0].Index;
                else
                {
                    string pattern = @"\b(הוכח|מצא|חשב)\b";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(textOfExercise);
                    if (match.Success)
                        index = match.Index;
                }
                if (index != -1)
                {   
                    data = textOfExercise.Substring(0, index);
                    proofs = textOfExercise.Substring(index);
                }
            }
            Console.WriteLine("the text divided");


        }

        /// <summary>
        /// מחלקת את הטקסט למשפטים
        /// </summary>
        /// <returns></returns>
        public string[] Dividing_the_text_into_sentences(string myText)
        {
            string[] sentences = myText.Split(new char[] { ',', '.' });
            return sentences;
        }
    }
}