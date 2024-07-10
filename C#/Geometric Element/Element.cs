namespace MyProject.Classes
{
    internal class Element
    {
        public List<Relation> ListAllRelations { get; set; }
        public List<Relation> listAllParallel {  get; set; }
        //public solving solving;
        public Element()
        {          
        }

        /// <summary>
        /// מחזירה רשימה של כל היחסים שקשורים לאוביקט הנוכחי
        /// </summary>
        /// <returns></returns>
        public List<(IRelated, double)> GetMyRelations()
        {
            var resultList = ListAllRelations.Where(p => p.obj2 == this ).Select(a => (a.obj1, a.relation)).ToList();
            resultList.AddRange(ListAllRelations.Where(p => p.obj1 == this).Select(a => (a.obj2, 1 / a.relation)));
            return resultList;
        }
        /// <summary>
        /// בדיקת אם שני אלמנטים שווים
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element1"></param>
        /// <param name="element2"></param>
        /// <returns></returns>
        public bool Is_equal<T>(T element1,T element2) where T:Element
        {
            var thisRelation = (element1 as IRelated, 1);
            if (!element2.GetMyRelations().Contains(thisRelation!))        
                return true;           
            return false;
        }
        /// <summary>
        /// מחזירה רשימה של כל הקווים שמקבילים לאוביקט הנוכחי
        /// </summary>
        /// <returns></returns>
        public List<Relation> GetMyParallel()
        {
            var resultList = listAllParallel.Where(p => p.obj1 == this||p.obj2==this).ToList();
            return resultList;
        }
    }
}