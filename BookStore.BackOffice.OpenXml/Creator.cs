using System;
using System.IO;
using BookStore.BackOffice.OpenXml.Abstract;
using BookStore.BackOffice.OpenXml.Concrete;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace BookStore.BackOffice.OpenXml
{
    public class Creator
    { 
        public static void CreateWord()
        {
            ICreator wordCreator = new WordCreator();
            wordCreator.Create();
        }

        public static void CreatePdf()
        {
            ICreator pdfCreator = new PdfCreator();
            pdfCreator.Create();
        }
    }
}