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
        public static List<Shape> shapes = new List<Shape>();
        public static List<Line> lines = new List<Line>();
        public static List<Angle> angles = new List<Angle>();

        public static Graph graph_of_relevant_sentences = graph_of_relevant_sentences = new Graph();

        public static List<Sentence> relevant_sentences=new List<Sentence>();

        public static Stack<Sentence> callStack=new Stack<Sentence>();
        public static List<Relation> ListAllRelations =new List<Relation>();
        public static List<Relation> listAllParallel =new List<Relation>();

    }
}