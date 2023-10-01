using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Engine.Enum;
using Heldia.Managers;
using Microsoft.Xna.Framework;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Objects.UI;

public class ItemCase : GameObject
{
    private Player _player;
    private int _caseId;
    public static int Width { get; set; } = 50;
    public static int Height { get; set; } = 50;

    private int _barNewWidth;

    private Color _outlineColor;
    private Color _inlineColor;

    public ItemCase(int caseId, int x, int y, Player player) : base(x, y, Width, Height, (int)EObjectId.ItemCase)
    {
        _player = player;
        _caseId = caseId;
        _outlineColor = Color.Black;
        _inlineColor = Color.Gray;
    }

    public override void Init(Main g, List<GameObject> objects)
    {
        
    }

    public override void Update(GameTime gt, Main g)
    {
        if (Instance.IsFullScreen)
        {
            Width *= Instance.GameScale / 2;
            Height *= Instance.GameScale / 2;
        }
        
        SetBounds(x, y, Width, Height);
        
        _barNewWidth = Width - 4;
    }

    public override  void Draw(GameTime gt, Main g)
    {
        Rectangle barNewRect = new Rectangle((int)x + 2, (int)y + 2, _barNewWidth, Height - 4);
        
        Drawing.FillRect(bounds, _outlineColor, 0.99f, g);
        Drawing.FillRect(barNewRect, _inlineColor, 0.99f, g);
        
        text = "" + _caseId;
        Vector2 textSize = Drawing.Ubuntu12.MeasureString(text);
        Vector2 textPosition = new Vector2(x + width - textSize.X, y);
        Drawing.DrawText(textPosition, Drawing.Ubuntu12, Color.White, text, 0.995f, g);
    }

    public override void Destroy(Main g)
    {
        
    }
}