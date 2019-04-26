using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Dictionary<string, dynamic> Assets;
        private string name;
        private string extension;

        public ContentLoader(Game game)
        {
            Content = new ContentManager(game.Services, "Content");
            Assets = new Dictionary<string, dynamic>();
        }

        public void LoadLocation(string directory)
        {
            string dir= "Content/" + directory;
            foreach (string dirName in Directory.GetDirectories(dir))
            {
                switch (Path.GetFileName(dirName))
                {
                    case "Textures":
                        foreach (string fileName in Directory.GetFiles(dir + "/Textures"))
                        {
                            name = directory + "/Textures/" + Path.GetFileNameWithoutExtension(fileName);
                            Assets.Add(name, Content.Load<Texture2D>(directory + "/Textures/" + Path.GetFileNameWithoutExtension(fileName)));
                        }
                        break;
                    case "Sounds":
                        foreach (string fileName in Directory.GetFiles(dir))
                        {
                            name = directory + "/Sounds/" + Path.GetFileNameWithoutExtension(fileName);
                            Assets.Add(name, Content.Load<SoundEffect>(directory + "/Sounds/" + Path.GetFileNameWithoutExtension(fileName)));
                        }
                        break;
                    case "Music":
                        foreach (string fileName in Directory.GetFiles(dir))
                        {
                            name = directory + "/Music/" + Path.GetFileNameWithoutExtension(fileName);
                            Assets.Add(name, Content.Load<Song>(directory + "/Music/" + Path.GetFileNameWithoutExtension(fileName)));
                        }
                        break;
                }
            }
        }

        public void UnloadLocation(string name)
        {
            foreach(KeyValuePair<string, dynamic> entry in Assets)
            {
                if (entry.Key.Split('/')[0].Equals(name))
                    Assets.Remove(entry.Key);
            }
        }
    }
}
