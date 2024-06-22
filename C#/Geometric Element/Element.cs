namespace MyProject.Classes
{
    internal class Element
    {
        public List<Relation> ListAllRelations { get; set; }
        public Element()
        {
            
        }
        //פונקצייה שמחזירה רשימה של כל היחסים שקשורים לאוביקט הנוכחי

        public List<(IRelated, double)> GetMyRelations()
        {
            var resultList = ListAllRelations.Where(p => p.obj2 == this).Select(a => (a.obj1, a.relation)).ToList();
            resultList.AddRange(ListAllRelations.Where(p => p.obj1 == this).Select(a => (a.obj2, 1 / a.relation)));
            return resultList;
        }
    }
}