using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zold.Cutscenes
{
    class Cutscene
    {
        private List<CutsceneCommand> listOfCommands = new List<CutsceneCommand>();

        public Cutscene()
        {
        }

        public void AddCommand(CutsceneCommand cutsceneCommand)
        {
            listOfCommands.Add(cutsceneCommand);
        }

        public void PlayCutscene()
        {
            foreach(CutsceneCommand cutsceneCommand in listOfCommands)
            {
                cutsceneCommand.execute();
            }
        }
    }
}
