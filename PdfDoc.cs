using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;

namespace PdfGenerator;

public class PdfDoc
{
    public void CreatePdf()
    {
        // Read document into memory for modification
        PdfDocument document = PdfReader.Open(General.PdfInputFile);

        PdfDictionary dict = new PdfDictionary(document);
        dict.Elements["/S"] = new PdfName("/GoTo");

        PdfArray array = new PdfArray(document);

        dict.Elements["/D"] = array;

        PdfReference iref = PdfInternals.GetReference(document.Pages[2]);

        array.Elements.Add(iref);

        array.Elements.Add(new PdfName("/FitV"));

        array.Elements.Add(new PdfInteger(-32768));

        document.Internals.AddObject(dict);

        document.Internals.Catalog.Elements["/OpenAction"] =
          PdfInternals.GetReference(dict);
    }
}
