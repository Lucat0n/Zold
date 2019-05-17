using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zold.Quests;

namespace Zold.Utilities
{
    class QuestManager
    {
        private Dictionary<string, Quest> completedQuests;
        private Dictionary<string, Quest> activeQuests;
        private JObject questsBase;
        private ItemManager itemManager;
        private InventoryManager inventoryManager;

        internal InventoryManager InventoryManager { get => inventoryManager; set => inventoryManager = value; }
        public Dictionary<string, Quest> CompletedQuests { get => completedQuests; set => completedQuests = value; }
        public Dictionary<string, Quest> ActiveQuests { get => activeQuests; set => activeQuests = value; }

        public QuestManager(InventoryManager inventoryManager, ItemManager itemManager)
        {
            this.itemManager = itemManager;
            questsBase = JObject.Parse(File.ReadAllText(@"..\..\..\..\Quests\Quests.json"));
            CompletedQuests = new Dictionary<string, Quest>();
            ActiveQuests = new Dictionary<string, Quest>();
            this.InventoryManager = inventoryManager;
        }

        public void UpdateQuests()
        {
            HashSet<Quest> questsToMove = new HashSet<Quest>();
            foreach(KeyValuePair<string, Quest> pair in ActiveQuests){
                if (pair.Value.CheckCompletion())
                    questsToMove.Add(pair.Value);
            }
            foreach(Quest quest in questsToMove)
            {
                ActiveQuests.Remove(quest.QuestID);
                CompletedQuests.Add(quest.QuestID, quest);
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
            ActiveQuests.Add(questID, itemQuest);
        }

        public void AddLocationQuest(string questID)
        {
            LocationQuest locationQuest = new LocationQuest(questID, this);
            locationQuest.Title = (string)questsBase["locationQuests"][questID]["title"];
            locationQuest.Description = (string)questsBase["locationQuests"][questID]["description"];
            locationQuest.LocationToVisit = (string)questsBase["locationQuests"][questID]["locationToVisit"];
            ActiveQuests.Add(questID, locationQuest);
        }

        public string GetActiveQuestName(int index, bool sorted)
        {
            var quests = ActiveQuests.Values.ToArray();
            if (sorted)
                Array.Sort(quests);
            return quests[index].Title;
        }

    }
}
