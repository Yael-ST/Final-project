using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class LineInTriangle : Line
    {
        private DescriptionLine descriptionLine;
        private Rib ribDest;
        private string nameAngleSource;
        private char vertexSource;
        private char cutPoint;

        internal Rib RibDest { get => ribDest; set => ribDest = value; }
        internal DescriptionLine DescriptionLine { get => descriptionLine; set => descriptionLine = value; }
        public char CutPoint { get => cutPoint; set => cutPoint = value; }
        public char VertexSource { get => vertexSource; set => vertexSource = value; }
        public string NameAngleSource { get => nameAngleSource; set => nameAngleSource = value; }

        public LineInTriangle() 
        {

        }
    }
}
