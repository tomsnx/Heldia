using System;
using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Managers;
using Heldia.Pages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Heldia.Menu;
public class Button : GameObject
{
    // Button Texture
    private Texture2D texture;

    private Vector2 _textSize;
    private Vector2 _textPosition;

    // Button Click Event
    public event EventHandler Click;

    // TODO: Mettre du text dans les boutons
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
    
    // Constructor
    public Button(int x, int y, int w, int h, int id, String text) : base(x, y, w, h, id)
    {
        visible = true;
        this.text = text;
    }
    
    // Init
    public override void Init(Main g)
    {
        _textSize = Drawing.Arial32.MeasureString(text);
        _textPosition = new Vector2(
            bounds.Center.X - _textSize.X / 2,
            bounds.Center.Y - _textSize.Y / 2
        );
    }

    // Destroy
    public override void Destroy(Main g)
    {
        
    }

    // Update states of button, check if is clicked...
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

    // Draw button
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
            
            Drawing.DrawText(_textPosition,
                Drawing.Arial32, Color.Black, 
                text, g);
        }
    }
}