using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

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
            {Guid.NewGuid().ToString(),("Pritam", 10) },
            {Guid.NewGuid().ToString(),("Sayantan", 10) },
            {Guid.NewGuid().ToString(),("Anuj", 10) },
        };
        Dictionary<string, (string name, int rating)> _batsmans = new Dictionary<string, (string, int)>
        {
            {Guid.NewGuid().ToString(),("Venkey", 10) },
            {Guid.NewGuid().ToString(),("Sohail", 10) },
            {Guid.NewGuid().ToString(),("Nilesh", 10) },
        };

        private readonly ILogger<BCCTeamController> _logger;

        public BCCTeamController(ILogger<BCCTeamController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTeam/{skipAllrounder:string?}/{skipBatter:string?")]
        public IEnumerable<BCCTeam> Get(string? skipAllrounder = "",string? skipBatter = "")
        {
            List<string> removeAll = new List<string>();
            if (!string.IsNullOrWhiteSpace(skipAllrounder))
            {
                removeAll = skipAllrounder.Split(new char[] {','}).ToList();
            }

            List<string> removeBat = new List<string>();
            if (!string.IsNullOrWhiteSpace(skipBatter))
            {
                removeBat = skipBatter.Split(new char[] { ',' }).ToList();
            }


            var filterAllRounders = _allrounders.Where(x => !removeAll.Contains(x.Value.name, StringComparer.OrdinalIgnoreCase));

            Random rand1 = new Random();
            filterAllRounders = filterAllRounders.OrderBy(x => rand1.Next()).ToDictionary(item => item.Key, item => item.Value);


            Random rand2 = new Random();

            var filterAllBatter = _batsmans.Where(x => !removeBat.Contains(x.Value.name, StringComparer.OrdinalIgnoreCase));

            filterAllBatter = filterAllBatter.OrderBy(x => rand2.Next()).ToDictionary(item => item.Key, item => item.Value);

            var rowdyFells = new BCCTeam
            {
                TeamName = "Rowdy Fellas",
                Players = new List<string>(),
            };

            var pushpaFire = new BCCTeam
            {
                TeamName = "Pushpa Fire",
                Players = new List<string>(),
            };
            int halfA = filterAllRounders.Count() / 2;

            var rowdyA = filterAllRounders.Take(halfA);
            var pushpaA = filterAllRounders.Skip(halfA);

            rowdyFells.Players.AddRange(rowdyA.Select(x=>x.Value.name));
            pushpaFire.Players.AddRange(pushpaA.Select(x => x.Value.name));


            int halfB = (filterAllBatter.Count() / 2)+1;

            var rowdyB = filterAllBatter.Take(halfB);
            var pushpaB = filterAllBatter.Skip(halfB);

            rowdyFells.Players.AddRange(rowdyB.Select(x => x.Value.name));
            pushpaFire.Players.AddRange(pushpaB.Select(x => x.Value.name));

            return new List<BCCTeam> { rowdyFells, pushpaFire };
        }
    }
    
}