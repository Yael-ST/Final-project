using MyProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    static class GlobalVariable
    {
        //רשימות ששומרות את הנתונים ל התרגיל
        public static List<Shape> shapes = new List<Shape>();
        public static List<Line> lines = new List<Line>();
        public static List<Angle> angles = new List<Angle>();

       //יחסים בין 2 אלמנטים
        public static List<Relation> ListAllRelations =new List<Relation>();
        public static List<Relation> listAllParallel =new List<Relation>();

        //רשימות ששומרות את הדרך לפיתרון
        public static List<Sentence> relevant_sentences = new List<Sentence>();
        public static Stack<Sentence> callStack = new Stack<Sentence>();
        public static Sentence final_sentence;
        public static Stack<Sentence> final_path =new Stack<Sentence>();
        public static bool stopAddingSentences = false;
    }
}