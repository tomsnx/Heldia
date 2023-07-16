using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia.Managers;
public class SpriteSheets
{
    // texture path
    string GlobalPath { get; set; } = "textures";
    string Type { get; set; }
    string Name { get; set; }

    Texture2D _texture;

    SpriteFolderManager _sfManager = new ();

    //constructor
    public SpriteSheets(Main g, int id,string name)
    {
        Name = name;
        Type = _sfManager.GetSfManager(id);

        if (Type != null)
        {
            GlobalPath = Path.Combine(GlobalPath, Type, Name);
        }
        _texture = g.Content.Load<Texture2D>(GlobalPath);
    }

    // gets
    public Texture2D GetSheet() { return _texture; }
}
