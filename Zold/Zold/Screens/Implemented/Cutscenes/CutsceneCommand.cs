using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Screens.Implemented.Cutscenes
{
    abstract class CutsceneCommand
    {
        protected Dummy dummy;

        protected CutsceneCommand(Dummy dummy)
        {
            this.dummy = dummy;
        }

        public abstract void execute();
    }
}
