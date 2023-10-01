using System;
using System.Collections.Generic;
using Heldia.Engine.Static;
using Microsoft.Xna.Framework.Input;
using static Heldia.Engine.Singleton.GameManager;

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
            // Directions
            {KeysList.top, Keys.Z},
            {KeysList.left, Keys.Q},
            {KeysList.bottom, Keys.S},
            {KeysList.right, Keys.D},
            
            // Specials
            {KeysList.escape, Keys.Escape},
            {KeysList.sprint, Keys.LeftShift},
            
            // Spels
            {KeysList.fspel, Keys.A},
            {KeysList.qspel, Keys.E},

            // Bar number
            {KeysList.one, Keys.D1},
            {KeysList.two, Keys.D2},
            {KeysList.three, Keys.D3},
            {KeysList.four, Keys.D4},
            {KeysList.five, Keys.D5},
            {KeysList.six, Keys.D6},
            {KeysList.seven, Keys.D7},
            {KeysList.eight, Keys.D8},
            {KeysList.nine, Keys.D9},
            {KeysList.zero, Keys.D0},
            
            // Others
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

    public Keys[] GetPressedKeys()
    {
        return _kbState.GetPressedKeys();
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

    public bool KeyboardUsed()
    {
        return Instance.GameKb.GetPressedKeys().Length > 0;
    }

    /// <summary>
    /// Update the Keyboard state.
    /// </summary>
    public void Update()
    {
        _kbState = Keyboard.GetState();
    }
    
}