using IronXL;
using MyProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyProject.Text_Of_Exercise
{
    delegate void create_geometric_sentence(string sentence, int index);

    internal class Text_of_proofs : TextAnalysis 
    {
        public Dictionary<string, create_geometric_sentence> key_words_of_proofs_dictionary;

        public Text_of_proofs()
        {
            key_words_of_proofs_dictionary = new Dictionary<string, create_geometric_sentence>();

        }
        /// <summary>
        /// בונה את המילון של מילות המפתח
        /// </summary>
        public void Build_dictionary_key_words_of_proofs()
        {
            WorkSheet key_words = Uploading_an_Excel_file();
            key_words_of_proofs_dictionary.Add(key_words["A19"].ToString(), create_find_perimeter);
            Console.WriteLine("the key words proofs dictionary is built");
        }

        /// <summary>
        ///יצירת אוביקט לפי מילת מפתח
        /// </summary>
        public void Scan_the_proofs()
        {
            Build_dictionary_key_words_of_proofs();

            string[] sentences = Dividing_the_text_into_sentences(proofs);
            for (int i = 0; i < sentences.Length; i++)
            {
                if (sentences[i] != " ")
                {
                    Console.WriteLine(sentences[i]);
                    //מעבר על כל מילות המפתח
                    foreach (var word in this.key_words_of_proofs_dictionary)
                    {
                        string pattern = word.Key;
                        create_geometric_sentence action = word.Value;
                        Match match = Regex.Match(sentences[i], pattern);
                        if (match.Success)
                        {
                            // אם נמצאה התאמה, הפעלת הפונקציה המתאימה
                            action(sentences[i], match.Index);
                        }
                    }
                }
            }
        }

        Sentence the_class_sentence = new Sentence();


        public void create_find_perimeter(string sentence, int index)
        {
            string nameShape = GetNameOfShape(sentence);
            Shape shape = GlobalVariable.shapes.FirstOrDefault(x => x.Name == nameShape)!;
            GlobalVariable.final_sentence = the_class_sentence.Create_sentence_find_Perimeter(shape);
            Console.WriteLine("the final sentence is built. " + shape.Name);
        }

        public string GetNameOfShape(string sentence)
        {
            string pattern = @"\b[A-Z]{3,4}\b";

            Match match = Regex.Match(sentence, pattern);
            return match.Value;

        }
    }
}
