using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Utilities;

namespace Zold.Quests
{
    class LocationQuest : Quest
    {
        private bool isLocationVisited;
        private String locationToVisit;

        internal LocationQuest(String questID, QuestManager questManager) : base(questID, questManager)
        {
            isLocationVisited = false;
        }

        public string LocationToVisit { get => locationToVisit; set => locationToVisit = value; }

        public override bool CheckCompletion()
        {
            return isLocationVisited;
        }

        public void TriggerVisit()
        {
            this.isLocationVisited = true;
        }
    }
}
