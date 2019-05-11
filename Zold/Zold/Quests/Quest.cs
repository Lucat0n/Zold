using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Utilities.Quests
{
    abstract class ItemQuest:IQuest
    {
        private bool isDone;
        private String title;
        private String description;
        private Dictionary<dynamic, byte> itemsToCollect;
        private Dictionary<dynamic, byte> itemsCollected;
        
        public ItemQuest(String title, String description)
        {
            this.isDone = false;
            this.title = title;
            this.Description = description;
        }

        #region properties
        public string Description { get => description; set => description = value; }
        public string Title { get => title;}
        public bool IsDone { get => isDone;}
        #endregion

        public bool CheckCompletion()
        {
            foreach(KeyValuePair<dynamic, byte> entry in itemsToCollect)
            if (itemsToCollect.Count == 0)
                return isDone = true;
            return false;
        }

    }
}
