using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class RightTriangle : Triangle
    {
        public RightTriangle(string nameTringle, Rib[] ribs, Angle[] angles, List<Line> moreLines, List<Angle> moreAngles) : base(nameTringle, ribs, angles, moreLines, moreAngles)
        {
            angles[0].ValueAngle = 90;

        }
        public double Finding_Length_rib_By_Pythagorean_Theorem;//משפט פיתגורס

    }
}
