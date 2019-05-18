using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Utilities;

namespace Zold.Inventory.Items
{
    /// <summary>
    /// Itemek służący do tymczasowej zmiany statystyki postaci
    /// </summary>
    class BuffItem : Item
    {
        private float attackBuff = 1.0f;
        private float defenseBuff = 1.0f;
        private float rangeBuff = 1.0f;
        private float speedBuff = 1.0f;
        private TimeSpan effectDuration = TimeSpan.Zero;
        public BuffItem(string id, ItemManager itemManager, string type) : base(id, itemManager, type)
        {
            IsBattleOnly = true;
        }

        public float AttackBuff { get => attackBuff; set => attackBuff = value; }
        public float DefenseBuff { get => defenseBuff; set => defenseBuff = value; }
        public float RangeBuff { get => rangeBuff; set => rangeBuff = value; }
        public float SpeedBuff { get => speedBuff; set => speedBuff = value; }
    }
}
