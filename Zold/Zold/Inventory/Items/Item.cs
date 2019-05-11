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
        private bool isBattleOnly = false;
        private bool isKeyItem = false;
        private short thrownDmg = 10;
        private String description = "Brak opisu";
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
            isBattleOnly = (bool)itemsBase["items"][name]["isBattleOnly"];
            isKeyItem = (bool)itemsBase["items"][name]["isKeyItem"];
            thrownDmg = (short)itemsBase["items"][name]["thrownDmg"];
            Description = (String)itemsBase["items"][name]["description"];
            miniature = (String)itemsBase["items"][name]["miniature"];
            largeScale = (String)itemsBase["items"][name]["largeScale"];
        }

    }
}
