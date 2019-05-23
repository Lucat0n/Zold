using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zold.Statistics;

namespace Zold.Utilities
{
    class StatisticsManager
    {
        private Dictionary<string, Stats> statistics;
        private Dictionary<string, Stats> multipliers;

        public StatisticsManager()
        {
            statistics = new Dictionary<string, Stats>();
            multipliers = new Dictionary<string, Stats>();

            JObject json = JObject.Parse(File.ReadAllText(@"..\..\..\..\Statistics\Statistics.json"));

            statistics = json.SelectToken("Statistics").Children().OfType<JProperty>()
                .ToDictionary(p => p.Name, p => p.Value.ToObject<Statistics.Stats>());
            
            multipliers = json.SelectToken("Multipliers").Children().OfType<JProperty>()
                .ToDictionary(p => p.Name, p => p.Value.ToObject<Statistics.Stats>());
        }
    }
}
