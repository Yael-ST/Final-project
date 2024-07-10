using IronSoftware.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    internal class Solving
    {
        public void Solver()
        {
            // סריקה של העץ
            foreach (Sentence sentence in GlobalVariable.relevant_sentences)
            {
                GlobalVariable.callStack = new Stack<Sentence>();
                ExploreTree(sentence);
            }
        }

        public static void ExploreTree(Sentence sentence)
        {
            // הוספת המשפט למחסנית
            GlobalVariable.callStack.Push(sentence);

            // זימון הפונקציה של המשפט הנוכחי
            sentence.FunctionPointer.DynamicInvoke(sentence.GeometricElement, GlobalVariable.callStack);

            // סריקה על כל תתי המשפטים
            for (int i = 0; i < sentence.myChildren.Count; i++)
            {
                ExploreTree(sentence.myChildren[i]);
            }
            // הסרת המשפט מהמחסנית לאחר הסריקה
            GlobalVariable.callStack.Pop();
        }

    }
}
