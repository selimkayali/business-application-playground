using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using BookStore.BackOffice.WebApi.Business.Abstract;
using BookStore.BackOffice.WebApi.Dto;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BookStore.BackOffice.WebApi.Business.Concrete
{
    public class CreatorService : ICreatorService
    {
        private readonly IBookService _bookService;

        public void CreateWord(IEnumerable<BookDto> bookList)
        {
//            Creator.CreateWord(bookList);


            using (MemoryStream mem = new MemoryStream())
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(mem,
                    WordprocessingDocumentType.Document, true))
                {
                    wordDoc.AddMainDocumentPart();
                    // siga a ordem
                    Document doc = new Document();
                    Body body = new Body();
                    
                    // Create an empty table.
                    Table table = new Table();

                    // Create a TableProperties object and specify its border information.
                    TableProperties tblProp = new TableProperties(
                        new TableBorders(
                            new TopBorder()
                            {
                                Val =
                                    new EnumValue<BorderValues>(BorderValues.Dashed),
                                Size = 15
                            },
                            new BottomBorder()
                            {
                                Val =
                                    new EnumValue<BorderValues>(BorderValues.Dashed),
                                Size = 15
                            },
                            new LeftBorder()
                            {
                                Val =
                                    new EnumValue<BorderValues>(BorderValues.Dashed),
                                Size = 15
                            },
                            new RightBorder()
                            {
                                Val =
                                    new EnumValue<BorderValues>(BorderValues.Dashed),
                                Size = 15
                            },
                            new InsideHorizontalBorder()
                            {
                                Val =
                                    new EnumValue<BorderValues>(BorderValues.Dashed),
                                Size = 15
                            },
                            new InsideVerticalBorder()
                            {
                                Val =
                                    new EnumValue<BorderValues>(BorderValues.Dashed),
                                Size = 15
                            }
                        )
                    );

                    // Append the TableProperties object to the empty table.
                    table.AppendChild<TableProperties>(tblProp);

                    for (int i = 0; i < 3; i++)
                    {
                        // Create a row.
                        TableRow tr = new TableRow();

                        // Create a cell.
                        TableCell tc1 = new TableCell();

                        // Specify the width property of the table cell.
                        tc1.Append(new TableCellProperties(
                            new TableCellWidth() {Type = TableWidthUnitValues.Auto, Width = "5000"}));

                        // Specify the table cell content.
                        tc1.Append(new Paragraph(new Run(new Text("some text what the hell is this?"))));

                        // Append the table cell to the table row.
                        tr.Append(tc1);

//                    // Create a second table cell by copying the OuterXml value of the first table cell.
//                    TableCell tc2 = new TableCell(tc1.OuterXml);
//
//                    // Append the table cell to the table row.
//                    tr.Append(tc2);

                        // Append the table row to the table.
                        table.Append(tr);
                    }


                    
                    body.Append(table);

                    doc.Append(body);

                    
                    wordDoc.MainDocumentPart.Document = doc;
                   

                    wordDoc.Close();
                }

                //return File(mem.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ABC.docx");
                File.WriteAllBytes("/Users/selim.kayali/Desktop/abc.docx", mem.ToArray());
            }
        }

        public void CreatePdf(IEnumerable<BookDto> bookList)
        {
            //@TODO
            //Madafa.CreatePdf();
        }
    }
}