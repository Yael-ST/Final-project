using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class Shape:Element
    {
        private string name;
        private Rib[] ribs;
        private Angle[] angles;
        private List<LineInTriangle> moreLines;
        private List<Angle> moreAngles;

        internal Angle[] Angles { get => angles; set => angles = value; }
        internal List<Angle> MoreAngles { get => moreAngles; set => moreAngles = value; }
        public Rib[] Ribs { get => ribs; set => ribs = value; }
        internal List<LineInTriangle> MoreLines { get => moreLines; set => moreLines = value; }
        public string Name { get => name; set => name = value; }

        public Shape()
        {
        }
        public void sortNameShape()
        {
            this.name = new string(this.name.Where(char.IsLetter).OrderBy(char.ToLower).ToArray());

        }
    }
}
