using System;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for input
        Console.Write("Enter client name: ");
        string clientName = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter invoice number: ");
        string invoiceNumber = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter description of the work done: ");
        string description = Console.ReadLine() ?? string.Empty;

        // Generate PDF
        string pdfPath = $"Invoice_{invoiceNumber}.pdf";
        string logoPath = @"assets\img\loguinho-abrev.png";
        CreatePdf(pdfPath, logoPath, clientName, invoiceNumber, description);

        Console.WriteLine($"Invoice generated: {pdfPath}");
    }

    static void CreatePdf(string path, string logoPath, string clientName, string invoiceNumber, string description)
    {
        // Create a new PDF document
        var document = new PdfDocument();

        // Set the document title
        document.Info.Title = $"Invoice number #{invoiceNumber}";

        // Create an empty page
        var page = document.AddPage();

        // Get an XGraphics object for drawing
        var gfx = XGraphics.FromPdfPage(page);
        var logo = XImage.FromFile(logoPath);
        var fontFamily = "Courier New";
        // Draw the image


        // Calculate the position to center the image
        double x = (page.Width.Point - logo.PixelWidth * 72 / logo.HorizontalResolution) / 2;
        double y = 20; // Top margin

        gfx.DrawImage(logo, x, y, logo.PixelWidth * 72 / logo.HorizontalResolution, logo.PixelHeight * 72 / logo.VerticalResolution);
        // Create a font
        var font = new XFont(fontFamily, 16, XFontStyleEx.Regular);

        // Calculate the starting position for the text content
        double textY = y + logo.PixelHeight * 72 / logo.VerticalResolution + 40;

        // Draw the table-like structure
        gfx.DrawString("Invoice Details", font, XBrushes.Black,
            new XRect(0, textY, page.Width.Point, page.Height.Point),
            XStringFormats.TopCenter);

        var smallFont = new XFont(fontFamily, 12, XFontStyleEx.Regular);
        var fontColor = XBrushes.DarkGray;

        gfx.DrawString("Client Name:", smallFont, fontColor,
            new XRect(40, textY + 40, page.Width.Point, page.Height.Point),
            XStringFormats.TopLeft);
        gfx.DrawString(clientName, smallFont, fontColor,
            new XRect(200, textY + 40, page.Width.Point, page.Height.Point),
            XStringFormats.TopLeft);

        gfx.DrawString("Invoice Number:", smallFont, fontColor,
            new XRect(40, textY + 70, page.Width.Point, page.Height.Point),
            XStringFormats.TopLeft);
        gfx.DrawString(invoiceNumber, smallFont, fontColor,
            new XRect(200, textY + 70, page.Width.Point, page.Height.Point),
            XStringFormats.TopLeft);

        gfx.DrawString("Description:", smallFont, fontColor,
            new XRect(40, textY + 100, page.Width.Point, page.Height.Point),
            XStringFormats.TopLeft);
        gfx.DrawString(description, smallFont, fontColor,
            new XRect(200, textY + 100, page.Width.Point, page.Height.Point),
            XStringFormats.TopLeft);

        // Save the document
        document.Save(path);
    }
}