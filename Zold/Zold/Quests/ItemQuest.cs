using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Inventory;
using Zold.Utilities;

namespace Zold.Quests
{
    class ItemQuest:Quest
    {
        private Dictionary<Item, byte> itemsToCollect;
        private Dictionary<Item, byte> itemsCollected;
        
        public ItemQuest(String questID, QuestManager questManager):base(questID, questManager)
        {
            itemsToCollect = new Dictionary<Item, byte>();
            itemsCollected = new Dictionary<Item, byte>();
        }

        #region properties

        #endregion

        public void AddItemToCollect(Item item, byte amount)
        {
            itemsToCollect.Add(item, amount);
            itemsCollected.Add(item,
                base.QuestManager.InventoryManager.getPlayerInventory().Items.ContainsKey(item.Name) ? 
                base.QuestManager.InventoryManager.getPlayerInventory().Items[item.Name].Amount : (byte)0
                );
        }

        override public bool CheckCompletion()
        {
            foreach(KeyValuePair<Item, byte> entry in itemsToCollect)
                if (entry.Value >= itemsCollected[entry.Key])
                {
                    IsDone = false;
                    return IsDone;
                }
            IsDone = true;
            return IsDone;
        }

        public void updateItemsAmount()
        {
            foreach(KeyValuePair<Item, byte> pair in itemsCollected)
            {
                if (base.QuestManager.InventoryManager.getPlayerInventory().Items.ContainsKey(pair.Key.Name))
                {
                    itemsCollected[pair.Key] = base.QuestManager.InventoryManager.getPlayerInventory().Items[pair.Key.Name].Amount;
                }
            }
                
        }

    }
}
