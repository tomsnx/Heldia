using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Heldia.Managers;

public class Overlay
{
    private readonly List<GameObject> _uiObjects = new List<GameObject>();
    private Camera _cam;

    public Overlay(Camera cam)
    {
        _cam = cam;
    }

    public void Update(GameTime gt, Main g)
    {
        foreach (var uiObject in _uiObjects)
        {
            uiObject.Update(gt, g, _uiObjects);
        }
    }

    public void Draw(Main g)
    {
        foreach (var uiObject in _uiObjects)
        {
            uiObject.Draw(g);
        }
    }
    
    public void AddUiObject(GameObject obj)
    {
        _uiObjects.Add(obj);
    }

    public void RemoveUiObject(GameObject obj)
    {
        _uiObjects.Remove(obj);
    }
}