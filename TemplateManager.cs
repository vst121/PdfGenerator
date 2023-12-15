using MigraDoc.DocumentObjectModel;
using System.Xml.XPath;

namespace PdfGenerator;

public class TemplateManager
{
    void FillContent()
    {
        //if ( !objPdfSharpDocument.AcroForm.Elements.ContainsKey("/NeedAppearances") )
        //{
        //    objPdfSharpDocument.AcroForm.Elements.Add("/NeedAppearances", new PdfSharp.Pdf.PdfBoolean(true));
        //}
        //else 
        //{
        //    objPdfSharpDocument.AcroForm.Elements("/NeedAppearances") = new PdfSharp.Pdf.PdfBoolean(true);
        //}


        //// Fill address in address text frame
        //XPathNavigator item = SelectItem("/invoice/to");
        //Paragraph paragraph = this.addressFrame.AddParagraph();
        //paragraph.AddText(GetValue(item, "name/singleName"));
        //paragraph.AddLineBreak();
        //paragraph.AddText(GetValue(item, "address/line1"));
        //paragraph.AddLineBreak();
        //paragraph.AddText(GetValue(item, "address/postalCode") + " " + GetValue(item, "address/city"));

        //// Iterate the invoice items
        //double totalExtendedPrice = 0;
        //XPathNodeIterator iter = this.navigator.Select("/invoice/items/*");
        //while (iter.MoveNext())
        //{
        //    item = iter.Current;
        //    double quantity = GetValueAsDouble(item, "quantity");
        //    double price = GetValueAsDouble(item, "price");
        //    double discount = GetValueAsDouble(item, "discount");

        //    // Each item fills two rows
        //    Row row1 = this.table.AddRow();
        //    Row row2 = this.table.AddRow();
        //    row1.TopPadding = 1.5;
        //    row1.Cells[0].Shading.Color = TableGray;
        //    row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        //    row1.Cells[0].MergeDown = 1;
        //    row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
        //    row1.Cells[1].MergeRight = 3;
        //    row1.Cells[5].Shading.Color = TableGray;
        //    row1.Cells[5].MergeDown = 1;

        //    row1.Cells[0].AddParagraph(GetValue(item, "itemNumber"));
        //    paragraph = row1.Cells[1].AddParagraph();
        //    paragraph.AddFormattedText(GetValue(item, "title"), TextFormat.Bold);
        //    paragraph.AddFormattedText(" by ", TextFormat.Italic);
        //    paragraph.AddText(GetValue(item, "author"));
        //    row2.Cells[1].AddParagraph(GetValue(item, "quantity"));
        //    row2.Cells[2].AddParagraph(price.ToString("0.00") + " €");
        //    row2.Cells[3].AddParagraph(discount.ToString("0.0"));
        //    row2.Cells[4].AddParagraph();
        //    row2.Cells[5].AddParagraph(price.ToString("0.00"));
        //    double extendedPrice = quantity * price;
        //    extendedPrice = extendedPrice * (100 - discount) / 100;
        //    row1.Cells[5].AddParagraph(extendedPrice.ToString("0.00") + " €");
        //    row1.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
        //    totalExtendedPrice += extendedPrice;

        //    this.table.SetEdge(0, this.table.Rows.Count - 2, 6, 2, Edge.Box, BorderStyle.Single, 0.75);
        //}

        //// Add an invisible row as a space line to the table
        //Row row = this.table.AddRow();
        //row.Borders.Visible = false;

        //// Add the total price row
        //row = this.table.AddRow();
        //row.Cells[0].Borders.Visible = false;
        //row.Cells[0].AddParagraph("Total Price");
        //row.Cells[0].Format.Font.Bold = true;
        //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //row.Cells[0].MergeRight = 4;
        //row.Cells[5].AddParagraph(totalExtendedPrice.ToString("0.00") + " €");

        //// Add the VAT row
        //row = this.table.AddRow();
        //row.Cells[0].Borders.Visible = false;
        //row.Cells[0].AddParagraph("VAT (19%)");
        //row.Cells[0].Format.Font.Bold = true;
        //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //row.Cells[0].MergeRight = 4;
        //row.Cells[5].AddParagraph((0.19 * totalExtendedPrice).ToString("0.00") + " €");

        //// Add the additional fee row
        //row = this.table.AddRow();
        //row.Cells[0].Borders.Visible = false;
        //row.Cells[0].AddParagraph("Shipping and Handling");
        //row.Cells[5].AddParagraph(0.ToString("0.00") + " €");
        //row.Cells[0].Format.Font.Bold = true;
        //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //row.Cells[0].MergeRight = 4;

        //// Add the total due row
        //row = this.table.AddRow();
        //row.Cells[0].AddParagraph("Total Due");
        //row.Cells[0].Borders.Visible = false;
        //row.Cells[0].Format.Font.Bold = true;
        //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //row.Cells[0].MergeRight = 4;
        //totalExtendedPrice += 0.19 * totalExtendedPrice;
        //row.Cells[5].AddParagraph(totalExtendedPrice.ToString("0.00") + " €");

        //// Set the borders of the specified cell range
        //this.table.SetEdge(5, this.table.Rows.Count - 4, 1, 4, Edge.Box, BorderStyle.Single, 0.75);

        //// Add the notes paragraph
        //paragraph = this.document.LastSection.AddParagraph();
        //paragraph.Format.SpaceBefore = "1cm";
        //paragraph.Format.Borders.Width = 0.75;
        //paragraph.Format.Borders.Distance = 3;
        //paragraph.Format.Borders.Color = TableBorder;
        //paragraph.Format.Shading.Color = TableGray;
        //item = SelectItem("/invoice");
        //paragraph.AddText(GetValue(item, "notes"));
    }

}
