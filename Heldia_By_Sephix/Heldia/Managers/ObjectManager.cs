using System.Collections.Generic;
using Heldia.Menu;
using Microsoft.Xna.Framework;

namespace Heldia.Managers;
public class ObjectManager
{
    //Properties
    public List<GameObject> objects;
    public int Count { get { return objects.Count; } }

    //Constructor
    public ObjectManager()
    {
        objects = new List<GameObject>();
    }
    
    //Update
    public void Update(GameTime gt, Main g)
    {
        for(int i = 0; i < Count; i++)
        {
            GameObject obj = objects[i];

            if (obj.rendered)
            {
                obj.SetBounds(obj.x, obj.y, obj.width, obj.height);
                obj.Update(gt, g);
            }
        }
    }

    //Drawing
    public void Draw(GameTime gt, Main g)
    {
        for (int i = 0; i < Count; i++)
        {
            GameObject obj = objects[i];

            if (obj.rendered && obj.visible)
            {
                obj.Draw(gt, g);
            }
        }
    }

    public void Destroy(Main g)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects.Remove(objects[i]);
        }
    }

    //Sets
    public void Add(GameObject obj, Main g) { objects.Add(obj); obj.Init(g, objects); }
    public virtual bool Remove(GameObject obj, Main g) { obj.Destroy(g); return objects.Remove(obj); }
    public virtual bool Remove(Button obj, Main g) { obj.Destroy(g); return objects.Remove(obj); }
    public virtual bool Remove(int index, Main g) { objects[index].Destroy(g);  return objects.Remove(objects[index]); }
    public void Clear() { objects.Clear(); }

    //Gets
    public int GetCount() { return Count; }
}
