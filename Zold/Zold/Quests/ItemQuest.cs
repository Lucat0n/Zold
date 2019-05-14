using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                base.QuestManager.InventoryManager.GetPlayerInventory().Items.ContainsKey(item.Name) ? 
                base.QuestManager.InventoryManager.GetPlayerInventory().Items[item.Name].Amount : (byte)0
                );
        }

        override public bool CheckCompletion()
        {
            UpdateItemsAmount();
            foreach (KeyValuePair<Item, byte> entry in itemsToCollect)
                if (entry.Value <= itemsCollected[entry.Key])
                    continue;
                else
                {
                    IsDone = false;
                    return IsDone;
                }
            IsDone = true;
            return IsDone;
        }

        public void UpdateItemsAmount()
        {
            var itemsCollectedArray = itemsCollected.ToArray();
            foreach(KeyValuePair<Item, byte> pair in itemsCollectedArray)
            {
                if (base.QuestManager.InventoryManager.GetPlayerInventory().Items.ContainsKey(pair.Key.Name))
                {
                    itemsCollected[pair.Key] = base.QuestManager.InventoryManager.GetPlayerInventory().Items[pair.Key.Name].Amount;
                    
                }
            }
                
        }

    }
}
