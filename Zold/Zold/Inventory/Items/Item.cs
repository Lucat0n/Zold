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
        private String largeScale;
        
        public bool IsBattleOnly { get => isBattleOnly; set => isBattleOnly = value; }
        public bool IsKeyItem { get => isKeyItem; set => isKeyItem = value; }
        public short ThrownDmg { get => thrownDmg; set => thrownDmg = value; }
        public string Description { get => description; set => description = value; }
        public string Name { get => name; set => name = value; }
        public String Miniature { get => miniature; set => miniature = value; }
        public String LargeScale { get => largeScale; set => largeScale = value; }

        public Item(String name, ItemManager itemManager)
        {
            this.name = name;
            JObject itemsBase = itemManager.ItemsBase;
            isBattleOnly = itemsBase["items"][name]["isBattleOnly"]!=null ? (bool)itemsBase["items"][name]["isBattleOnly"] : false;
            isKeyItem = itemsBase["items"][name]["isKeyItem"]!=null ? (bool)itemsBase["items"][name]["isKeyItem"] : false;
            thrownDmg = itemsBase["items"][name]["thrownDmg"]!=null ? (short)itemsBase["items"][name]["thrownDmg"] : (short)10;
            Description = itemsBase["items"][name]["thrownDmg"]!=null ? (String)itemsBase["items"][name]["description"] : "Brak opisu";
            miniature = (String)itemsBase["items"][name]["miniature"];
            largeScale = (String)itemsBase["items"][name]["largeScale"];
        }

    }
}
