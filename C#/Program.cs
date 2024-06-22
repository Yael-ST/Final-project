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


class Program
{
    Relation angRelation = new Relation();

    static void Main(string[] args)
    {
        // הגדרת הנתיב המלא לפייתון
        string pythonPath = @"C:\ProgramData\anaconda3\python.exe";

        // יצירת התהליך להרצת הפייתון
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = pythonPath;  // הנתיב המלא לפייתון
        start.FileName = "python";  // ודא כי 'python' נמצא ב-path
        start.Arguments = "\"C:\\Users\\win 10\\Desktop\\יעלצוווק\\python.py\"";  // הנתיב לסקריפט הפייתון שלך במרכאות
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.RedirectStandardError = true;  // זה יכול לעזור בלכידת הודעות שגיאה

        // יצירת התהליך והרצתו
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
            }
            // לקרוא את הודעות השגיאה
            using (StreamReader errorReader = process.StandardError)
            {
                string error = errorReader.ReadToEnd();
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error: " + error);
                }
            }
        }
    }
}


//        // יצירת מנוע פייתון
//        ScriptEngine engine = Python.CreateEngine();

//        // הוספת נתיבים לספריות פייתון
//        ICollection<string> searchPaths = engine.GetSearchPaths();
//        searchPaths.Add(@"C:\Users\win 10\AppData\Local\Programs\Python\Python38\Lib\site-packages");
//        engine.SetSearchPaths(searchPaths);

//        string pythonCode = @"
//import pytesseract
//pytesseract.pytesseract.tesseract_cmd = r'C:\Program Files\Tesseract-OCR\tesseract.exe'
//import os
//os.environ['TESSDATA_PREFIX'] = r'C:\Program Files\Tesseract-OCR\tessdata'
//from PIL import Image

//def pytesseract_function():
//    image = Image.open(r'C:\Users\win 10\Desktop\my-project\pytesseract\problem_Heb.png')
//    text = pytesseract.image_to_string(image, lang='heb+eng')
//    return text

//result = pytesseract_function()
//";

//        // יצירת סביבה להרצת הקוד
//        ScriptScope scope = engine.CreateScope();

//        // הרצת הקוד
//        engine.Execute(pythonCode, scope);

//        // קבלת תוצאה
//        dynamic result = scope.GetVariable("result");
//        Console.WriteLine(result);



/*

   IronOcr.License.LicenseKey = "IRONSUITE.YAEL.YY.100.GMAIL.COM.22855-9D15FEE2B6-AV3SO-C7A7VNTJPB3I-SMJAYAWZT524-5P5LDVZ4M2YO-2HNDFCLGNRWR-UGZ2DRFWXH3S-NQNWVDYTC2XR-MK7U6Q-T4RIH2XYHRWMUA-DEPLOYMENT.TRIAL-CGVD33.TRIAL.EXPIRES.23.JUN.2024";

   // יצירת אובייקט IronTesseract
   var Ocr = new IronTesseract();

   // יצירת אובייקט OcrInput ושיפור התמונה
   using (var Input = new OcrInput())
   {
       // טעינת התמונה
       Input.AddImage(@"C:\Users\win 10\Desktop\my-project\MyProject\img\trial.png");

       // שיפור התמונה לפני OCR
       Input.DeNoise(); // הסרת רעשים
       Input.Deskew();  // סידור הטקסט בצורה אופקית
       Input.Invert();  // הפיכת צבעים (שחור ללבן ולהיפך), אם יש צורך
       Input.ToGrayScale(); // המרת התמונה לשחור-לבן
       Ocr.Language = OcrLanguage.Hebrew;
       Ocr.AddSecondaryLanguage(OcrLanguage.English);
       // ביצוע OCR על התמונה המשופרת
       var Result = Ocr.Read(Input);

       Console.WriteLine(Result.Text);
       string result = Result.Text;
       result=result.Replace(Result.Text[12].ToString(), "");
       Console.WriteLine(result);
       Console.WriteLine(Result.Text[12]);
       /*

       IronOCR ocr = new IronOCR();
       textAnalysis ta = new textAnalysis();
       ta.textOfExercise = ocr.extract_text_from_img();

       //מציאת מילות מפתח בטקסט
       MatchCollection matches = ta.find_key_words(ta.textOfExercise);
       */


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


