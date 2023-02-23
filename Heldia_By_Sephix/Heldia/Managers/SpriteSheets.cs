using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia
{
    public class SpriteSheets
    {
        // texture path
        string path { get; set; } = "textures";
        string type { get; set; }
        string name { get; set; }

        Texture2D texture;

        SpriteFolderManager sfManager = new SpriteFolderManager();

        //constructor
        public SpriteSheets(Main g, int id,string name)
        {
            this.name = name;
            type = sfManager.getSFManager(id);

            if (type != null)
            {
                this.path = Path.Combine(path, type, name);
            }
            this.texture = g.Content.Load<Texture2D>(path);
        }

        // gets
        public Texture2D GetSheet() { return texture; }
    }
}
