using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Drawing;

namespace PdfGenerator;

public class Watermark
{
    private const string watermark = "Air Board";
    public static void AddWatermark()
    {
        PdfDoc outputDoc = new PdfDoc();
        PdfDocument document = PdfReader.Open(General.PdfInputFile);

        AddWatermarkToPage(document.Pages[0]);
        AddWatermarkToPage(document.Pages[document.Pages.Count-1]);

        document.Save(General.Sample_WatermarkOutputFile);
    }
    private static void AddWatermarkToPage(PdfPage page)
    {
        // Variation 1: Draw a watermark as a text string.

        // Get an XGraphics object for drawing beneath the existing content.
        var gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Prepend);

        var font = new XFont("Arial", 80, XFontStyleEx.Bold);

        // Get the size (in points) of the text.
        var size = gfx.MeasureString(watermark, font);

        // Define a rotation transformation at the center of the page.
        gfx.TranslateTransform(page.Width / 2, page.Height / 2);
        gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
        gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

        // Create a string format.
        var format = new XStringFormat();
        format.Alignment = XStringAlignment.Near;
        format.LineAlignment = XLineAlignment.Near;

        // Create a gray brush.
        XBrush brush = new XSolidBrush(XColor.FromArgb(200, 200, 200, 200));

        // Draw the string.
        gfx.DrawString(watermark, font, brush,
        new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2),
        format);

    }
}
