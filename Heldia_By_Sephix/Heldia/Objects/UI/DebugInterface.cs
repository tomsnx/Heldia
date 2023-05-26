using System;
using Heldia.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Objects.UI;

public class DebugInterface
{
    private Player _player;
    private int _hudSpacingRight;
    private String _text;
    private Vector2 _playerPos;
    private SpriteFont _ubuntu12;

    public DebugInterface(Player player)
    {
        _player = player;
        _hudSpacingRight = 10;
        _ubuntu12 = Drawing.Ubuntu12;
    }

    public void Init(Main g)
    {
        
    }

    public void Update(GameTime gt, Main g)
    {
        
    }

    public void Draw(Main g)
    {
        _text = "DebugMode";
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacingRight - 10, 20), _ubuntu12, Color.Black, _text, g);
        
        _text = "FPS : " + Drawing.FpsUpdate;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacingRight, 40), _ubuntu12, Color.Black, _text, g);

        _text = "Player - X : " + Instance.PlayerX;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacingRight, 80), _ubuntu12, Color.Black, _text, g);
        
        _text = "Player - Y : " + Instance.PlayerY;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacingRight, 100), _ubuntu12, Color.Black, _text, g);
        
        _text = "StaminaDownToZero : " + _player.StaminaDownToZero ;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacingRight, 120), _ubuntu12, Color.Black, _text, g);
        
        _text = "Clock (sec): " + (int)Instance.TotalGameTime / 1000 ;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacingRight, 140), _ubuntu12, Color.Black, _text, g);

        _text = "Speed : " + Math.Round((_player.Speed.Length()), 1);
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacingRight, 160), _ubuntu12, Color.Black, _text, g);
    }
}