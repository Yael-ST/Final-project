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

        /*var ocr = new IronTesseract();


        IronOcr.License.LicenseKey = "IRONOCR-MYLICENSE-KEY-1EF01";

        using (var ocrInput = new OcrInput())
        {
            //למה הוא לא קולט ניתוב חלקי?
            ocrInput.LoadImage("C:\\Users\\win 10\\Desktop\\my-project\\MyProject\\img\\problem.png");
            ocr.Language = OcrLanguage.Hebrew;
            ocr.AddSecondaryLanguage(OcrLanguage.English);
            var ocrResult = ocr.Read(ocrInput);
            Console.WriteLine(ocrResult.Text);

        }*/



        // הגדרת מפתח הרישיון
        IronOcr.License.LicenseKey = "IRONSUITE.YAEL.YY.100.GMAIL.COM.22855-9D15FEE2B6-AV3SO-C7A7VNTJPB3I-SMJAYAWZT524-5P5LDVZ4M2YO-2HNDFCLGNRWR-UGZ2DRFWXH3S-NQNWVDYTC2XR-MK7U6Q-T4RIH2XYHRWMUA-DEPLOYMENT.TRIAL-CGVD33.TRIAL.EXPIRES.23.JUN.2024";

        var ocr = new IronTesseract();
        using (var Input = new OcrInput(@"C:\Users\win 10\Desktop\my-project\MyProject\img\problem.png"))
        {
            ocr.Language = OcrLanguage.Hebrew;
            ocr.AddSecondaryLanguage(OcrLanguage.English);
            var Result = ocr.Read(Input);
            Console.WriteLine(Result.Text);
        }

        /*

        IronOCR ocr = new IronOCR();
        textAnalysis ta = new textAnalysis();
        ta.textOfExercise = ocr.extract_text_from_img();
        
        //מציאת מילות מפתח בטקסט
        MatchCollection matches = ta.find_key_words(ta.textOfExercise);
        */
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