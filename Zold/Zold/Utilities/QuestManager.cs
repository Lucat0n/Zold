using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using Zold.Quests;

namespace Zold.Utilities
{
    class QuestManager
    {
        private HashSet<Quest> completedQuests;
        private HashSet<Quest> activeQuests;
        private JObject questsBase;
        private ItemManager itemManager;
        private InventoryManager inventoryManager;

        internal InventoryManager InventoryManager { get => inventoryManager; set => inventoryManager = value; }

        public QuestManager(InventoryManager inventoryManager, ItemManager itemManager)
        {
            this.itemManager = itemManager;
            questsBase = JObject.Parse(File.ReadAllText(@"..\..\..\..\Quests\Quests.json"));
            completedQuests = new HashSet<Quest>();
            activeQuests = new HashSet<Quest>();
            this.InventoryManager = inventoryManager;
        }

        public void UpdateQuests()
        {
            foreach(Quest quest in activeQuests){
                quest.CheckCompletion();
            }
        }

        public void AddItemQuest(string questID)
        {
            ItemQuest itemQuest = new ItemQuest(questID, this);
            itemQuest.Title = (string)questsBase["itemQuests"][questID]["title"];
            itemQuest.Description = (string)questsBase["itemQuests"][questID]["description"];
            var itemsToCollect = questsBase["itemQuests"][questID]["itemsToCollect"];
            foreach(var item in itemsToCollect)
                itemQuest.AddItemToCollect(itemManager.GetItem(item.ToObject<JProperty>().Name), (byte)item);
            //itemQuest.
            activeQuests.Add(itemQuest);
        }

    }
}
