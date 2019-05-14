using System;
using Zold.Utilities;

namespace Zold.Quests
{
    public abstract class Quest
    {

        private bool isDone;
        private String title;
        private String description;
        private QuestManager questManager;

        internal Quest(String questID, QuestManager questManager)
        {
            this.isDone = false;
            this.title = title;
            this.Description = description;
            this.QuestManager = questManager;
        }

        #region properties
        public string Description { get => description; set => description = value; }
        public string Title { get => title; set => title = value; }
        public bool IsDone { get => isDone; set => isDone = value; }
        internal QuestManager QuestManager { get => questManager; set => questManager = value; }
        #endregion

        abstract public bool CheckCompletion();

    }
}
