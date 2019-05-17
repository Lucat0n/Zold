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
            ItemsToCollect = new Dictionary<Item, byte>();
            ItemsCollected = new Dictionary<Item, byte>();
        }

        #region properties
        internal Dictionary<Item, byte> ItemsToCollect { get => itemsToCollect; set => itemsToCollect = value; }
        internal Dictionary<Item, byte> ItemsCollected { get => itemsCollected; set => itemsCollected = value; }
        #endregion

        public void AddItemToCollect(Item item, byte amount)
        {
            ItemsToCollect.Add(item, amount);
            ItemsCollected.Add(item,
                base.QuestManager.InventoryManager.GetPlayerInventory().Items.ContainsKey(item.Id) ? 
                base.QuestManager.InventoryManager.GetPlayerInventory().Items[item.Id].Amount : (byte)0
                );
        }

        override public bool CheckCompletion()
        {
            UpdateItemsAmount();
            foreach (KeyValuePair<Item, byte> entry in ItemsToCollect)
                if (entry.Value <= ItemsCollected[entry.Key])
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
            var itemsCollectedArray = ItemsCollected.ToArray();
            foreach(KeyValuePair<Item, byte> pair in itemsCollectedArray)
            {
                if (base.QuestManager.InventoryManager.GetPlayerInventory().Items.ContainsKey(pair.Key.Id))
                {
                    ItemsCollected[pair.Key] = base.QuestManager.InventoryManager.GetPlayerInventory().Items[pair.Key.Id].Amount;
                    
                }
            }
                
        }

    }
}
