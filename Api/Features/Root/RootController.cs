using System.Web.Http;

namespace Api.Features.Root
{
    [Route("")]
    public class RootController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new DefaultViewModel {Description = "Homepage of Site integration."});
        }
    }
}
