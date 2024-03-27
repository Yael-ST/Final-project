using MyProject.Classes;

Relation angRelation = new Relation();

void Solve()
{
    //רשימה של זוויות
    List<Angle> angles = new List<Angle>();
    //רשימה של ישרים
    List<Line> lines = new List<Line>();

    List<Relation> relations = new List<Relation>();

    relations.Add(angRelation);
    relations.Add(angRelation);
    relations.Add(angRelation);

    Line l = new Line();
    var r = l.GetMyRelations();

    EquilateralTriangle tr= new EquilateralTriangle() { ListAllRelations=relations  };

    Common.Relations.Add(null);


}