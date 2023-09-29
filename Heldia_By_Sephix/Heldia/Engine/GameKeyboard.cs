using System;
using System.Collections.Generic;
using Heldia.Engine.Static;
using Microsoft.Xna.Framework.Input;

namespace Heldia.Engine;

public class GameKeyboard
{
    private KeyboardState _kbState;
        
    private Dictionary<string, Keys> _keys;

    /// <summary>
    /// Game keyboard constructor.
    /// </summary>
    public GameKeyboard()
    {
        _kbState = new KeyboardState();
        
        _keys = new Dictionary<string, Keys>
        {
            {KeysList.top, Keys.Z},
            {KeysList.left, Keys.Q},
            {KeysList.bottom, Keys.S},
            {KeysList.right, Keys.D},
            {KeysList.fspel, Keys.A},
            {KeysList.qspel, Keys.E},
            {KeysList.sprint, Keys.LeftShift}
        };
    }

    /// <summary>
    /// Retrieve the value corresponding to the key in the _keys dictionary.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool GetKeyPressed(string action)
    {
        if (_keys.TryGetValue(action, out Keys key))
        {
            return _kbState.IsKeyDown(key);
        }
        return false;
    }
    
    /// <summary>
    /// Set the value corresponding to the key in the dictionary.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="key"></param>
    public void SetKeyBinding(string action, Keys key)
    {
        if (_keys.ContainsKey(action))
        {
            _keys[action] = key;
        }
    }

    /// <summary>
    /// Check if the key specified is down.
    /// </summary>
    /// <param name="k"></param>
    /// <returns></returns>
    public bool IsPressedKey(Keys k)
    {
        return this._kbState.IsKeyDown(k);
    }
    
    /*
    //set
    public void SetTop(Keys k)
    {
        this._keys[0] = k;
    }
    public void SetLeft(Keys k)
    {
        this._keys[1] = k;
    }
    public void SetBot(Keys k)
    {
        this._keys[2] = k;
    }
    public void SetRight(Keys k)
    {
        this._keys[3] = k;
    }
    public void SetSprint(Keys k)
    {
        this._keys[6] = k;
    }*/

    public void Update()
    {
        _kbState = Keyboard.GetState();
    }
    
}