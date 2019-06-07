using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zold.Statistics;

namespace Zold.Utilities
{
    class StatisticsManager
    {
        private readonly Dictionary<string, Stats> statistics;
        private readonly Dictionary<string, Stats> multipliers;

        public StatisticsManager()
        {
            statistics = new Dictionary<string, Stats>();
            multipliers = new Dictionary<string, Stats>();

            JObject json = JObject.Parse(File.ReadAllText(@"..\..\..\..\Statistics\Statistics.json"));

            statistics = json.SelectToken("Statistics").Children().OfType<JProperty>()
                .ToDictionary(p => p.Name, p => p.Value.ToObject<Stats>());
            
            multipliers = json.SelectToken("Multipliers").Children().OfType<JProperty>()
                .ToDictionary(p => p.Name, p => p.Value.ToObject<Stats>());
        }

        public Stats SetStats(string name, int level)
        {
            return new Stats(level,
                statistics[name].MaxHealth + level * multipliers[name].MaxHealth,
                statistics[name].Damage + level * multipliers[name].Damage,
                statistics[name].Speed + level * multipliers[name].Speed,
                statistics[name].Defence + level * multipliers[name].Defence,
                statistics[name].Attack + level * multipliers[name].Attack
                );
        }
    }
}
