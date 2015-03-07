using System.Web.Http;
using System.Web.Mvc;
using System.Web.SessionState;
using PS.Bussiness.Managers;

namespace PS.WebApi.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    public class PostsController : ApiController
    {
        public async void Post([FromBody]string value)
        {
            var manager = new PostsManager();

            await manager.ProcessPostAndSave(value);
        }
    }
}
