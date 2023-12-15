using PdfSharp;
using PdfSharp.Pdf;
using MigraDoc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.IO;
using MigraDoc.DocumentObjectModel.Fields;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Visitors;
using System.Xml.XPath;

namespace PdfGenerator;

public class DocManager
{
    public Color TableBlue = Color.FromRgb(0, 0, 255);
    public Color TableGray = Color.FromRgb(128, 128, 128);
    public Color TableBorder = Color.FromRgb(64, 64, 64);

    public Document document;
    public TextFrame addressFrame;
    public Table table;
    //public Navigator navigator;

    public Document CreateDocument()
    {
        // Create a new MigraDoc document
        this.document = new Document();
        this.document.Info.Title = "A sample invoice";
        this.document.Info.Subject = "Demonstrates how to create an invoice.";
        this.document.Info.Author = "Stefan Lange";

        DefineStyles();

        CreatePage();

        //FillContent();

        return this.document;
    }

    void DefineStyles()
    {
        // Get the predefined style Normal.
        Style style = this.document.Styles["Normal"];
        // Because all styles are derived from Normal, the next line changes the 
        // font of the whole document. Or, more exactly, it changes the font of
        // all styles and paragraphs that do not redefine the font.
        style.Font.Name = "Arial";

        style = this.document.Styles[StyleNames.Header];
        style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

        style = this.document.Styles[StyleNames.Footer];
        style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

        // Create a new style called Table based on style Normal
        style = this.document.Styles.AddStyle("Table", "Normal");
        style.Font.Name = "Arial";
        //style.Font.Name = "Times New Roman";
        style.Font.Size = 9;

        // Create a new style called Reference based on style Normal
        style = this.document.Styles.AddStyle("Reference", "Normal");
        style.ParagraphFormat.SpaceBefore = "5mm";
        style.ParagraphFormat.SpaceAfter = "5mm";
        style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
    }

    void CreatePage()
    {
        // Each MigraDoc document needs at least one section.
        Section section = this.document.AddSection();

        // Put a logo in the header
        var imgaeFile = General.ProjectPath + "Images//gespraech.jpg";
        Image image = section.Headers.Primary.AddImage(imgaeFile);
        image.Height = "2.5cm";
        image.LockAspectRatio = true;
        image.RelativeVertical = RelativeVertical.Line;
        image.RelativeHorizontal = RelativeHorizontal.Margin;
        image.Top = ShapePosition.Top;
        image.Left = ShapePosition.Right;
        image.WrapFormat.Style = WrapStyle.Through;

        // Create footer
        Paragraph paragraph = section.Footers.Primary.AddParagraph();
        paragraph.AddText("PowerBooks Inc · Sample Street 42 · 56789 Cologne · Germany");
        paragraph.Format.Font.Size = 9;
        paragraph.Format.Alignment = ParagraphAlignment.Center;

        // Create the text frame for the address
        this.addressFrame = section.AddTextFrame();
        this.addressFrame.Height = "3.0cm";
        this.addressFrame.Width = "7.0cm";
        this.addressFrame.Left = ShapePosition.Left;
        this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
        this.addressFrame.Top = "5.0cm";
        this.addressFrame.RelativeVertical = RelativeVertical.Page;

        // Put sender in address frame
        paragraph = this.addressFrame.AddParagraph("PowerBooks Inc · Sample Street 42 · 56789 Cologne");
        //paragraph.Format.Font.Name = "Times New Roman";
        paragraph.Format.Font.Name = "Arial";
        paragraph.Format.Font.Size = 7;
        paragraph.Format.SpaceAfter = 3;

        // Add the print date field
        paragraph = section.AddParagraph();
        paragraph.Format.SpaceBefore = "8cm";
        paragraph.Style = "Reference";
        paragraph.AddFormattedText("INVOICE", TextFormat.Bold);
        paragraph.AddTab();
        paragraph.AddText("Cologne, ");
        paragraph.AddDateField("dd.MM.yyyy");

        // Create the item table
        this.table = section.AddTable();
        this.table.Style = "Table";
        this.table.Borders.Color = TableBorder;
        this.table.Borders.Width = 0.25;
        this.table.Borders.Left.Width = 0.5;
        this.table.Borders.Right.Width = 0.5;
        this.table.Rows.LeftIndent = 0;

        // Before you can add a row, you must define the columns
        Column column = this.table.AddColumn("1cm");
        column.Format.Alignment = ParagraphAlignment.Center;

        column = this.table.AddColumn("2.5cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = this.table.AddColumn("3cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = this.table.AddColumn("3.5cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = this.table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Center;

        column = this.table.AddColumn("4cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        // Create the header of the table
        Row row = table.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Center;
        row.Format.Font.Bold = true;
        row.Shading.Color = TableBlue;
        row.Cells[0].AddParagraph("Item");
        row.Cells[0].Format.Font.Bold = false;
        row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[0].MergeDown = 1;
        row.Cells[1].AddParagraph("Title and Author");
        row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[1].MergeRight = 3;
        row.Cells[5].AddParagraph("Extended Price");
        row.Cells[5].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[5].MergeDown = 1;

        row = table.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Center;
        row.Format.Font.Bold = true;
        row.Shading.Color = TableBlue;
        row.Cells[1].AddParagraph("Quantity");
        row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[2].AddParagraph("Unit Price");
        row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[3].AddParagraph("Discount (%)");
        row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[4].AddParagraph("Taxable");
        row.Cells[4].Format.Alignment = ParagraphAlignment.Left;

        this.table.SetEdge(0, 0, 6, 2, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
    }

}
