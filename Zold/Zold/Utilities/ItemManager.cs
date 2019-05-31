using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Inventory;
using Zold.Screens;
using Zold.Utilities;

namespace Zold.Utilities
{
    class ItemManager
    {
        private GameScreenManager gameScreenManager;
        private JObject itemsBase;
        private ContentLoader contentLoader;
        private Dictionary<String, Item> items;

        public ItemManager(ContentLoader contentLoader)
        {
            this.contentLoader = contentLoader;
            items = new Dictionary<string, Item>();
            ItemsBase = JObject.Parse(File.ReadAllText(@"..\..\..\..\Inventory\Items\Items.json"));
        }

        public GameScreenManager GameScreenManager { get => gameScreenManager; set => gameScreenManager = value; }
        internal JObject ItemsBase { get => itemsBase; set => itemsBase = value; }

        public void AddItem(string name)
        {
            //TUTAJ POWINNA BYĆ WCZYTANA TEKSTURA ZE SPRITESHEETA
            if (!items.ContainsKey(name))
            {
                Item item = new Item(name, this);
                items.Add(name, item);
            }
        }
        public Item GetItem(string name)
        {
            if (!items.ContainsKey(name))
                AddItem(name);
            return items[name];
        }

        public void AddWeapon(string name)
        {
            if (!items.ContainsKey(name))
            {
                Weapon weapon = new Weapon(name, this);
                items.Add(name, weapon);
            }
        }

        public Weapon GetWeapon(string name)
        {
            if (!items.ContainsKey(name))
                AddWeapon(name);
            return (Weapon)items[name];
        }
    }
}
