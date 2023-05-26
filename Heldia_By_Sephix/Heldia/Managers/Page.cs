using System;
using Microsoft.Xna.Framework;

namespace Heldia.Managers;
public abstract class Page
{
    public int id;
    public bool IsLoad { get; set; }
    public ObjectManager ObjMgr { get; set; }

    public Page(int id)
    {
        this.id = id;
    }

    public abstract void Init(Main g);
    public abstract void Update(GameTime gt, Main g);
    public abstract void Draw(GameTime gt, Main g);

    public void Destroy(Main g)
    {
        IsLoad = false;
        ObjMgr.Destroy(g);
    }
}
