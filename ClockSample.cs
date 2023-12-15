﻿using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Globalization;

namespace PdfGenerator;

public class ClockSample
{
    DateTime time; 
    public PdfDocument LoadClock()
    {
        // Create new PDF document
        PdfDocument document = new PdfDocument();
        this.time = document.Info.CreationDate;
        document.Info.Title = "PDFsharp Clock Demo";
        document.Info.Author = "Stefan Lange";
        document.Info.Subject = "Server time: " +
          this.time.ToString("F", CultureInfo.InvariantCulture);

        // Create new page
        PdfPage page = document.AddPage();
        page.Width = XUnit.FromMillimeter(200);
        page.Height = XUnit.FromMillimeter(200);

        // Create graphics object and draw clock
        XGraphics gfx = XGraphics.FromPdfPage(page);
        RenderClock(gfx);

        // Send PDF to browser
        //MemoryStream stream = new MemoryStream();
        //document.Save(stream, false);
        //Response.Clear();
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-length", stream.Length.ToString());
        //Response.BinaryWrite(stream.ToArray());
        //Response.Flush();
        //stream.Close();
        //Response.End();

        return document;
    }

    void RenderClock(XGraphics gfx)
    {
        // Clocks should always look happy on hardcopies...
        //this.time = new DateTime(2005, 1, 1, 11, 6, 22, 500);

        XColor strokeColor = XColors.DarkBlue;
        XColor fillColor = XColors.DarkOrange;

        XPen pen = new XPen(strokeColor, 5);
        XBrush brush = new XSolidBrush(fillColor);

        strokeColor.A = 0.8;
        fillColor.A = 0.8;
        XPen handPen = new XPen(strokeColor, 5);
        XBrush handBrush = new XSolidBrush(fillColor);

        DrawText(gfx, pen, brush);

        double width = gfx.PageSize.Width;
        double height = gfx.PageSize.Height;
        gfx.TranslateTransform(width / 2, height / 2);
        double scale = Math.Min(width, height);
        gfx.ScaleTransform(scale / 2000);

        DrawFace(gfx, pen, brush);
        DrawHourHand(gfx, handPen, handBrush);
        DrawMinuteHand(gfx, handPen, handBrush);
        DrawSecondHand(gfx, new XPen(XColors.Red, 7));
    }

    static void DrawText(XGraphics gfx, XPen pen, XBrush brush)
    {
        XSize size = gfx.PageSize;
        XGraphicsPath path = new XGraphicsPath();
        path.AddString("PDFsharp",
          new XFontFamily("Arial"), XFontStyleEx.BoldItalic, 60,
          new XRect(0, size.Height / 3.5, size.Width, 0), XStringFormats.Center);
        gfx.DrawPath(new XPen(pen.Color, 3), brush, path);
    }

    static void DrawFace(XGraphics gfx, XPen pen, XBrush brush)
    {
        for (int i = 0; i < 60; i++)
        {
            int size = i % 5 == 0 ? 100 : 30;
            gfx.DrawEllipse(pen, brush, 0 - size / 2, -900 - size / 2, size, size);
            gfx.RotateTransform(6);
        }
    }

    void DrawHourHand(XGraphics gfx, XPen pen, XBrush brush)
    {
        XGraphicsState gs = gfx.Save();
        gfx.RotateTransform(360 * time.Hour / 12 + 30 * time.Minute / 60);
        gfx.DrawPolygon(pen, brush,
          new XPoint[]{new XPoint(0,  150), new XPoint(100, 0),
                 new XPoint(0, -600), new XPoint(-100, 0)},
          XFillMode.Winding);
        gfx.Restore(gs);
    }

    void DrawMinuteHand(XGraphics gfx, XPen pen, XBrush brush)
    {
        XGraphicsState gs = gfx.Save();
        gfx.RotateTransform(360 * time.Minute / 60 + 6 * time.Second / 60);

        gfx.DrawPolygon(pen, brush,
          new XPoint[]{new XPoint(0,  200), new XPoint(50, 0),
                 new XPoint(0, -800), new XPoint(-50, 0)},
          XFillMode.Winding);
        gfx.Restore(gs);
    }

    void DrawSecondHand(XGraphics gfx, XPen pen)
    {
        XGraphicsState gs = gfx.Save();

        gfx.RotateTransform(360 * time.Second / 60 + 6 * time.Millisecond / 1000);

        gfx.DrawEllipse(new XSolidBrush(pen.Color), -15, -15, 30, 30);
        gfx.DrawLine(pen, 0, 40, 0, -800);
        gfx.Restore(gs);
    }
}
