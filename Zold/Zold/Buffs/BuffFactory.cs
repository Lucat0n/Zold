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
        private string[] targetStats = new string[] { "Hp" };

        /// <summary>
        /// Zwraca buffa działającego jednorazowo i natychmiastowo.
        /// </summary>
        /// <param name="amount">O ile ma być zmieniona dana statystyka</param>
        /// <param name="targetStatID">Docelowa statystyka do zmiany. 0 - HP</param>
        /// <param name="character">Docelowa postać, na którą ma działać buff.</param>
        public InstantBuff CreateInstantBuff(int amount, byte targetStatID, Character character)
        {
            InstantBuff ib = new InstantBuff();
            ib.Amount = amount;
            ib.TargetStat = targetStats[targetStatID];
            ib.Character = character;
            return ib;
        }

        /// <summary>
        /// Zwraca buffa czasowego z czasem trwania 10 sekund i zmianą statystyki co 2 sekundy.
        /// </summary>
        /// <param name="targetStatID">Docelowa statystyka do zmiany. 0 - HP</param>
        /// <param name="delta">Przyrost, o jaki ma się zmieniać statystyka co interwał.</param>
        /// <param name="character">Docelowa postać, na którą ma działać buff.</param>
        public TimedBuff CreateTimedBuff(byte targetStatID, int delta, Character character)
        {
            TimedBuff tb = new TimedBuff
            {
                TargetStat = targetStats[targetStatID],
                Delta = delta,
                Duration = 10,
                IntervalsAmount = 5
            };
            return tb;
        }

        /// <summary>
        /// Zwraca buffa czasowego z pięcioma interwałami.
        /// </summary>
        /// <param name="targetStatID">Docelowa statystyka do zmiany. 0 - HP</param>
        /// <param name="delta">Przyrost, o jaki ma się zmieniać statystyka co interwał.</param>
        /// <param name="duration">Czas trwania buffa.</param>
        /// <param name="character">Docelowa postać, na którą ma działać buff.</param>
        public TimedBuff CreateTimedBuff(byte targetStatID, int delta, int duration, Character character)
        {
            TimedBuff tb = new TimedBuff
            {
                TargetStat = targetStats[targetStatID],
                Delta = delta,
                Duration = duration,
                IntervalsAmount = 5
            };
            return tb;
        }

        /// <summary>
        /// Zwraca buffa czasowego
        /// </summary>
        /// <param name="targetStatID">Docelowa statystyka do zmiany. 0 - HP</param>
        /// <param name="delta">Przyrost, o jaki ma się zmieniać statystyka co interwał.</param>
        /// <param name="duration">Czas trwania buffa.</param>
        /// <param name="intervalsAmount">Ilość interwałów.</param>
        /// <param name="character">Docelowa postać, na którą ma działać buff.</param>
        public TimedBuff CreateTimedBuff(byte targetStatID, int delta, int duration, byte intervalsAmount, Character character)
        {
            TimedBuff tb = new TimedBuff
            {
                TargetStat = targetStats[targetStatID],
                Delta = delta,
                Duration = duration,
                IntervalsAmount = intervalsAmount
            };
            return tb;
        }
    }
}
