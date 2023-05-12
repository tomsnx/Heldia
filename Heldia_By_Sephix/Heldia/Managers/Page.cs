using System;
using Microsoft.Xna.Framework;

namespace Heldia.Managers;
public abstract class Page
{

    public int id;
    public bool IsLoad { get; set; }

    public Page(int id)
    {
        this.id = id;
    }

    public abstract void Init(Main g);
    public abstract void Update(GameTime gt, Main g);
    public abstract void Draw(Main g);
    public abstract void Destroy(Main g);
}
