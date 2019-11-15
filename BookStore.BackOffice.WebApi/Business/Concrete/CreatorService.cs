using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using BookStore.BackOffice.WebApi.Business.Abstract;
using BookStore.BackOffice.WebApi.Dto;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.BackOffice.WebApi.Business.Concrete
{
    public class CreatorService : ICreatorService
    {
        private readonly IBookService _bookService;

        public FileStreamResult CreateWord(IEnumerable<BookDto> bookList)
        {
//            Creator.CreateWord(bookList);


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
                    tr.Append(CreateCell("Title"));
                    tr.Append(CreateCell("Author"));
                    tr.Append(CreateCell("Price"));
                    tr.Append(CreateCell("Best Seller"));
                    tr.Append(CreateCell("Availability"));
                    table.Append(tr);

                    foreach (var book in bookList)
                    {
                        tr = new TableRow();

                        // Specify the table cell content.
                        tr.Append(CreateCell(book.Title));

                        tr.Append(CreateCell(book.Author.Firstname + " " + book.Author.Lastname));

                        tr.Append(CreateCell(string.Format("€{0:C}", book.Price)));

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
            //Madafa.CreatePdf();
            return null;
        }

        public TableProperties CreateTableProperties()
        {
            // Create a TableProperties object and specify its border information.
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

        private static TableCell CreateCell(string text)
        {
            var cell = new TableCell();
            var paragraph = new Paragraph(new Run(new Text(text)));
            ParagraphProperties paragraphProperties = new ParagraphProperties();
            paragraphProperties.AppendChild<Justification>(new Justification() {Val = JustificationValues.Center});
            cell.AppendChild<ParagraphProperties>(paragraphProperties);
            cell.AppendChild<Paragraph>(paragraph);
            return cell;
        }
    }
}