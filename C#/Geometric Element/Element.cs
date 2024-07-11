namespace MyProject.Classes
{
    internal class Element
    {
       
        public Element()
        {
        }

        /// <summary>
        /// מחזירה רשימה של כל היחסים שקשורים לאוביקט הנוכחי
        /// </summary>
        /// <returns></returns>
        public List<(IRelated, double)> GetMyRelations()
        {
            var resultList = GlobalVariable. ListAllRelations.Where(p => p.obj2 == this ).Select(a => (a.obj1, a.relation)).ToList();
            resultList.AddRange(GlobalVariable. ListAllRelations.Where(p => p.obj1 == this).Select(a => (a.obj2, 1 / a.relation)));
            return resultList;
        }
        /// <summary>
        /// בדיקה האם שני אלמנטים שווים
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element1"></param>
        /// <param name="element2"></param>
        /// <returns></returns>
        public bool Is_equal<T>(T element1,T element2) where T:Element
        {
            var thisRelation = (element1 as IRelated, 1);
            if (!element2.GetMyRelations().Contains(thisRelation!))        
                return false;           
            return true;
        }

        /// <summary>
        /// אם נמצא יחס חדש על אלמנט, מוסיפים אתו לכל האלמנטים שקשורים אליו
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        public void CompleteAllMyRelations() 
        {
            List<(IRelated, double)> myRelations = this.GetMyRelations();
            foreach ((IRelated, double) item in myRelations)
            {
                if (this is Line)
                    ((Line)item.Item1).LenLine = (this as Line)!.LenLine * item.Item2;
                if (this is Angle)
                    ((Angle)item.Item1).ValueAngle = (this as Angle)!.ValueAngle * item.Item2;
            }
        }
        /// <summary>
        /// מחזירה רשימה של כל הקווים שמקבילים לאוביקט הנוכחי
        /// </summary>
        /// <returns></returns>
        public List<Relation> GetMyParallel()
        {
            var resultList = GlobalVariable. listAllParallel.Where(p => p.obj1 == this||p.obj2==this).ToList();
            return resultList;
        }

    }
}