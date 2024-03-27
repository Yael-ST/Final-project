using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class EquilateralTriangle :Triangle
    {
        public EquilateralTriangle()
        {
            foreach(Angle angle in this.Angles) //במשו"צ כל הזויות שוות 60
            {
                angle.ValueAngle = 60;
            }
        }    
    }
}
