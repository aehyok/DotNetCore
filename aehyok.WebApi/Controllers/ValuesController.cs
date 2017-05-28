using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aehyok.Core.Data.Entity;
using aehyok.Model.Blog;
using aehyok.Core.Data;

namespace aehyok.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly CodeFirstDbContext _dbContext;
        private readonly IRepository<Tag, int> _tagRepository;
        public ValuesController(CodeFirstDbContext dbContext)
        {
            _dbContext = dbContext;
           // _tagRepository = tagRepository;
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //测试添加Tag 并且返回Id
            //Tag tag = new Tag();
            //tag.Name = "Test";
            //_dbContext.Tag.Add(tag);
            //_dbContext.SaveChanges();
            //var tagId = tag.Id;

            Tag tag = new Tag();
            tag.Name = "TestRepository";
            _tagRepository.Insert(tag);
            var tagId = tag.Id;
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
