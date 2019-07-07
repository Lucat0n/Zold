using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zold.Utilities;

namespace Zold.Screens.Implemented.Cutscenes
{
    class Cutscene : GameScreen
    {
        private TileMapGenerator backgorund;
        private List<CutsceneCommand> commands;
        private List<Dummy> actors;

        public Cutscene(List<Dummy> actors, params CutsceneCommand[] cutsceneCommands)
        {
            this.actors = actors;
            commands = cutsceneCommands.ToList();
        }

        public void AddCommand(CutsceneCommand cutsceneCommand)
        {
            commands.Add(cutsceneCommand);
        }
        public void PlayCutscene()
        {
            foreach(CutsceneCommand cutsceneCommand in commands)
            {
                cutsceneCommand.execute();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void HandleInput(MouseState mouseState, Rectangle mousePos, KeyboardState keyboardState)
        {
            throw new NotImplementedException();
        }


        public override void LoadContent()
        {
            throw new NotImplementedException();
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
