using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp;
using System.Diagnostics;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using MigraDoc.DocumentObjectModel;

namespace PdfGenerator;

//PdfSharpUtilities.cs

//*******************************************************************/     

public class PdfSharpUtilities
{
    private double topMargin = 0;
    private double leftMargin = 0;
    private double rightMargin = 0;
    private double bottomMargin = 0;
    private double cm;

    private PdfDocument document;
    private PdfPage page;
    private XGraphics gfx;
    private XFont font;
    private XPen pen;
    private String outputPath;

    public PdfSharpUtilities(String argOutputpath, Boolean argAddMarginGuides = false)
    {
        this.outputPath = argOutputpath;

        //You’ll need a PDF document:
        this.document = new PdfDocument();

        //And you need a page:
        this.page = document.AddPage();
        this.page.Size = PageSize.Letter;

        //Define how much a cm is in document's units
        this.cm = linearInterpolation(0, 0, 27.9, page.Height, 1);
        Console.WriteLine("1 cm:" + cm);

        //Drawing is done with an XGraphics object:

        this.gfx = XGraphics.FromPdfPage(page);

        this.font = new XFont("Arial", 12, XFontStyleEx.Bold);
        this.pen = new XPen(XColors.Black, 0.5);

        //Sugested margins

        topMargin = 2.5 * cm;
        leftMargin = 3 * cm;
        rightMargin = page.Width - (3 * cm);
        bottomMargin = page.Height - (2.5 * cm);

        if (argAddMarginGuides)
        {
            gfx.DrawString("+", font, XBrushes.Black, rightMargin, topMargin);
            gfx.DrawString("+", font, XBrushes.Black, leftMargin, topMargin);
            gfx.DrawString("+", font, XBrushes.Black, rightMargin, bottomMargin);
            gfx.DrawString("+", font, XBrushes.Black, leftMargin, bottomMargin);
        }

        Console.WriteLine("Page Width in cm:" + page.Width * cm);
        Console.WriteLine("Page Height in cm:" + page.Height * cm);

        Console.WriteLine("Top Margin in cm:" + topMargin);
        Console.WriteLine("Left Margin in cm:" + leftMargin);
        Console.WriteLine("Right Margin in cm:" + rightMargin);
        Console.WriteLine("Bottom Margin in cm:" + bottomMargin);
    }

    public void drawTable(double initialPosX, double initialPosY, double width, double height, XBrush xbrush, List<String[]> contents = null)
    {
        drawSquare(new DPoint(initialPosX, initialPosY), width, height, xbrush);

        if (contents == null)
        {
            contents = new List<String[]>();

            contents.Add(new string[] { "Type", "Size", "Weight", "Stock", "Tax", "Price" });
            contents.Add(new string[] { "Obo", "1", "45", "56", "16.00", "6.50" });
            contents.Add(new string[] { "Crotolamo", "2", "72", "63", "16.00", "19.00" });
        }

        int columns = contents[0].Length;
        int rows = contents.Count;

        double distanceBetweenRows = height / rows;
        double distanceBetweenColumns = width / columns;

        /*******************************************************************/
        // Draw the row lines
        /*******************************************************************/

        DPoint pointA = new DPoint(initialPosX, initialPosY);
        DPoint pointB = new DPoint(initialPosX + width, initialPosY);

        for (int i = 0; i <= rows; i++)
        {
            drawLine(pointA, pointB);

            pointA.y = pointA.y + distanceBetweenRows;
            pointB.y = pointB.y + distanceBetweenRows;
        }

        /*******************************************************************/
        // Draw the column lines
        /*******************************************************************/

        pointA = new DPoint(initialPosX, initialPosY);
        pointB = new DPoint(initialPosX, initialPosY + height);

        for (int i = 0; i <= columns; i++)
        {
            drawLine(pointA, pointB);

            pointA.x = pointA.x + distanceBetweenColumns;
            pointB.x = pointB.x + distanceBetweenColumns;
        }

        /*******************************************************************/
        // Insert text corresponding to each cell
        /*******************************************************************/

        pointA = new DPoint(initialPosX, initialPosY);

        foreach (String[] rowDataArray in contents)
        {
            foreach (String cellText in rowDataArray)
            {

                this.gfx.DrawString(cellText, this.font, XBrushes.Black, new XRect(leftMargin + (pointA.x * cm), topMargin + (pointA.y * cm), distanceBetweenColumns * cm, distanceBetweenRows * cm), XStringFormats.Center);

                pointA.x = pointA.x + distanceBetweenColumns;
            }

            pointA.x = initialPosX;
            pointA.y = pointA.y + distanceBetweenRows;
        }
    }

    public void addText(String text, DPoint xyStartingPosition, int size = 12)
    {
        this.gfx.DrawString(text, this.font, XBrushes.Black, leftMargin + (xyStartingPosition.x * cm), topMargin + (xyStartingPosition.y * cm));
    }

    public void drawSquare(DPoint xyStartingPosition, double width, double height, XBrush xbrush)
    {
        Console.WriteLine("Drawing square starting at: " + xyStartingPosition.x + "," + xyStartingPosition.y + " width: " + width + " height: " + height);
        this.gfx.DrawRectangle(xbrush, new XRect(leftMargin + (xyStartingPosition.x * cm), topMargin + (xyStartingPosition.y * cm), (width * cm), (height * cm)));
    }

    public void drawLine(DPoint fromXyPosition, DPoint toXyPosition)
    {
        this.gfx.DrawLine(this.pen, leftMargin + (fromXyPosition.x * cm), topMargin + (fromXyPosition.y * cm), leftMargin + (toXyPosition.x * cm), topMargin + (toXyPosition.y * cm));
    }

    public void saveAndShow(Boolean argShowAfterSaving = true)
    {
        document.Save(this.outputPath);

        //if (argShowAfterSaving)
        //{
        //    Process.Start(this.outputPath);
        //}
    }

    public double linearInterpolation(double x0, double y0, double x1, double y1, double xd)
    {
        /*******************************************************************/
        //
        //  x0          ------->    y0
        //  given x     ------->    what is y?
        //  x1          ------->    y1
        /*******************************************************************/

        return (y0 + ((y1 - y0) * ((xd - x0) / (x1 - x0))));
    }
}

//DPoint.cs

public class DPoint
{
    public double x { get; set; }
    public double y { get; set; }

    public DPoint(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}





