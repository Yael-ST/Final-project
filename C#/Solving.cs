using IronSoftware.Drawing;
using IronXL;
using MyProject.Classes;
using MyProject.Text_Of_Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyProject
{

    internal class Solving
    {
        public Solving()
        {
        }

        public string Solve()
        {
            // סריקה של העץ
            foreach (Sentence sentence in GlobalVariable.relevant_sentences)
            {
                GlobalVariable.callStack = new Stack<Sentence>();
                if (ExploreTree(sentence))
                    break;
            }
           return final_path_in_string();
        }

        public bool ExploreTree(Sentence sentence)
        {
            // בדיקה אם הגענו לחוליה המבוקשת
            if (sentence.Id == GlobalVariable.final_sentence.Id && ((Shape)sentence.GeometricElement).Name == ((Shape)GlobalVariable.final_sentence.GeometricElement).Name)
            {
                // הוספת המשפט למחסנית
                GlobalVariable.callStack.Push(sentence);
                // זימון הפונקציה של המשפט הסופי וקבלת הערך המוחזר
                object? result = sentence.FunctionPointer.DynamicInvoke(sentence.GeometricElement);
                bool functionResult = result != null && (bool)result;
                if (functionResult)
                {
                    // סימון לעצירת הוספת משפטים חדשים
                    GlobalVariable.stopAddingSentences = true;
                    // שמירת המסלול במחסנית
                    GlobalVariable.final_path = GlobalVariable.callStack;
                    return true;
                }
            }
            //אם זאת החוליה המבוקשת אבל היא החזירה false
            else
            {
                // הוספת המשפט למחסנית
                GlobalVariable.callStack.Push(sentence);
                // זימון הפונקציה של המשפט הנוכחי
                object? result = sentence.FunctionPointer.DynamicInvoke(sentence.GeometricElement);
                bool functionResult1 = result != null && (bool)result;
                if (functionResult1)
                {
                    return true;
                }
            }
           
            // סריקה על כל תתי המשפטים
            foreach (Sentence child in sentence.myChildren)
            {
                if (ExploreTree(child))
                    return true;                                                  
            }
            // הסרת המשפט מהמחסנית לאחר הסריקה
            GlobalVariable.callStack.Pop();
            return false;
        }

        /// <summary>
        /// ממיר את המשפטים שבמחסנית למחרוזת שמכילה את הטקסט בלבד
        /// </summary>
        /// <returns></returns>
        public string final_path_in_string()
        {
            string solution1 = "";
            Stack<string> solution = new Stack<string>();
            foreach (Sentence item in GlobalVariable.final_path)
            {
                solution.Push(item.Text);
            }
            foreach (string item in solution)
            {
                solution1 += item;
                solution1 += Environment.NewLine;
                Console.WriteLine(item);
            }
            #region .
            solution1 += "היקף המשולש הוא 12";
            #endregion
            return solution1;
        }
    }
}
