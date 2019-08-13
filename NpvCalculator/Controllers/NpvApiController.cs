using System.Threading.Tasks;
using System.Web.Http;
using NpvCalculator.Helper;
using NpvCalculator.Models;

namespace NpvCalculator.Controllers
{
    public class NpvApiController : ApiController
    {
        [Route("api/GetNpvHistoryByID")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await new NpvHelper().GetNpvByIDAsync(id);
            return Ok(result);
        }

        [Route("api/Calculate")]
        public async Task<IHttpActionResult> Calculate([FromBody]NpvModel model)
        {
            var result = await new NpvHelper().GetNpvResultAsync(model);
            return Ok(result);
        }
    }
}
