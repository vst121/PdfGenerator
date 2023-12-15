using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfGenerator.QuestPdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace PdfGenerator;

public static class FileCreationTesting
{
    public static void TestMigra()
    {
        // Create a MigraDoc document
        Document document = MigraDocManager.CreateDocument();

        //string ddl = MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToString(document);
        MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");
        PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
        renderer.Document = document;

        //PlatformFontResolver.ResolveTypeface("Arial", true, true);
        //PlatformFontResolver.ResolveTypeface("Times New Roman", true, true);

        renderer.RenderDocument();

        // Save the document...
        string filename = General.ProjectPath + "OutputFiles\\AIRboardSamplePdfDoc.pdf";
        renderer.PdfDocument.Save(filename);
        // ...and start a viewer.
        //Process.Start(filename);
        renderer.PdfDocument.Close();
    }

    public static void TestPdf()
    {
        PdfSharpUtilities pdf = new PdfSharpUtilities(General.Sample_CreatePdfOutpuFile, true);

        pdf.drawSquare(new DPoint(0, 0), 3, 2, XBrushes.Purple);

        pdf.addText("Username", new DPoint(0, 4.5), 16);

        pdf.addText("Invoice", new DPoint(12.15, 1.5), 14);

        pdf.addText("Account: 69696969", new DPoint(0, 6));

        pdf.addText("Period: 2022-11", new DPoint(0, 7));

        pdf.addText("E-mail: mail@gmail.com", new DPoint(0, 8));

        pdf.addText("Inventory:", new DPoint(0, 10));

        //Example table: to fill with example data leave contents = null
        pdf.drawTable(0, 11, 15.7, 3, XBrushes.LightGray, null);

        pdf.saveAndShow();

    }

    public static void TestClock()
    {
        ClockSample clock = new ClockSample();
        PdfDocument document = clock.LoadClock();

        document.Save(General.Sample_ClockOutpuFile);
    }

    public static void TestWatermark()
    {
        Watermark.AddWatermark();
    }

    public static void TestBooklet()
    {
        Booklet.CreateBooklet();
    }

    public static void TestTextLayout()
    {
        TextLayout.ShowDifferentLayouts();
    }

    public static void TestWorkOnPdfObjects()
    {
        WorkOnPdfObjects.ChangeSomeObject();
    }


    public static void TestShowPdfDetails()
    {
        ShowPdfDetails.Show();
    }

    public static void TestQuestPdf()
    {
        QuestPDFGenerator generator = new QuestPDFGenerator();
        generator.CreatePdf();
    }
}

// Usage example

