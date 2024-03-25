using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class EquilateralTriangle :Triangle
    {
        public EquilateralTriangle(string nameTringle, Rib[] ribs, Angle[] angles, List<Line> moreLines, List<Angle> moreAngles):base(nameTringle, ribs, angles, moreLines, moreAngles)      
        {
            foreach(Angle angle in angles) //במשו"צ כל הזויות שוות 60
            {
                angle.ValueAngle = 60;
            }
        }    
    }
}
