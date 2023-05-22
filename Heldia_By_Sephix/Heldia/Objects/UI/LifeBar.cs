using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Heldia.Engine;
using Heldia.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Heldia.Engine.GameManager;

namespace Heldia.Objects.UI;

public class LifeBar : GameObject
{
    private Player _player;

    // Définir la taille initiale de la barre du joueur
    public static int barWidth { get; set; } = 200;
    public static int barHeight { get; set; } = 20;

    private int _barNewWidth;

    private Color _outlineColor;
    private Color _inlineColor;

    public LifeBar(int x, int y, Player player) : base(x, y, barWidth, barHeight, ObjectId.LifeBar)
    {
        _player = player;
        _outlineColor = Color.Black;
        _inlineColor = Color.Red;
    }

    public override void Init(Main g)
    {
        
    }

    public override void Update(GameTime gt, Main g, List<GameObject> objects)
    {
        SetBounds(x, y, barWidth, barHeight);

        float viePercent = (float)Instance.PlayerLife / 100;
        _barNewWidth = (int)((barWidth - 4) * viePercent);
    }

    public override  void Draw(Main g)
    {
        Rectangle barNewRect = new Rectangle((int)x + 2, (int)y + 2, _barNewWidth, barHeight - 4);
        
        Drawing.FillRect(bounds, _outlineColor, 1, g);
        Drawing.FillRect(barNewRect, _inlineColor, 1, g);

        text = "" + (int)Instance.PlayerLife;
        Vector2 textSize = Drawing.PixExtrusion12.MeasureString(text);
        Vector2 textPosition = new Vector2(
            bounds.Center.X - textSize.X / 2,
            bounds.Center.Y - textSize.Y / 2
        );
        Drawing.DrawText(textPosition,
                         Drawing.PixExtrusion12, Color.White, 
                         text, g);
    }

    public override void Destroy(Main g)
    {
        
    }
}