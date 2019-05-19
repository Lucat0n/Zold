using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zold.Screens.Implemented.Combat.CombatObjects.Characters;

namespace Zold.Buffs
{
    class BuffFactory
    {
        private static string[] targetStats = new string[] { "Hp" };

        /// <summary>
        /// Zwraca buffa działającego jednorazowo i natychmiastowo.
        /// </summary>
        /// <param name="amount">O ile ma być zmieniona dana statystyka</param>
        /// <param name="targetStatID">Docelowa statystyka do zmiany. 0 - HP</param>
        public static InstantBuff CreateInstantBuff(int amount, byte targetStatID)
        {
            InstantBuff ib = new InstantBuff
            {
                Amount = amount,
                TargetStat = targetStats[targetStatID],
            };
            ib.Init();
            return ib;
        }

        /// <summary>
        /// Zwraca buffa czasowego z czasem trwania 10 sekund i zmianą statystyki co 2 sekundy.
        /// </summary>
        /// <param name="targetStatID">Docelowa statystyka do zmiany. 0 - HP</param>
        /// <param name="delta">Przyrost, o jaki ma się zmieniać statystyka co interwał.</param>
        public static TimedBuff CreateTimedBuff(byte targetStatID, int delta)
        {
            TimedBuff tb = new TimedBuff
            {
                TargetStat = targetStats[targetStatID],
                Amount = delta,
                TicksToLive = 10,
                TicksPerUpdate = 2
            };
            tb.Init();
            return tb;
        }

        /// <summary>
        /// Zwraca buffa czasowego z pięcioma interwałami.
        /// </summary>
        /// <param name="targetStatID">Docelowa statystyka do zmiany. 0 - HP</param>
        /// <param name="delta">Przyrost, o jaki ma się zmieniać statystyka co interwał.</param>
        /// <param name="duration">Czas trwania buffa.</param>
        public static TimedBuff CreateTimedBuff(byte targetStatID, int delta, int duration)
        {
            TimedBuff tb = new TimedBuff
            {
                TargetStat = targetStats[targetStatID],
                Amount = delta,
                TicksToLive = duration,
                TicksPerUpdate = 2
            };
            tb.Init();
            return tb;
        }

        /// <summary>
        /// Zwraca buffa czasowego
        /// </summary>
        /// <param name="targetStatID">Docelowa statystyka do zmiany. 0 - HP</param>
        /// <param name="delta">Przyrost, o jaki ma się zmieniać statystyka co interwał.</param>
        /// <param name="duration">Czas trwania buffa.</param>
        /// <param name="ticksPerUpdate">Co ile ticków ma być modyfikowana statystyka.</param>
        public static TimedBuff CreateTimedBuff(byte targetStatID, int delta, int duration, byte ticksPerUpdate)
        {
            TimedBuff tb = new TimedBuff
            {
                TargetStat = targetStats[targetStatID],
                Amount = delta,
                TicksToLive = duration,
                TicksPerUpdate = ticksPerUpdate
            };
            tb.Init();
            return tb;
        }
    }
}
