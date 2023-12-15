using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System.Resources;
using PdfSharp.Pdf.AcroForms;

namespace PdfGenerator;

public class ShowPdfDetails
{
    public static void Show()
    {
        PdfDoc outputDoc = new PdfDoc();
        PdfDocument document = PdfReader.Open(General.Sample_TemplateInputFile);
        //PdfDocument document = PdfReader.Open(General.PdfInputFile);

        foreach (var item in document.CustomValues!)
        {
            Console.WriteLine("CustomValues.Key: " + item.Key);
            Console.WriteLine("CustomValues.Value: " + item.Value);
        }

        Console.WriteLine();

        foreach (var item in document.Info)
        {
            Console.WriteLine("Info.Key: " + item.Key);
            Console.WriteLine("Info.Value: " + item.Value);
        }

        Console.WriteLine();

        foreach (var item in document.Outlines)
        {
            Console.WriteLine("Outlines.Title: " + item.Title);
            Console.WriteLine("Outlines.TextColor: " + item.TextColor);
            Console.WriteLine("Outlines.Elements: " + item.Elements);
            Console.WriteLine("Outlines.Elements.KeyNames: " + item.Elements.KeyNames);
            Console.WriteLine("Outlines.HasChildren: " + item.HasChildren);
        }

        Console.WriteLine();

        if (document.AcroForm is not null)
        {
            foreach (var item in document.AcroForm.Elements)
            {
                Console.WriteLine("Elements.Key: " + item.Key);
                Console.WriteLine("Elements.Value: " + item.Value);
            }

            Console.WriteLine();

            foreach (var item in document.AcroForm.Fields)
            {
                Console.WriteLine("Fields.ToString: " + item.ToString());
                Console.WriteLine("Fields.Type.Name: " + item.GetType().Name);
                Console.WriteLine("Fields.Type.FullName: " + item.GetType().FullName);
            }

            Console.WriteLine();



            PdfAcroForm af = document.AcroForm;
            foreach (var item in document.AcroForm.Fields.Names)
            {
                Console.WriteLine("Elements.Fields.Name: " + item);

                var field = af.Fields[item];
                if (field is PdfTextField)
                {
                    //PdfDictionary dict = new PdfDictionary(document);
                    ////PdfArray array = new PdfArray(document);

                    PdfItem val = field;


                    //array.Elements.Add(val);

                    field.Value = new PdfString($"Set AIRboard value for {field.Name}");
                    Guid guid = Guid.NewGuid(); 
                    document.AcroForm.Elements.Add("/"+guid.ToString(), field);

                    //(PdfTextField)field).Text = "AIRboard";
                    //dictPDFFields.Add(key, ((PdfTextField)field).Text);
                }
                //else if (field is PdfCheckBoxField)
                //{
                //    dictPDFFields.Add(key, ((PdfCheckBoxField)field).Checked.ToString());
                //}
            }

            Console.WriteLine();
        }

        foreach (var item in document.Pages)
        {
            Console.WriteLine("Pages.Internals: " + item.Internals);
            Console.WriteLine("Pages.Internals.TypeID: " + item.Internals.TypeID);
            Console.WriteLine("Pages.Internals.ObjectID: " + item.Internals.ObjectID);
            Console.WriteLine("Pages.Internals.GenerationNumber: " + item.Internals.GenerationNumber);
        }

        Console.WriteLine();

        foreach (var item in document.Pages)
        {
            Console.WriteLine("Pages.Internals: " + item.Internals);
            Console.WriteLine("Pages.Internals.TypeID: " + item.Internals.TypeID);

            Console.WriteLine();

            Console.WriteLine("Sub items:");

            foreach (var subItem in item)
            {
                Console.WriteLine("Pages.Internals -> SubItem.Key: " + subItem.Key);
                Console.WriteLine("Pages.Internals -> SubItem.Value: " + subItem.Value);

                //subItem.Value = "T-AIRboard-T";
            }
        }


        //array.Elements.Add(pdfItem);

        // Set the array as the value of key /D.
        // This makes the array a direct object of the dictionary.
        //dict.Elements["/Resources"].SetValue("aksdgaskgkalga");


        document.Save(General.Sample_TemplateOutputFile);
    }
}
