using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class Line: Element, IRelated
    {
        private string nameLine;
        private double lenLine = 0;
        private string nameTriangle;

        public string NameTriangle { get => nameTriangle; set => nameTriangle = value; }
        public string NameLine { get => nameLine; set => nameLine = value; }
        public double LenLine { get => lenLine; set => lenLine = value; }

        public Line() { }
        

    }
}
