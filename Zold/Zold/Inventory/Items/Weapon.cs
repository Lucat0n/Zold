using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Inventory.Items;

namespace Zold.Inventory
{
    class Weapon:Item
    {
        private short baseDmg;
        private short range = 32;
        private float dmgMultiplier = 1.0f;
        private float defMultiplier = 1.0f;
        private float sigma = 0.25f;
        //długość ataku względem jednej sekundy
        private float speed = 1.0f;
        private int cost = 100;

        #region constructors
        public Weapon(String name, ItemManager itemManager) : base(name, itemManager)
        {
            JObject itemsBase = itemManager.ItemsBase;
            this.baseDmg = (short)itemsBase["weapons"][name]["baseDmg"];
            this.range = (short)itemsBase["weapons"][name]["range"];
            this.dmgMultiplier = (float)itemsBase["weapons"][name]["dmgMultiplier"];
            this.defMultiplier = (float)itemsBase["weapons"][name]["defMultiplier"];
            this.Sigma = (float)itemsBase["weapons"][name]["sigma"];
            this.speed = (float)itemsBase["weapons"][name]["speed"];
            this.cost = (int)itemsBase["weapons"][name]["cost"];
        }
        #endregion

        #region properties
        public short BaseDmg { get => baseDmg; set => baseDmg = value; }
        public short Range { get => range; set => range = value; }
        public float DmgMultiplier { get => dmgMultiplier; set => dmgMultiplier = value; }
        public float DefMultiplier { get => defMultiplier; set => defMultiplier = value; }
        public float Speed { get => speed; set => speed = value; }
        public int Cost { get => cost; set => cost = value; }
        public float Sigma { get => sigma; set => sigma = value; }
        #endregion
    }
}
