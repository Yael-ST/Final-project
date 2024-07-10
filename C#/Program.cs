using MyProject.Classes;
using Tesseract;
using IronOcr;
using MyProject;
using System.Text.RegularExpressions;
using MyProject.Text_Of_Exercise;
using System.Collections.Generic;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Diagnostics;
using System.IO;
using Python.Runtime;
using System.Text;

class Program
{
    static void Main()
    {
        IronOCR ocr = new IronOCR();
        TextAnalysis textAnalysis = new TextAnalysis();
        Solving solving = new Solving();

        //string text_of_exercise = ocr.extract_text_from_img();
         string text_of_exercise = "נתון משולש שווה שוקיים ABC . אורך התיכון AD לבסיס BC שווה 7. BC=5. חשב את היקף המשולש. ";
        Console.WriteLine(text_of_exercise);
        textAnalysis.textOfExercise = text_of_exercise;
        textAnalysis.Scan_the_data();
        solving.Solver();
    }
}



/*
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
*/


