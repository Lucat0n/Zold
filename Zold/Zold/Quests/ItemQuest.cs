using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Inventory;

namespace Zold.Utilities.Quests
{
    abstract class ItemQuest:Quest
    {
        private bool isDone;
        private String title;
        private String description;
        private Dictionary<Item, byte> itemsToCollect;
        private Dictionary<Item, byte> itemsCollected;
        
        public ItemQuest(String questID, QuestManager questManager):base(questID, questManager){ }

        #region properties
        
        #endregion

        override public bool CheckCompletion()
        {
            foreach(KeyValuePair<Item, byte> entry in itemsToCollect)
            if (itemsToCollect.Equals(itemsCollected))
                return isDone = true;
            return false;
        }

        public void updateItemsAmount()
        {
            foreach(KeyValuePair<Item, byte> pair in itemsCollected)
            {

            }
                
        }

    }
}
