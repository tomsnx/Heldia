using System;
using System.Collections.Generic;
using Heldia.Pages;
using Microsoft.Xna.Framework;

namespace Heldia.Managers;
public class PageManager
{
    // Properties
    public List<Page> pages = new List<Page>();
    public Page PreviousPage { get; set; }
    public Page ActualPage { get; set;  }
    public Page NextPage { get; set; }

    public int Count { get { return pages.Count; } }
    public int Selected { get; set; }
    
    public void Update(GameTime gt, Main g)
    {
        UpdateActualPage();
        
        // Update ActualPage to the select Page which is just set before
        ActualPage = pages[Selected]; // By default is 1 for pageMenu set in Main class

        for(int i = 0; i < Count; i++)
        {
            Page page = pages[i];
            if(page.id == Selected) { page.Update(gt, g); }
        }

        // Destroy the page content which is not used by the game
        if (PreviousPage != null)
        {
            if (PreviousPage.IsLoad)
            {
                PreviousPage.Destroy(g);
            }
        }
    }
    
    public void Draw(GameTime gt, Main g)
    {
        for (int i = 0; i < Count; i++)
        {
            Page page = pages[i];
            if (page.id == Selected) { page.Draw(gt, g); }
        }
    }

    // Public Methods
    public virtual void Set(int id) { Selected = id; }
    public virtual void Set(Page page) { Selected = page.id; }
    public void Add(Page page, Main g) { pages.Add(page);  page.Init(g); }
    public void Remove(Page page) { pages.Remove(page); }
    public void Clear() { pages.Clear(); }
    
    public void ChangePage(Page nextPage)
    {
        NextPage = nextPage;
    }

    // Private methods
    private Page FindPage(int id)
    {
        return pages.Find(p => p.id == id);
    }
    
    /**
     * Pass NextPage to ActualPage via Set ( set `Selected` variable to ActualPage id)
     */
    private void UpdateActualPage()
    {
        if (NextPage != null)
        {
            PreviousPage = ActualPage;
            ActualPage = NextPage;
            Set(ActualPage); // Set the Selected variable to the id of ActualPage
            NextPage = null;
        }
    }
}
