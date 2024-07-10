using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Classes
{
    internal class EquilateralTriangle :Triangle
    {
        public EquilateralTriangle()
        {
        }    
        public void Set_attributes_of_the_Equilateral_Triangle()
        {
            //במשו"צ כל הזויות שוות 60
            foreach (Angle angle in this.Angles)
            {
                angle.ValueAngle = 60;
            }

            //כל הצלעות שוות          
            var thisRelation1 = (Ribs[1], 1);
            var thisRelation2 = (Ribs[2], 1);

            if (!Ribs[0].GetMyRelations().Contains(thisRelation1))
            {
                Relation relation1 = new Relation() { obj1 = Ribs[0], obj2 = Ribs[1], relation = 1 };
                this.ListAllRelations.Add(relation1);
            }
            if (!Ribs[0].GetMyRelations().Contains(thisRelation2))
            {
                Relation relation2 = new Relation() { obj1 = Ribs[0], obj2 = Ribs[2], relation = 1 };
                this.ListAllRelations.Add(relation2);
            }
            if (!Ribs[1].GetMyRelations().Contains(thisRelation2))
            {
                Relation relation2 = new Relation() { obj1 = Ribs[1], obj2 = Ribs[2], relation = 1 };
                this.ListAllRelations.Add(relation2);
            }
        }
    }
}