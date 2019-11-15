using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using BookStore.BackOffice.WebApi.Business.Abstract;
using BookStore.BackOffice.WebApi.Dto;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using BottomBorder = DocumentFormat.OpenXml.Wordprocessing.BottomBorder;
using InsideHorizontalBorder = DocumentFormat.OpenXml.Wordprocessing.InsideHorizontalBorder;
using InsideVerticalBorder = DocumentFormat.OpenXml.Wordprocessing.InsideVerticalBorder;
using LeftBorder = DocumentFormat.OpenXml.Wordprocessing.LeftBorder;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using ParagraphProperties = DocumentFormat.OpenXml.Wordprocessing.ParagraphProperties;
using RightBorder = DocumentFormat.OpenXml.Wordprocessing.RightBorder;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using TableCell = DocumentFormat.OpenXml.Wordprocessing.TableCell;
using TableCellProperties = DocumentFormat.OpenXml.Drawing.TableCellProperties;
using TableProperties = DocumentFormat.OpenXml.Wordprocessing.TableProperties;
using TableRow = DocumentFormat.OpenXml.Wordprocessing.TableRow;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using TopBorder = DocumentFormat.OpenXml.Wordprocessing.TopBorder;
using VerticalTextAlignment = DocumentFormat.OpenXml.Wordprocessing.VerticalTextAlignment;

namespace BookStore.BackOffice.WebApi.Business.Concrete
{
    public class CreatorService : ICreatorService
    {
        private readonly IBookService _bookService;

        public FileStreamResult CreateWord(IEnumerable<BookDto> bookList)
        {
            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
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
                    table.AppendChild<TableProperties>(CreateTableProperties());


                    // Create row for title.
                    TableRow tr = new TableRow();
                    tr.Append(CreateCell("Title",true));
                    tr.Append(CreateCell("Author",true));
                    tr.Append(CreateCell("Price",true));
                    tr.Append(CreateCell("Best Seller",true));
                    tr.Append(CreateCell("Availability",true));
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
                return  new FileStreamResult(ms,"application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            }
        }

        public FileStreamResult CreatePdf(IEnumerable<BookDto> bookList)
        {
            //@TODO
            return null;
        }

        public TableProperties CreateTableProperties()
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

        private static TableCell CreateCell(string text, bool? isThead=false)
        {
            var cell = new TableCell();
            var paragraph = new Paragraph(new Run(new Text(text)));
            ParagraphProperties paragraphProperties = new ParagraphProperties();
            TableCellProperties cellProperties = new TableCellProperties();
            cellProperties.AppendChild(new TableCellVerticalAlignment(){Val = TableVerticalAlignmentValues.Center});
            cellProperties.AppendChild(new Justification() {Val = JustificationValues.Center});

            TableCellWidth cellWidth = new TableCellWidth() { Type = TableWidthUnitValues.Auto };
            if(isThead.HasValue && isThead.Value)
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
    }
}