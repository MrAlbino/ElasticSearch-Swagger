using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElasticSearchTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IElasticClient elasticClient;

        public UsersController(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(string id)
        {
            //var response = await elasticClient.SearchAsync<User>(s => s
            //   .Index("users")
            //   .Query(q => q
            //   .Term(t => t.Name, id) || q.Match(m => m.Field(f => f.Name).Query(id))));

            var response = await elasticClient.SearchAsync<User>(s => s
               .Index("users")
               .Query(q => q.Match(m => m.Field(f => f.Name).Query(id))));
            var result= response?.Documents?.FirstOrDefault();
            return result;
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<string> Post([FromBody] User value)
        {
            var response = await elasticClient.IndexAsync<User>(value,x=>x.Index("users"));
            return response.Id;
        }

    }
}
