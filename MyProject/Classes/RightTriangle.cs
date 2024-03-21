using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class RightTriangle : Triangle
    {
        public RightTriangle(string nameTringle, Side[] sides, Angle[] angles, List<Line> moreLines, List<Angle> moreAngles) : base(nameTringle, sides, angles, moreLines, moreAngles)
        {

        }
    }
}
