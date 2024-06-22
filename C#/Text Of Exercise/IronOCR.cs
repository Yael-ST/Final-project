using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronOcr;


namespace MyProject
{
    internal class IronOCR
    {
        public string extract_text_from_img()
        {
            var ocr = new IronTesseract();
            using (var ocrInput = new OcrInput())
            {
                ocrInput.LoadImage("C:\\Users\\win 10\\Desktop\\my-project\\MyProject\\img\\someone.jpg");
                ocr.Language = OcrLanguage.Hebrew;
                ocr.AddSecondaryLanguage(OcrLanguage.English);
                ocrInput.DeNoise(); 
                ocrInput.Deskew();  
                ocrInput.Invert();  
                ocrInput.ToGrayScale(); 

                var ocrResult = ocr.Read(ocrInput);
                return ocrResult.Text;
            }
        }
    }
}
