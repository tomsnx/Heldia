using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Heldia.Engine;
using Heldia.Objects;
using Heldia.Objects.UI;
using Heldia.Overlay;
using Microsoft.Xna.Framework;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Managers;

public class Overlay
{
    private readonly List<GameObject> _uiObjects = new List<GameObject>();
    private Player _player;
    private DebugInterface _debugInterface;
    private Settings _settings;

    public Overlay(Player player)
    {
        _player = player;
        _settings = new Settings(player);
        _debugInterface = new DebugInterface(player);
    }

    public void Update(GameTime gt, Main g)
    {
        foreach (var uiObject in _uiObjects)
        {
            uiObject.Update(gt, g);
        }
    }

    public void Draw(GameTime gt, Main g)
    {
        foreach (var uiObject in _uiObjects)
        {
            uiObject.Draw(gt, g);
        }

        if (Instance.DebugMode)
        {
            _debugInterface.Draw(g);
        }

        if (Instance.SettingsMode)
        {
            _settings.Draw(g);
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