using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    internal class LineInTriangle : Line
    {
        private DescriptionLine descriptionLine;
        private Side sideDest;
        private Angle angleSource;
        private char cutPoint;

        internal Side SideDest { get => sideDest; set => sideDest = value; }
        internal Angle AngleSource { get => angleSource; set => angleSource = value; }
        internal DescriptionLine DescriptionLine { get => descriptionLine; set => descriptionLine = value; }
        public char CutPoint { get => cutPoint; set => cutPoint = value; }
        


    }
}
