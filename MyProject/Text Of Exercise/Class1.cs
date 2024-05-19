using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using IronXL;
using System.Collections;



namespace MyProject.Text_Of_Exercise
{
    internal class Class1
    {
 
       public MatchCollection find_key_words(string textOfExercise)
       {
            //העלאת קובץ אקסל
            WorkBook wb = WorkBook.Load("C:\\Users\\win 10\\Desktop\\my-project\\MyProject\\Text Of Exercise\\key_words.xlsx");
            WorkSheet ws = wb.GetWorkSheet("sheet1");

            // בניית ביטוי רגולרי שיכלול את כל מילות המפתח
            string pattern = @"(" + string.Join("|", ws["A1:A12"]) + @")";

            // יצירת אובייקט Regex
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            // חיפוש ההתאמות בטקסט
            MatchCollection matches = regex.Matches(textOfExercise);
            return matches;
        }


        public void build_dictionary()
        {

            Dictionary<string, Delegate> key_words_dictionary = new Dictionary<string,Delegate>();

            key_words_dictionary.Add("משולש", create_triangle);
            key_words_dictionary.Add("משולש שוווה שוקיים", create_isosceles_triangle);
            key_words_dictionary.Add("משולש שוווה צלעות", create_equilateral_triangle);
            key_words_dictionary.Add("משולש ישר זווית", create_right_triangle);
            key_words_dictionary.Add("זווית", create_angle);
            key_words_dictionary.Add("ישר", create_line);
            key_words_dictionary.Add("תיכון", create_middle);
            key_words_dictionary.Add("גובה", create_plumb);
            key_words_dictionary.Add("חוצה זווית", create_bisects_angle);
            key_words_dictionary.Add("צלע", create_rib);
            key_words_dictionary.Add("ניצב", create_nichav);
            key_words_dictionary.Add("יתר", create_yeter);
        }
    }
}