using QuestPDF;
using QuestPDF.Drawing;
using QuestPDF.Elements;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Text;

namespace PdfGenerator.QuestPdf;

internal class QuestPDFGenerator
{
    public void CreatePdf()
    {
        QuestPDF.Settings.License = LicenseType.Community; 
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(14));

                page.Header()
                        .Text("Air Reporting - Draft Version")
                        .SemiBold().FontSize(26)
                        .FontColor(Colors.Blue.Medium);

                page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(15);
                            x.Item().Text("Test data");
                            x.Item().Text("Another test data");
                            x.Item().Image(Placeholders.Image(200, 150));
                        });

                page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
            });
        }).GeneratePdf(General.PdfOutputQuestTestFile);
    }

    private string Bin2Hex(byte[] binary)
    {
        StringBuilder builder = new StringBuilder();
        foreach (byte num in binary)
        {
            builder.AppendFormat("{0:X}", num);
            //if (num > 15)
            //{
            //    builder.AppendFormat("{0:X}", num);
            //}
            //else
            //{
            //    builder.AppendFormat("0{0:X}", num);
            //}
        }
        return builder.ToString();
    }
}
