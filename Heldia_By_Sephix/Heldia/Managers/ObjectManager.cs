using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia
{
    public class ObjectManager
    {
        //Properties
        public List<GameObject> objects = new List<GameObject>();
        public int count { get { return objects.Count; } }

        //Constructor
        public ObjectManager()
        {

        }
        
        //Update
        public void Update(GameTime gt, Main g)
        {
            for(int i = 0; i < count; i++)
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
        public void Draw(Main g)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject obj = objects[i];

                if (obj.rendered && obj.visible)
                {
                    obj.Draw(g);
                }
            }
        }

        //Sets
        public void Add(GameObject obj, Main g) { objects.Add(obj); obj.Init(g); }
        public virtual void Remove(GameObject obj, Main g) { obj.Destroy(g); objects.Remove(obj); }
        public virtual void Remove(int index, Main g) { objects[index].Destroy(g);  objects.Remove(objects[index]); }
        public void Clear() { objects.Clear(); }

        //Gets
        public int GetCount() { return count; }
    }
}
