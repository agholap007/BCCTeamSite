using Microsoft.AspNetCore.Mvc;

namespace BCCTeamSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BCCTeamController : ControllerBase
    {
        Dictionary<string, (string name, int rating)> _allrounders = new Dictionary<string, (string, int)>
        {
            {Guid.NewGuid().ToString(),("Amol", 10) },
            {Guid.NewGuid().ToString(),("Paresh", 10) },
            {Guid.NewGuid().ToString(),("Atul", 10) },
            {Guid.NewGuid().ToString(),("Amit", 10) },
            {Guid.NewGuid().ToString(),("Priam", 10) },
            {Guid.NewGuid().ToString(),("Sayantan", 10) },
            {Guid.NewGuid().ToString(),("Anuj", 10) },
        };
        Dictionary<string, (string name, int rating)> _batsmans = new Dictionary<string, (string, int)>
        {
            {Guid.NewGuid().ToString(),("Venkey", 10) },
            {Guid.NewGuid().ToString(),("Sohail", 10) },
            {Guid.NewGuid().ToString(),("Nilesh", 10) },
        };

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<BCCTeamController> _logger;

        public BCCTeamController(ILogger<BCCTeamController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTeam")]
        public IEnumerable<BCCTeam> Get()
        {
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
            Random rand = new Random();
            _allrounders = _allrounders.OrderBy(x => rand.Next()).ToDictionary(item => item.Key, item => item.Value);

            _batsmans = _batsmans.OrderBy(x => rand.Next()).ToDictionary(item => item.Key, item => item.Value);

            var rowdyFells = new BCCTeam
            {
                TeamName = "Rowdy Fells",
                Players = new List<string>(),
            };

            var pushpaFire = new BCCTeam
            {
                TeamName = "Pushpa Fire",
                Players = new List<string>(),
            };
            int index = 0;
            foreach(var allronder in _allrounders)
            {
                if(index <= 3)
                {
                    rowdyFells.Players.Add(allronder.Value.name);
                }
                else
                {
                    pushpaFire.Players.Add(allronder.Value.name);
                }
                index++;
            }

            int index1 = 0;
            foreach (var batsman in _batsmans)
            {
                if (index1 <= 0)
                {
                    rowdyFells.Players.Add(batsman.Value.name);
                }
                else
                {
                    pushpaFire.Players.Add(batsman.Value.name);
                }
                index1++;
            }

            return new List<BCCTeam> { rowdyFells, pushpaFire };
        }
    }
}