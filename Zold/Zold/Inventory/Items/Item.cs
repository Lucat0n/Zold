using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Utilities;

namespace Zold.Inventory
{
    class Item
    {
        private bool isBattleOnly;
        private bool isKeyItem;
        private short thrownDmg;
        private int cost;
        private string description;
        private string miniature;
        private string name;
        private string id;
        private string largeScale;
        
        public bool IsBattleOnly { get => isBattleOnly; set => isBattleOnly = value; }
        public bool IsKeyItem { get => isKeyItem; set => isKeyItem = value; }
        public short ThrownDmg { get => thrownDmg; set => thrownDmg = value; }
        public int Cost { get => cost; set => cost = value; }
        public string Description { get => description; set => description = value; }
        public string Id { get => id; set => id = value; }
        public string Miniature { get => miniature; set => miniature = value; }
        public string LargeScale { get => largeScale; set => largeScale = value; }
        public string Name { get => name; set => name = value; }

        public Item(string id, ItemManager itemManager)
        {
            this.id = id;
            JObject itemsBase = itemManager.ItemsBase;
            Name = itemsBase["items"][id]["name"]!=null ? (string)itemsBase["items"][id]["name"] : "Brak nazwy";
            isBattleOnly = itemsBase["items"][id]["isBattleOnly"]!=null ? (bool)itemsBase["items"][id]["isBattleOnly"] : false;
            isKeyItem = itemsBase["items"][id]["isKeyItem"]!=null ? (bool)itemsBase["items"][id]["isKeyItem"] : false;
            thrownDmg = itemsBase["items"][id]["thrownDmg"]!=null ? (short)itemsBase["items"][id]["thrownDmg"] : (short)10;
            cost = itemsBase["items"][id]["cost"] != null ? (int)itemsBase["weapons"][name]["cost"] : 100;
            Description = itemsBase["items"][id]["thrownDmg"]!=null ? (string)itemsBase["items"][id]["description"] : "Brak opisu";
            miniature = itemsBase["items"][id]["miniature"]!=null ? (string)itemsBase["items"][id]["miniature"] : "iconPlaceholder";
            largeScale = itemsBase["items"][id]["largeScale"]!=null ? (string)itemsBase["items"][id]["largeScale"] : "largePlaceholder";
        }

        public Item(string id, ItemManager itemManager, string type)
        {
            JObject itemsBase = itemManager.ItemsBase;
            Name = itemsBase[type][id]["name"] != null ? (string)itemsBase[type][id]["name"] : "Brak nazwy";
            isBattleOnly = itemsBase[type][id]["isBattleOnly"] != null ? (bool)itemsBase[type][id]["isBattleOnly"] : false;
            isKeyItem = itemsBase[type][id]["isKeyItem"] != null ? (bool)itemsBase[type][id]["isKeyItem"] : false;
            thrownDmg = itemsBase[type][id]["thrownDmg"] != null ? (short)itemsBase[type][id]["thrownDmg"] : (short)10;
            cost = itemsBase[type][id]["cost"] != null ? (int)itemsBase[type][id]["cost"] : 100;
            Description = itemsBase[type][id]["thrownDmg"] != null ? (string)itemsBase[type][id]["description"] : "Brak opisu";
            miniature = itemsBase[type][id]["miniature"] != null ? (string)itemsBase[type][id]["miniature"] : "iconPlaceholder";
            largeScale = itemsBase[type][id]["largeScale"] != null ? (string)itemsBase[type][id]["largeScale"] : "largePlaceholder";
        }

    }
}
