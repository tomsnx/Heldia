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
            {KeysList.escape, Keys.Escape},
            {KeysList.sprint, Keys.LeftShift},
            {KeysList.away, Keys.J}
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

    /// <summary>
    /// Update the Keyboard state.
    /// </summary>
    public void Update()
    {
        _kbState = Keyboard.GetState();
    }
    
}