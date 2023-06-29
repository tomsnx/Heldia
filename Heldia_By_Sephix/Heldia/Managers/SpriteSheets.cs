using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia.Managers;
public class SpriteSheets
{
    // texture path
    string path { get; set; } = "textures";
    string type { get; set; }
    string name { get; set; }

    Texture2D _texture;

    SpriteFolderManager _sfManager = new SpriteFolderManager();

    //constructor
    public SpriteSheets(Main g, int id,string name)
    {
        this.name = name;
        type = _sfManager.GetSfManager(id);

        if (type != null)
        {
            path = Path.Combine(path, type, name);
        }
        _texture = g.Content.Load<Texture2D>(path);
    }

    // gets
    public Texture2D GetSheet() { return _texture; }
}
