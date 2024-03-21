using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class Side : Line
    {
        private int codeTriangle;//אם זה בלי דאטה בייס לא צריך קוד אלא שם?
        private DescriptionSide DescriptionSide;

        public int CodeTriangle { get => codeTriangle; set => codeTriangle = value; }
        internal DescriptionSide DescriptionSide1 { get => DescriptionSide; set => DescriptionSide = value; }
    }
}
