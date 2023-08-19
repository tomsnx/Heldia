using System;
using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Menu;
public class Button : GameObject
{
    // Button Texture
    private Texture2D texture;

    private Vector2 _textSize;
    private Vector2 _textPosition;

    public static bool isAnyButtonHovered;
    public bool MouseHover { get; private set; }
    
    public Color ButtonColor { get; set; }
    
    public event EventHandler Click;
    
    //TODO: Mettre des textures sur les boutons
    //private SpriteFont font;

    /*public Button(int x, int y, int w, int h, int id, string text, SpriteFont font , Color textColor) : base(x, y, w, h, id)
    {
        visible = true;
        this.text = text;
        this.font = font;
        this.textColor = textColor;
    }*/
    
    // Constructor
    public Button(int x, int y, int w, int h, int id, String text) : base(x, y, w, h, id)
    {
        visible = true;
        this.text = text;
    }
    
    // Methods
    public override void Init(Main g, List<GameObject> objects)
    {
        _textSize = Drawing.Arial32.MeasureString(text);
        _textPosition = new Vector2(
            bounds.Center.X - _textSize.X / 2,
            bounds.Center.Y - _textSize.Y / 2
        );
    }
    
    public override void Destroy(Main g)
    {
        
    }
    
    public override void Update(GameTime gt, Main g)
    {
        if (bounds.Contains(Instance.MouseState.Position))
        {
            if (!MouseHover)
            {
                MouseHover = true;
                isAnyButtonHovered = true;
            }

            ButtonColor = Color.Gray;

            if (Instance.MouseState.LeftButton == ButtonState.Pressed)
            {
                isAnyButtonHovered = false;
                Click?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            if (MouseHover)
            {
                MouseHover = false;
                if (IsAnyButtonHovered())
                {
                    isAnyButtonHovered = false;
                }
            }

            ButtonColor = Color.White;
        }
    }

    public override void Draw(Main g)
    {
        if (visible)
        {
            Drawing.FillRect(bounds, ButtonColor, 0, g);
            
            Drawing.DrawText(_textPosition,
                             Drawing.Arial32, 
                             Color.Black, 
                             text, g);
        }
    }
    
    public static bool IsAnyButtonHovered()
    {
        return isAnyButtonHovered;
    }
}