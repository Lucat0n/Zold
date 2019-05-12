using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Utilities.Quests;

namespace Zold.Utilities
{
    public class QuestManager
    {
        private HashSet<Quest> CompletedQuests;
        private HashSet<Quest> ActiveQuests;
        private JObject questsBase;

        public QuestManager()
        {
            CompletedQuests = new HashSet<Quest>();
            ActiveQuests = new HashSet<Quest>();
        }

        public void UpdateQuests()
        {
            foreach(Quest quest in ActiveQuests){
                quest.CheckCompletion();
            }
        }

    }
}
