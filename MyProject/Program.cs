using MyProject.Classes;
using Tesseract;
using IronOcr;
using MyProject;
using System.Text.RegularExpressions;
using MyProject.Text_Of_Exercise;
using System.Collections.Generic;


class Program
{
    Relation angRelation = new Relation();

    static void Main(string[] args)
    {

        IronOCR ocr = new IronOCR();
        Class1 class1 = new Class1();
        //חילוץ טקסט מתמונה
        string textOfExercise = ocr.extract_text_from_img();
        //מציאת מילות מפתח בטקסט
        MatchCollection matches = class1.find_key_words(textOfExercise);
    } 


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

        EquilateralTriangle tr = new EquilateralTriangle() { ListAllRelations = relations };

        Common.Relations.Add(null);
    }
}