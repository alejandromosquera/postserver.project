using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using PS.Bussiness.Managers;

namespace PS.WebApi.Controllers
{
    public class PostsController : ApiController
    {        
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }
        
        public string Get(int id)
        {
            return "value";
        }
        
        public async void Post([FromBody]string value)
        {            
            var postsManager = new PostsManager();
            await postsManager.ProcessPost(value);          
        }        
        
        public void Put(int id, [FromBody]string value)
        {
        }
        
        public void Delete(int id)
        {
        }
    }
}
