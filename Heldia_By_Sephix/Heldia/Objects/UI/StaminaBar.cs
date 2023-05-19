using Heldia.Engine;
using Heldia.Managers;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Heldia.Objects.UI;
public class StaminaBar : GameObject
{
    private Player _player;

    // Définir la taille initiale de la barre du joueur
    public static int barWidth { get; set; } = 200;
    public static int barHeight { get; set; } = 20;

    private int _barNewWidth;

    private Color _outlineColor;
    private Color _inlineColor;

    public StaminaBar(int x, int y, Player player) : base(x, y, barWidth, barHeight, ObjectId.StaminaBar)
    {
        _player = player;
        _outlineColor = Color.Black;
        _inlineColor = Color.Green;
    }

    public override void Init(Main g)
    {
        
    }

    public override void Update(GameTime gt, Main g, List<GameObject> objects)
    {
        Vector2 playerPos = _player.GetPositionCentered();
        SetBounds(x, y, barWidth, barHeight);

        float staminaPercent = (float)_player.Stamina / 100;
        _barNewWidth = (int)((barWidth - 4) * staminaPercent);
    }

    public override  void Draw(Main g)
    {
        Rectangle barNewRect = new Rectangle((int)x + 2, (int)y + 2, _barNewWidth, barHeight - 4);
        
        Drawing.FillRect(bounds, _outlineColor, 1, g);
        Drawing.FillRect(barNewRect, _inlineColor, 1, g);
    }

    public override void Destroy(Main g)
    {
        
    }
}