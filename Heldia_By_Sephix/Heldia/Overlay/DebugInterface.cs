using System;
using Heldia.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Objects.UI;

public class DebugInterface
{
    private Player _player;
    private int _hudSpacing;
    private String _text;
    private Vector2 _playerPos;
    private SpriteFont _ubuntu12;

    public DebugInterface(Player player)
    {
        _player = player;
        _hudSpacing = 10;
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
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacing - 10, 20), _ubuntu12, Color.Black, _text, g);
        
        _text = "TPS : " + Drawing.TpsUpdate;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacing, 40), _ubuntu12, Color.Black, _text, g);
        
        _text = "FPS : " + Drawing.FpsDraw;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacing, 60), _ubuntu12, Color.Black, _text, g);

        _text = "Player - X : " + Instance.PlayerX;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacing, 80), _ubuntu12, Color.Black, _text, g);
        
        _text = "Player - Y : " + Instance.PlayerY;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacing, 100), _ubuntu12, Color.Black, _text, g);
        
        _text = "Player - TilesX : " + Instance.PlayerTileX;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacing, 120), _ubuntu12, Color.Black, _text, g);
        
        _text = "Player - TilesY : " + Instance.PlayerTileY;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacing, 140), _ubuntu12, Color.Black, _text, g);
        
        _text = "StaminaDownToZero : " + _player.StaminaDownToZero ;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacing, 160), _ubuntu12, Color.Black, _text, g);
        
        _text = "Clock (sec): " + (int)Instance.TotalGameTime / 1000 ;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacing, 180), _ubuntu12, Color.Black, _text, g);
        
        _text = "Day: " + Instance.Day;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacing, 200), _ubuntu12, Color.Black, _text, g);
        
        _text = "Speed : " + Math.Round(_player.Speed.Length(), 1);
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(Drawing.Width - _playerPos.X -  _hudSpacing, 220), _ubuntu12, Color.Black, _text, g);

        _text = "--- Direction ---";
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(_hudSpacing, 240), _ubuntu12, Color.Black, _text, g);
        
        _text = "North : " + Player.north;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(_hudSpacing, 260), _ubuntu12, Color.Black, _text, g);
        
        _text = "East : " + Player.east;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(_hudSpacing, 280), _ubuntu12, Color.Black, _text, g);
        
        _text = "South : " + Player.south;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(_hudSpacing, 300), _ubuntu12, Color.Black, _text, g);
        
        _text = "West : " + Player.west;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2( _hudSpacing, 320), _ubuntu12, Color.Black, _text, g);
        
        _text = "--- Camera ---";
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(_hudSpacing, 340), _ubuntu12, Color.Black, _text, g);
        
        _text = "Pos X: " + Math.Round(Instance.Camera.GetPositionCentered().X, 1);
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(_hudSpacing, 360), _ubuntu12, Color.Black, _text, g);
        
        _text = "Pos Y: " + Math.Round(Instance.Camera.GetPositionCentered().Y, 1);
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(_hudSpacing, 380), _ubuntu12, Color.Black, _text, g);
        
        _text = "Delay " + Instance.Camera.CameraDelay;
        _playerPos = _ubuntu12.MeasureString(_text);
        Drawing.DrawText(new Vector2(_hudSpacing, 400), _ubuntu12, Color.Black, _text, g);
    }
}