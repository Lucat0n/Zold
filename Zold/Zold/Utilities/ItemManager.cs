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
            Item item = new Item(name, this);
            //TUTAJ POWINNA BYĆ WCZYTANA TEKSTURA ZE SPRITESHEETA
            items.Add(name, item);
        }
        public Item GetItem(string name)
        {
            return items[name];
        }

        public Weapon GetWeapon(string name)
        {
            return (Weapon)items[name];
        }
    }
}
