using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    internal class Angle
    {
        private int codeAngle;
        private string nameAngle;
        private Side side1;
        private Side side2;
        private double valueAngle;

        public int CodeAngle { get => codeAngle; set => codeAngle = value; }
        public string NameAngle { get => nameAngle; set => nameAngle = value; }
        public double ValueAngle { get => valueAngle; set => valueAngle = value; }
        internal Side Side1 { get => side1; set => side1 = value; }
        internal Side Side2 { get => side2; set => side2 = value; }
    }
}
