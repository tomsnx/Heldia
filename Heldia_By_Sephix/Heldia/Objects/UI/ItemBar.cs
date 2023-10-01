using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Engine.Enum;
using Heldia.Managers;
using Microsoft.Xna.Framework;

namespace Heldia.Objects.UI;

public class ItemBar : GameObject
{
    public static float barWidth = 10 * ItemCase.Width;
    public static float barHeight = ItemCase.Height;

    private Player _player;

    private ItemCase[] _itemCaseList;
    private CurrentSlot _currentSlot;

    public ItemBar(int x, int y, Player player) : base(x, y, (int)barWidth, (int)barHeight, (int)EObjectId.ItemBar)
    {
        _player = player;
        _itemCaseList = new ItemCase[10];
        _currentSlot = new CurrentSlot(x, y, player);

        for(int i = 0; i < 10; i++) {
            _itemCaseList[i] = new ItemCase(i+1, x + i * ItemCase.Width + (2 * i), y + 0, player);
        }
    }

    public override void Init(Main g, List<GameObject> objects)
    {
        
    }

    public override void Update(GameTime gt, Main g)
    {
        _currentSlot.Update(gt, g);
        foreach (var item in _itemCaseList)
        {
            item.Update(gt, g);
        }
    }

    public override void Draw(GameTime gt, Main g)
    {
        foreach (var item in _itemCaseList)
        {
            item.Draw(gt, g);
        }
        _currentSlot.Draw(gt, g);
    }

    public override void Destroy(Main g)
    {
        
    }
}