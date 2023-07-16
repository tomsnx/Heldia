using Heldia.Engine;
using Heldia.Managers;
using System.Collections.Generic;
using Heldia.Engine.Enum;
using Microsoft.Xna.Framework;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Objects.UI;
public class StaminaBar : GameObject
{
    private Player _player;

    // Définir la taille initiale de la barre du joueur
    public static int barWidth { get; set; } = (int)Instance.PlayerMaxStamina * 2;
    public static int barHeight { get; set; } = 20;

    private int _barNewWidth;

    private Color _outlineColor;
    private Color _inlineColor;

    public StaminaBar(int x, int y, Player player) : base(x, y, barWidth, barHeight, (int)EObjectId.StaminaBar)
    {
        _player = player;
        _outlineColor = Color.Black;
        _inlineColor = Color.Green;
    }

    public override void Init(Main g, List<GameObject> objects)
    {
        
    }

    public override void Update(GameTime gt, Main g)
    {
        if (Instance.IsFullScreen)
        {
            barWidth *= Instance.GameScale / 2;
            barHeight *= Instance.GameScale / 2;
        }
        
        SetBounds(x, y, barWidth, barHeight);

        float staminaPercent = (float)Instance.PlayerStamina / Instance.PlayerMaxStamina;
        _barNewWidth = (int)((barWidth - 4) * staminaPercent);
    }

    public override  void Draw(Main g)
    {
        Rectangle barNewRect = new Rectangle((int)x + 2, (int)y + 2, _barNewWidth, barHeight - 4);
        
        Drawing.FillRect(bounds, _outlineColor, 1, g);
        Drawing.FillRect(barNewRect, _inlineColor, 1, g);
        
        text = "" + (int)Instance.PlayerStamina;
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