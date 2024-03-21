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
        private string nameTriangle;//אם זה בלי דאטה בייס לא צריך קוד אלא שם?
        private DescriptionRib descriptionRib;

        public string NameTriangle { get => nameTriangle; set => nameTriangle = value; }
        internal DescriptionRib DescriptionRib { get => descriptionRib; set => descriptionRib = value; }
    }
}
