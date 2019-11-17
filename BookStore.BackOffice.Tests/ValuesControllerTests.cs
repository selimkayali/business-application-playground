using System;
using System.Collections.Generic;
using BookStore.BackOffice.WebApi.Business.Abstract;
using BookStore.BackOffice.WebApi.Business.Concrete;
using BookStore.BackOffice.WebApi.Controllers;
using BookStore.BackOffice.WebApi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;

namespace BookStore.BackOffice.Tests
{
    public class ValuesControllerTests
    {
        private Mock<IBookService> _bookService;

        private ValuesController _valuesController;

        [Fact]
        public void ValuesController_IfNecessaryDependencyNotInjected_ReturnsNullReferenceException()
        {
            CreateFakeController(true, false);
            Assert.Throws<NullReferenceException>(TestCode);
        }

        [Fact]
        public void ValuesController_GetAction_CheckAtLeastOneQueryParameterNeeded_ReturnsArgumentNullException()
        {
            CreateFakeController(false, true);
            Assert.Throws<ArgumentNullException>(TestCode);
        }
        
        [Fact]
        public void ValuesController_GetAction_CheckAtLeastOneQueryParameterProvided_ReturnsArgumentNullException()
        {
            CreateFakeController(true, true);
            Assert.IsType<FileStreamResult>(TestCode());
        }

        [Fact]
        public void ValuesController_GetAction_CheckRequestInvalidDocumentType_InvalidOperationException()
        {
            CreateFakeController(true, true);
            Assert.Throws<InvalidOperationException>(() => _valuesController.Get(null, null, null, null, 3));
        }

        [Fact]
        public void ValuesController_GetAction_CheckRequestValidDocumentType_ReturnsFileStreamResult()
        {
            CreateFakeController(true, true);
            var result = TestCode();
            Assert.IsType<FileStreamResult>(result);
        }

        [Fact]
        public void WordReportCreator_CreateReport_SendListOfBooks_FileStreamResult()
        {
            var wordReportCreator = new WordReportCreator();
            Assert.IsType<FileStreamResult>(wordReportCreator.CreateReport(GetMockBookList()));
        }
        
        [Fact]
        public void WordReportCreator_CreateReport_SendNull_NullReferenceException()
        {
            var wordReportCreator = new WordReportCreator();
            Assert.Throws<NullReferenceException>(() => wordReportCreator.CreateReport(null));
        }

        private FileStreamResult TestCode()
        {
            return _valuesController.Get(null, null, null, null, 1);
        }

        private void CreateFakeController(bool addQuerystring, bool addDependency)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["test"] = "test"; // For Create a Request
            if (addQuerystring)
                httpContext.Request.QueryString = new QueryString("?isBestSeller=true");
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            _bookService = new Mock<IBookService>();
            _valuesController = new ValuesController(addDependency ? _bookService.Object : null)
                {ControllerContext = controllerContext};
        }


        public IEnumerable<BookDto> GetMockBookList()
        {
            return new List<BookDto>
            {
                new BookDto
                {
                    Author = new AuthorDto {Id = 1, Firstname = "Robert C.", Lastname = "Martin"}, Id = 1,
                    Title = "Clean Code: A Handbook of Agile Software Craftsmanship", Price = 28, AvailableStock = 0
                }
            };
        }
    }
}