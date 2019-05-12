using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Inventory;
using static Zold.Inventory.Inventory;

namespace Zold.Utilities
{
    class InventoryManager
    {
        private readonly String PlayerInventoryID = "Player";
        private Dictionary<String, Inventory.Inventory> inventories;

        public InventoryManager()
        {
            inventories = new Dictionary<string, Inventory.Inventory>
            {
                { PlayerInventoryID, new Inventory.Inventory() }
            };
        }

        

        public void AddInventory(String name, Inventory.Inventory inventory)
        {
            inventories.Add(name, inventory);
        }

        public void ClearInventory(String name)
        {
            if (inventories.ContainsKey(name))
                inventories[name].ClearInventory();
        }

        /// <summary>
        /// Tworzy nowy klucz z kopią wartości spod klucza origin lub kopiuje inwentarz pod klucz target.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        public void CopyInventory(String origin, String target)
        {
            if(inventories.ContainsKey(target))
                inventories[target] = inventories[origin];
            else
                inventories.Add(target, inventories[origin]);
        }

        /// <summary>
        /// Przenosi ekwipunek z jednego klucza pod inny z możliwością wyczyszczenia źródłowego ekwipunku.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <param name="clearBefore"></param>
        public void MoveInventory(String origin, String target, bool clearOrigin)
        {
            foreach (KeyValuePair<string, ItemAmountPair> pair in inventories[origin].Items)
            {
                inventories[target].InsertItem(pair.Key, pair.Value.Item, pair.Value.Amount);
            }
            if (clearOrigin)
                inventories[origin].ClearInventory();
        }

        public Inventory.Inventory getInventory(String name)
        {
            return inventories[name];
        }

        public Inventory.Inventory getPlayerInventory()
        {
            return inventories[PlayerInventoryID];
        }

        public void InsertItem(String targetInventory, String name, Item item)
        {
            inventories[targetInventory].InsertItem(name, item);
        }
        public void InsertItem(String targetInventory, String name, Item item, byte amount)
        {
            inventories[targetInventory].InsertItem(name, item, amount);
        }

        public void RemoveInventory(String name)
        {
            inventories.Remove(name);
        }

        /// <summary>
        /// Przenosi wszystkie sztuki danego przedmiotu do docelowego ekwipunku.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <param name="name"></param>
        public void Transfer(String origin, String target, String name)
        {
            byte amount = inventories[origin].Items[name].Amount;
            inventories[origin].RemoveItem(name);
            inventories[target].InsertItem(name, inventories[origin].Items[name].Item, amount);
        }

        /// <summary>
        /// Przenosi jedną sztukę danego przedmiotu do docelowego ekwipunku
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <param name="name"></param>
        public void TransferItem(String origin, String target, String name)
        {
            inventories[origin].RemoveItem(name, 1);
            inventories[target].InsertItem(name, inventories[origin].Items[name].Item);
        }

        /// <summary>
        /// Przenosi określoną ilość danego przedmiotu do docelowego ekwipunku
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        public void TransferItem(String origin, String target, String name, byte amount)
        {
            inventories[origin].RemoveItem(name, amount);
            inventories[target].InsertItem(name, inventories[origin].Items[name].Item ,amount);
        }

    }
}
