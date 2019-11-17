using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using BookStore.BackOffice.WebApi.Business.Abstract;
using BookStore.BackOffice.WebApi.Dto;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.BackOffice.WebApi.Business.Concrete
{
    public class WordReportCreator : ICreatorService
    {
        public FileStreamResult CreateReport(IEnumerable<BookDto> bookList)
        {
            if (bookList == null)
                throw new NullReferenceException();

            var culture = (CultureInfo) CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.CurrencySymbol = "€";
            using (MemoryStream mem = new MemoryStream())
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(mem,
                    WordprocessingDocumentType.Document, true))
                {
                    wordDoc.AddMainDocumentPart();
                    Document doc = new Document();
                    Body body = new Body();

                    // Create an empty table.
                    Table table = new Table();

                    // Append the TableProperties object to the empty table.
                    table.AppendChild(CreateTableProperties());


                    // Create row for title.
                    TableRow tr = new TableRow();
                    tr.Append(CreateCell("Title", true));
                    tr.Append(CreateCell("Author", true));
                    tr.Append(CreateCell("Price", true));
                    tr.Append(CreateCell("Best Seller", true));
                    tr.Append(CreateCell("Availability", true));
                    table.Append(tr);

                    foreach (var book in bookList)
                    {
                        tr = new TableRow();

                        // create data cells.
                        tr.Append(CreateCell(book.Title));

                        tr.Append(CreateCell(book.Author.Firstname + " " + book.Author.Lastname));

                        tr.Append(CreateCell(book.Price.ToString("C", culture)));

                        tr.Append(CreateCell(book.IsBestSeller ? "Bestseller" : "Not Bestseller"));

                        tr.Append(CreateCell(book.AvailableStock > 0
                            ? "Available in stock\n(" + book.AvailableStock + ")"
                            : "Not available in stock"));

                        // Append the table row to the table.
                        table.Append(tr);
                    }

                    body.Append(table);
                    doc.Append(body);
                    wordDoc.MainDocumentPart.Document = doc;
                    wordDoc.Close();
                }

                var ms = new MemoryStream(mem.ToArray());
                return new FileStreamResult(ms,
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                {
                    FileDownloadName = $"BookSearchReport-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm")
                };
            }
        }

        private static TableCell CreateCell(string text, bool? isThead = false)
        {
            var cell = new TableCell();
            var paragraph = new Paragraph(new Run(new Text(text)));
            ParagraphProperties paragraphProperties = new ParagraphProperties();
            TableCellProperties cellProperties = new TableCellProperties();
            cellProperties.AppendChild(new TableCellVerticalAlignment() {Val = TableVerticalAlignmentValues.Center});
            cellProperties.AppendChild(new Justification() {Val = JustificationValues.Center});

            TableCellWidth cellWidth = new TableCellWidth() {Type = TableWidthUnitValues.Auto};
            if (isThead.HasValue && isThead.Value)
                paragraphProperties.AppendChild(new Justification() {Val = JustificationValues.Center});
            else
                paragraphProperties.AppendChild(new Justification() {Val = JustificationValues.Left});
            paragraphProperties.AppendChild(new TextAlignment() {Val = VerticalTextAlignmentValues.Center});
            cell.AppendChild(cellWidth);
            cell.AppendChild(cellProperties);
            cell.AppendChild(paragraphProperties);
            cell.AppendChild(paragraph);
            return cell;
        }

        private TableProperties CreateTableProperties()
        {
            // create properties for table borders.
            return new TableProperties(
                new TableBorders(
                    new TopBorder()
                    {
                        Val =
                            new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 5
                    },
                    new BottomBorder()
                    {
                        Val =
                            new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 5
                    },
                    new LeftBorder()
                    {
                        Val =
                            new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 5
                    },
                    new RightBorder()
                    {
                        Val =
                            new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 5
                    },
                    new InsideHorizontalBorder()
                    {
                        Val =
                            new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 5
                    },
                    new InsideVerticalBorder()
                    {
                        Val =
                            new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 5
                    }
                )
            );
        }
    }
}