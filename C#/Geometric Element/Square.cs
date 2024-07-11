using MyProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Geometric_Element
{
    internal class Square :Shape
    {
        public Square()
        {
            this.Ribs = new Rib[4];
            this.Angles=new Angle[4];
        }
        /// <summary>
        /// מציאת שטח
        /// </summary>
        /// <returns></returns>
        public double find_Area()
        {
            LineInShape plump = this.MoreLines.First(p => p.DescriptionLine == DescriptionLine.plumb);
            if (plump != null)
                return (plump.RibDest.LenLine  * plump.LenLine);
            return 0;
        }
    }
}
