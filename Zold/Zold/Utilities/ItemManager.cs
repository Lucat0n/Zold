using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Utilities;

namespace Zold.Inventory.Items
{
    class ItemManager
    {
        JObject itemsBase;

        public ItemManager()
        {
            ItemsBase = JObject.Parse(File.ReadAllText(@"..\..\..\..\Inventory\Items\Items.json"));
        }

        internal JObject ItemsBase { get => itemsBase; set => itemsBase = value; }

        public Item getItem(string name)
        {
            Item item = new Item(name, this);
            return item;
        }

        public Weapon getWeapon(string name)
        {
            return new Weapon(name, this);
        }
    }
}
