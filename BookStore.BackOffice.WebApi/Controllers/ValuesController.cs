using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookStore.BackOffice.WebApi.Business.Abstract;
using BookStore.BackOffice.WebApi.Dto;
using Microsoft.AspNetCore.Mvc;



namespace BookStore.BackOffice.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ICreatorService _creatorService;
        public ValuesController(IBookService bookService, ICreatorService creatorService)
        {
            _bookService = bookService;
            _creatorService = creatorService;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<BookDto> Get([FromQuery]int? beforeThisYear,int? afterThisYear,int? authorId,bool? isBestSeller=false)
        {
                var appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Directory.CreateDirectory(Environment.SpecialFolder.Desktop + "/asd").Create();
//                Madafa.CreateWordprocessingDocument(appDataDir+"/asd");
//Madafa.Asd();


                
            if (string.IsNullOrEmpty(Request.QueryString.Value)) return new List<BookDto>();
            var dictionary = Request.QueryString.Value.Replace("?", "").Split('&')
                .ToDictionary(x => x.Split('=')[0], x => x.Split('=')[1]).Where(s=>!string.IsNullOrEmpty(s.Value));
            if (dictionary.Any())
            {
                var ls = _bookService.Get(beforeThisYear,afterThisYear,authorId,isBestSeller);
                _creatorService.CreateWord(ls);
                return ls;
            }
            return new List<BookDto>();
        }

//        // GET api/values/5
//        [HttpGet("{id}")]
//        public ActionResult<string> Get(int id)
//        {
//            return "value";
//        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
