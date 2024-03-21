using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    [Flags]
    internal enum DescriptionLine
    {
        simple =2,
        middle=4,
        bisectsAngle=8,
        plumb=16
    }
}
