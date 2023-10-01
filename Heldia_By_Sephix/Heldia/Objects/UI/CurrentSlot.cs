using System.Collections.Generic;
using System.Linq.Expressions;
using Heldia.Engine;
using Heldia.Engine.Enum;
using Heldia.Managers;
using Microsoft.Xna.Framework;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Objects.UI;

public class CurrentSlot : GameObject
{
    private Player _player;

    private int _itemBarX;
    private int _barNewWidth;

    private Color _outlineColor;
    private Color _inlineColor;

    public CurrentSlot(int x, int y, Player player) : base(x, y, ItemCase.Width, ItemCase.Height, (int)EObjectId.ItemSlot)
    {
        _player = player;

        _itemBarX = x;
        
        _outlineColor = Color.Black;
        _inlineColor = Color.Blue;
    }

    public override void Init(Main g, List<GameObject> objects)
    {
        
    }

    public override void Update(GameTime gt, Main g)
    {
        if (Instance.IsFullScreen)
        {
            width *= Instance.GameScale / 2;
            height *= Instance.GameScale / 2;
        }
        
        SetPosition(_itemBarX + _player.CurrentSlot * width + 2 * _player.CurrentSlot, y);

        SetBounds(x, y, width, height);
        
        _barNewWidth = width - 4;
    }

    public override void Draw(GameTime gt, Main g)
    {
        Rectangle barNewRect = new Rectangle((int)x + 2, (int)y + 2, _barNewWidth, height - 4);
        
        Drawing.FillRect(bounds, _outlineColor, 0.5f, g);
        Drawing.FillRect(barNewRect, _inlineColor, 0.5f, g);
    }

    public override void Destroy(Main g)
    {
        
    }
}