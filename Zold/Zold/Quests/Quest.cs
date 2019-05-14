using System;
using Zold.Utilities;

namespace Zold.Quests
{
    public abstract class Quest
    {

        private bool isDone;
        private String description;
        private String questID;
        private String title;
        private QuestManager questManager;

        internal Quest(String questID, QuestManager questManager)
        {
            this.QuestID = questID;
            this.isDone = false;
            this.QuestManager = questManager;
        }

        #region properties
        public string Description { get => description; set => description = value; }
        public string Title { get => title; set => title = value; }
        public bool IsDone { get => isDone; set => isDone = value; }
        public string QuestID { get => questID; set => questID = value; }
        internal QuestManager QuestManager { get => questManager; set => questManager = value; }
        #endregion

        abstract public bool CheckCompletion();

    }
}
