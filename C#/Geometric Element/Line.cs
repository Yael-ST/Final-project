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
        private double? lenLine;
        private string nameShape;

        public string NameLine { get => nameLine; set => nameLine = value; }
        public string NameShape { get => nameShape; set => nameShape = value; }
        public double? LenLine { get => lenLine; set => lenLine = value; }

        public Line() { }
        public void sortNameLine()
        {
            this.nameLine = new string(this.nameLine.OrderBy(char.ToLower).ToArray());
        }
    }
}
