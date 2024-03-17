using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    internal class Side :Line
    {
        private int codeTriangle;
        private DescriptionSide DescriptionSide;

        public int CodeTriangle { get => codeTriangle; set => codeTriangle = value; }
        internal DescriptionSide DescriptionSide1 { get => DescriptionSide; set => DescriptionSide = value; }
    }
}
