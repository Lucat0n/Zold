using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Utilities.Quests
{
    public abstract class Quest
    {

        private bool isDone;
        private String title;
        private String description;
        private QuestManager questManager;

        public Quest(String questID, QuestManager questManager)
        {
            this.isDone = false;
            this.title = title;
            this.Description = description;
            this.questManager = questManager;
        }

        #region properties
        public string Description { get => description; set => description = value; }
        public string Title { get => title; }
        public bool IsDone { get => isDone; }
        #endregion

        abstract public bool CheckCompletion();

    }
}
