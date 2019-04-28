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
        private string name;

        public ContentLoader(Game game, ContentManager Content)
        {
            this.Content = Content;
        }

        public void LoadLocation(string directory)
        {
            string dir = Content.RootDirectory + "/" + directory;
            foreach (string dirName in Directory.GetDirectories(dir))
            {
                switch (Path.GetFileName(dirName))
                {
                    case "Textures":
                        foreach (string fileName in Directory.GetFiles(dir + "/Textures"))
                        {
                            name = directory + "/Textures/" + Path.GetFileNameWithoutExtension(fileName);
                            Assets.Instance.Set(name, Content.Load<Texture2D>(directory + "/Textures/" + Path.GetFileNameWithoutExtension(fileName)));
                        }
                        break;
                    case "Sounds":
                        foreach (string fileName in Directory.GetFiles(dir))
                        {
                            name = directory + "/Sounds/" + Path.GetFileNameWithoutExtension(fileName);
                            Assets.Instance.Set(name, Content.Load<SoundEffect>(directory + "/Sounds/" + Path.GetFileNameWithoutExtension(fileName)));
                        }
                        break;
                    case "Music":
                        foreach (string fileName in Directory.GetFiles(dir))
                        {
                            name = directory + "/Music/" + Path.GetFileNameWithoutExtension(fileName);
                            Assets.Instance.Set(name, Content.Load<Song>(directory + "/Music/" + Path.GetFileNameWithoutExtension(fileName)));
                        }
                        break;
                    case "Fonts":
                        foreach (string fileName in Directory.GetFiles(dir))
                        {
                            name = directory + "/Fonts/" + Path.GetFileNameWithoutExtension(fileName);
                            Assets.Instance.Set(name, Content.Load<SpriteFont>(directory + "/Fonts/" + Path.GetFileNameWithoutExtension(fileName)));
                        }
                        break;
                }
            }
        }

        
        public void UnloadLocation(string name)
        {
            Assets.Instance.Remove(name);
        }
        
    }
}
