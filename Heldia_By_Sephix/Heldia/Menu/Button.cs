using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Heldia.Menu;
public class Button : GameObject
{
    // Button Texture
    private Texture2D texture;

    // Button Click Event
    public event EventHandler Click;

    /*private String text;
    private SpriteFont font;
    private Color textColor;*/

    /*public Button(int x, int y, int w, int h, int id, string text, SpriteFont font , Color textColor) : base(x, y, w, h, id)
    {
        visible = true;
        this.text = text;
        this.font = font;
        this.textColor = textColor;
    }*/
    
    public Button(int x, int y, int w, int h, int id) : base(x, y, w, h, id)
    {
        visible = true;
        /*this.text = text;
        this.font = font;
        this.textColor = textColor;*/
    }

    public override void Init(Main g)
    {
        // You can initialize the button here
    }

    public override void Destroy(Main g)
    {
        // You can destroy the button here
    }

    public override void Update(GameTime gt, Main g, List<GameObject> objects)
    {
        // Check if the button is being clicked
        if (bounds.Contains(Mouse.GetState().Position))
        {
            hover = true;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Click?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            hover = false;
        }
    }

    public override void Draw(Main g)
    {
        // Draw the button texture
        if (visible)
        {
            Color color = Color.White;
            if (hover)
            {
                color = Color.Gray;
            }
            Drawing.FillRect(bounds, color, 0, g);
            //Drawing.FillRect(bounds, color, 0, font, textColor, text, g);
        }
    }
}