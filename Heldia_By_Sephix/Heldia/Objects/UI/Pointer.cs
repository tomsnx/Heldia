using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Intrinsics.X86;
using Heldia.Engine;
using Heldia.Engine.Enum;
using Heldia.Managers;
using Microsoft.Xna.Framework;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Objects.UI;

public class Pointer
{
    

    public Pointer() { }

    public void Init(Main g)
    {
        
    }

    public void Update(GameTime gt, Main g)
    {
        
    }

    public void Draw(Main g)
    {
        Drawing.FillRect(new Rectangle(new Point(Drawing.Width / 2 - 10, 
                                                                Drawing.Height / 2 - 2), 
                           new Point(20, 4)), Color.Red, 1, g);
        
        Drawing.FillRect(new Rectangle(new Point(Drawing.Width / 2 - 2, 
                                                                Drawing.Height / 2 - 10), 
            new Point(4, 20)), Color.Red, 1, g);
    }

    public void Destroy(Main g)
    {
        
    }
}