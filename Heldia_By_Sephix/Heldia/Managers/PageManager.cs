using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Heldia
{
    public class PageManager
    {

        public List<Page> pages = new List<Page>();
        public Page actualPage;
        public Page nextPage;

        public int count { get { return pages.Count; } }
        public static int selected;

        public void Update(GameTime gt, Main g)
        {
            actualPage = pages[selected];
            for(int i = 0; i < count; i++)
            {
                Page page = pages[i];
                if(page.id == selected) { page.Update(gt, g); }
            }
        }

        public void Draw(Main g)
        {
            for (int i = 0; i < count; i++)
            {
                Page page = pages[i];
                if (page.id == selected) { page.Draw(g); }
            }
        }

        public virtual void Set(int id) { selected = id; }
        public virtual void Set(Page page) { selected = page.id; }
        public void Add(Page page, Main g) { pages.Add(page);  page.Init(g); }
        public void Remove(Page page) { pages.Remove(page); }
        public void Clear() { pages.Clear(); }
        public void ChangePage(Page nextPage)
        {
            this.nextPage = nextPage;
        }
    }
}
