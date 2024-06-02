using MyProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Delegates
{
    internal class createElements
    {
        public delegate Triangle create_triangle();
        public delegate void create_isosceles_triangle();
        public delegate void create_equilateral_triangle();
        public delegate void create_right_triangle();
        public delegate void create_angle();
        public delegate void create_line();
        public delegate void create_middle();
        public delegate void create_plumb();
        public delegate void create_bisects_angle();
        public delegate void create_rib();
        public delegate void create_nichav();
        public delegate void create_yeter();

        public static Triangle func_create_triangle( )
        {
            Triangle triangle = new Triangle();
            triangle.Name = "";
            return triangle;
        }
        create_triangle handler=func_create_triangle;


    }
}
