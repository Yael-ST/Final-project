using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class Angle : Element, IRelated
    {
        private string nameAngle;
        private Rib rib1;
        private Rib rib2;
        private double valueAngle=0;

        public string NameAngle { get => nameAngle; set => nameAngle = value; }
        public double ValueAngle { get => valueAngle; set => valueAngle = value; }
        internal Rib Rib1 { get => rib1; set => rib1 = value; }
        internal Rib Rib2 { get => rib2; set => rib2 = value; }

        public Angle() 
        {

        }  
        public void sortNameAngle()
        {
            nameAngle = nameAngle[0] > nameAngle[2] ? string.Concat(nameAngle[2], nameAngle[1], nameAngle[0]) : nameAngle;
        }
    }
}
