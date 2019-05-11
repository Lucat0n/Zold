using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Utilities.Quests
{
    interface IQuest
    {

        #region properties
        string Description { get; set; }
        string Title { get; }
        bool IsDone { get; }
        #endregion

        bool CheckCompletion();
    }
}
