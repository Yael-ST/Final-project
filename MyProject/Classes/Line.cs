using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class Line
    {
        private string nameLine;
        private char vertex1;
        private char vertex2;
        private double lenLine;

        public string NameLine { get => nameLine; set => nameLine = value; }
        public char Vertex1 { get => vertex1; set => vertex1 = value; }
        public char Vertex2 { get => vertex2; set => vertex2 = value; }
        public double LenLine { get => lenLine; set => lenLine = value; }


    }
}
