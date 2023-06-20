using Microsoft.AspNetCore.Mvc;

namespace TeamSize.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamSizeController : ControllerBase
    {

        public TeamSizeController()
        {
        }

        [HttpGet(Name = "GetMaxEqualTeams")]
        public ActionResult<int> GetMaxEqualTeams([FromQuery] List<int> teamSize, [FromQuery] int k)
        {
            if (teamSize == null || k == null)
            {
                return BadRequest("Los parámetros no pueden ser nulos.");
            }
            if (teamSize.Count == 0)
            {
                return BadRequest("El arreglo 'teamSize' no puede estar vacío.");
            }
            if (k == 0)
            {
                return BadRequest("El parámetro 'k' no puede ser cero.");
            }

            int mode = CalculateMode(teamSize);

            for (int i = 0; i < k; i++)
            {
                int max = teamSize.Max();
                teamSize[teamSize.IndexOf(max)] = mode;
            }
            return Ok(teamSize.ToArray());
        }

        private int CalculateMode(List<int> teamSizes)
        {
            Dictionary<int, int> frequencyMap = new Dictionary<int, int>();

            foreach (int teamSize in teamSizes)
            {
                if (frequencyMap.ContainsKey(teamSize))
                {
                    frequencyMap[teamSize]++;
                }
                else
                {
                    frequencyMap[teamSize] = 1;
                }
            }

            int mode = teamSizes[0];
            int maxFrequency = 0;

            foreach (var kvp in frequencyMap)
            {
                if (kvp.Value > maxFrequency)
                {
                    mode = kvp.Key;
                    maxFrequency = kvp.Value;
                }
            }
            return mode;
        }
    }
}