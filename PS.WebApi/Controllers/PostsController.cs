using System.Web.Http;
using PS.Bussiness.Managers;

namespace PS.WebApi.Controllers
{
    public class PostsController : ApiController
    {
        public async void Post([FromBody]string value)
        {
            var manager = new PostsManager();

            await manager.ProcessPost(value);
        }
    }
}
