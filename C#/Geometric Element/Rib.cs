using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class Rib : Line
    {
        private DescriptionRib descriptionRib;

        internal DescriptionRib DescriptionRib { get => descriptionRib; set => descriptionRib = value; }

        public Rib() { }
    }
}
