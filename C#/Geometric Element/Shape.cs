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
        private List<Angle> moreAngles;
        private List<LineInShape> moreLines;


        internal Angle[] Angles { get => angles; set => angles = value; }
        internal List<Angle> MoreAngles { get => moreAngles; set => moreAngles = value; }
        public Rib[] Ribs { get => ribs; set => ribs = value; }
        public string Name { get => name; set => name = value; }
        internal List<LineInShape> MoreLines { get => moreLines; set => moreLines = value; }

        public Shape()
        {
        }
        public void sortNameShape()
        {
            this.name = new string(this.name.OrderBy(char.ToLower).ToArray());

        }
        /// <summary>
        /// מציאת היקף
        /// </summary>
        /// <returns></returns>
        public double find_Perimeter()
        {
            double perimeter = 0;
            foreach (Rib rib in this.Ribs)
            {
                perimeter += rib.LenLine??0;
            }
            return perimeter;
        }
    }
}
