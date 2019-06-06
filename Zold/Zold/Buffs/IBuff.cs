using Zold.Statistics;

namespace Zold.Buffs
{
    interface IBuff
    {

        string TargetStat { get; set; }
        Stats Statistics{ get; set; }

        void Start();

    }
}
