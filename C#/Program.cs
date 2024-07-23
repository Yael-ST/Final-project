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


namespace MyProject;

public class Program
{
    public static void Main()
    {
        Solve();
    }

   
    public static string  Solve()
    {
        IronOCR ocr = new IronOCR();
        TextAnalysis textAnalysis = new TextAnalysis();
        Text_of_Data text_Of_Data = new Text_of_Data();
        Text_of_proofs text_of_proofs = new Text_of_proofs();
        Solving solving = new Solving();

        string text_of_exercise = ocr.extract_text_from_img();
        #region .
        text_of_exercise = "נתון משולש שווה שוקיים ABC (AB=AC). אורך התיכון AD לבסיס BC שווה 4. CD=3 . חשב את היקף המשולש ACD. ";
        #endregion
        text_Of_Data.SetTextOfExercise(text_of_exercise);
        text_Of_Data.Scan_the_data();
        text_of_proofs.Scan_the_proofs();
        return solving.Solve();
    }
}




