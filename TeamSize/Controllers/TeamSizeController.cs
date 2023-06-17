using Microsoft.AspNetCore.Mvc;

namespace TeamSize.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamSizeController : ControllerBase
    {
 

        private readonly ILogger<TeamSizeController> _logger;

        public TeamSizeController(ILogger<TeamSizeController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMaxEqualTeams")]
        public ActionResult<int> GetMaxEqualTeams([FromQuery] int[] teamSize, [FromQuery] int k)
        {
            if (teamSize == null || k == null)
            {
                return BadRequest("Los parámetros no pueden ser nulos.");
            }
            if (teamSize.Length == 0)
            {
                return BadRequest("El arreglo 'teamSize' no puede estar vacío.");
            }
            if (k == 0)
            {
                return BadRequest("El parámetro 'k' no puede ser cero.");
            }
            Dictionary<int, int> counter = teamSize.GroupBy(x => x)
                                                .ToDictionary(x => x.Key, x => x.Count());
            List<int> keys = counter.Keys.OrderByDescending(x => x).ToList();
            int result = 0;

            foreach (int key in keys)
            {
                //result = Math.Max(result, counter[key] + Math.Min(k, greater));
                //greater += counter[key];
                result = Math.Max(result, counter[key] + Math.Min(k, result));
            }

            return result;
        }
    }
}