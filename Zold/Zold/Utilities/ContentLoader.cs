using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace Zold.Utilities
{
    class ContentLoader
    {
        ContentManager Content;
        Dictionary<string, dynamic> Assets;
        private string name;
        private string extention;

        public ContentLoader(Game game)
        {
            Content = new ContentManager(game.Services, "../Content");
            Assets = new Dictionary<string, dynamic>();
        }

        void LoadLocation(string location)
        {
            Search("../Content/" + location);
        }

        void Search(string dir)
        {
            foreach (string dirName in Directory.GetDirectories(dir))
            {
                switch (dirName)
                {
                    case "Textures":
                        foreach (string fileName in Directory.GetFiles(dir))
                        {
                            SplitName(fileName);
                            Assets.Add(name, Content.Load<Texture2D>(dir + "/Textures/" + name));
                        }
                        break;
                    case "Sounds":
                        foreach (string fileName in Directory.GetFiles(dir))
                        {
                            SplitName(fileName);
                            Assets.Add(name, Content.Load<SoundEffect>(dir + "/Sounds/" + name));
                        }
                        break;
                    case "Music":
                        foreach (string fileName in Directory.GetFiles(dir))
                        {
                            SplitName(fileName);
                            Assets.Add(name, Content.Load<Texture2D>(dir + "/Music/" + name));
                        }
                        break;
                }
            }
        }

        void SplitName(string fileName)
        {
            string[] temp = fileName.Split('.');
            name = temp[0];
            extention = temp[1];
        }
    }
}
