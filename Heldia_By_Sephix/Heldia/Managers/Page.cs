using System;
using Microsoft.Xna.Framework;

namespace Heldia
{
    public abstract class Page
    {

        public int id;

        public Page(int id)
        {
            this.id = id;
        }

        public abstract void Init(Main g);
        public abstract void Update(GameTime gt, Main g);
        public abstract void Draw(Main g);
    }
}
