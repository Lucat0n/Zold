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
        private String description;
        private String miniature;
        private String name;
        private String id;
        private String largeScale;
        
        public bool IsBattleOnly { get => isBattleOnly; set => isBattleOnly = value; }
        public bool IsKeyItem { get => isKeyItem; set => isKeyItem = value; }
        public short ThrownDmg { get => thrownDmg; set => thrownDmg = value; }
        public string Description { get => description; set => description = value; }
        public string Id { get => id; set => id = value; }
        public String Miniature { get => miniature; set => miniature = value; }
        public String LargeScale { get => largeScale; set => largeScale = value; }
        public string Name { get => name; set => name = value; }

        public Item(String id, ItemManager itemManager)
        {
            this.id = id;
            JObject itemsBase = itemManager.ItemsBase;
            Name = itemsBase["items"][id]["name"]!=null ? (string)itemsBase["items"][id]["name"] : "Brak nazwy";
            isBattleOnly = itemsBase["items"][id]["isBattleOnly"]!=null ? (bool)itemsBase["items"][id]["isBattleOnly"] : false;
            isKeyItem = itemsBase["items"][id]["isKeyItem"]!=null ? (bool)itemsBase["items"][id]["isKeyItem"] : false;
            thrownDmg = itemsBase["items"][id]["thrownDmg"]!=null ? (short)itemsBase["items"][id]["thrownDmg"] : (short)10;
            Description = itemsBase["items"][id]["thrownDmg"]!=null ? (String)itemsBase["items"][id]["description"] : "Brak opisu";
            miniature = (String)itemsBase["items"][id]["miniature"]!=null ? (String)itemsBase["items"][id]["miniature"] : "iconPlaceholder";
            largeScale = (String)itemsBase["items"][id]["largeScale"]!=null ? (String)itemsBase["items"][id]["largeScale"] : "largePlaceholder";
        }

        public Item(String id, ItemManager itemManager, String type)
        {
            JObject itemsBase = itemManager.ItemsBase;
            isBattleOnly = itemsBase[type][id]["isBattleOnly"] != null ? (bool)itemsBase[type][id]["isBattleOnly"] : false;
            isKeyItem = itemsBase[type][id]["isKeyItem"] != null ? (bool)itemsBase[type][id]["isKeyItem"] : false;
            thrownDmg = itemsBase[type][id]["thrownDmg"] != null ? (short)itemsBase[type][id]["thrownDmg"] : (short)10;
            Description = itemsBase[type][id]["thrownDmg"] != null ? (String)itemsBase[type][id]["description"] : "Brak opisu";
            miniature = (String)itemsBase[type][id]["miniature"] != null ? (String)itemsBase[type][id]["miniature"] : "iconPlaceholder";
            largeScale = (String)itemsBase[type][id]["largeScale"] != null ? (String)itemsBase[type][id]["largeScale"] : "largePlaceholder";
        }

    }
}
