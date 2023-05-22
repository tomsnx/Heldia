using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Objects;
using Heldia.Objects.UI;
using Microsoft.Xna.Framework;

namespace Heldia.Managers;

public class Overlay
{
    private bool _debugMode;
    private readonly List<GameObject> _uiObjects = new List<GameObject>();
    private Camera _cam;
    private Player _player;
    private DebugInterface _debugInterface;

    public Overlay(Camera cam, Player player)
    {
        _cam = cam;
        _player = player;
        _debugMode = true;
        _debugInterface = new DebugInterface(player);
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

        if (_debugMode)
        {
            _debugInterface.Draw(g);
        }
    }
    
    public void AddHudObject(GameObject obj)
    {
        _uiObjects.Add(obj);
    }

    public void RemoveUiObject(GameObject obj)
    {
        _uiObjects.Remove(obj);
    }
}