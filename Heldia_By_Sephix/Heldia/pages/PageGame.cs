using System;
using System.Threading;
using Heldia.Engine;
using Heldia.Engine.Static;
using Heldia.Managers;
using Heldia.Objects;
using Heldia.Objects.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using static Heldia.Engine.Singleton.GameManager;
using Timer = Heldia.Engine.Timer;

namespace Heldia.Pages;

//TODO: Faire un système d'événement qui bouge la caméra à un endroit voulu lorsque le joueur
//TODO: arrive (par exemple) à un endroit précis de la map
//TODO: (faire en sorte que une fois terminé la caméra revienne sur le joueur)
public class PageGame : Page
{
    private Color _backgroundColor = Color.Azure;

    // Managers
    private PageManager _pageMgr;
    private ObjectManager _objMgr;
    
    // Objects
    private Map _map;
    private Player _player;
    
    private Managers.Overlay _hud;
    private LifeBar _lifeBar;
    private StaminaBar _staminaBar;
    private ItemBar _itemBar;

    private Camera _cam;

    private Timer _dayTimer;

    private Song _song;

    private Pointer _pointer;

    // Others
    private bool _escapePressed;

    /// <summary>
    /// States which display the game and made all calculations
    /// </summary>
    public PageGame(PageManager pageMgr) : base(PageId.Game)
    {
        _pageMgr = pageMgr;
        _objMgr = new ObjectManager();
        _player = new Player(1000, 1000);
        _cam = new Camera(new Vector2(0, 0));
        _map = new Map(Instance.GameScale);
        _lifeBar = new LifeBar(10, 10, _player);
        _staminaBar = new StaminaBar(10, LifeBar.barHeight * 2, _player);
        _hud = new Managers.Overlay(_player);
        _itemBar = new ItemBar((int)(Drawing.Width/(float)2 - ItemBar.barWidth/(float)2), 
                                Drawing.Height - ItemCase.Height - 2, 
                                 _player);

        _pointer = new Pointer();

        _dayTimer = new Timer(960, () =>
        {
            Instance.Day++;
        }, null,true);
    }

    public override void Init(Main g)
    {
        IsLoad = true;
        
        // Player
        _player.SetScale(Instance.GameScale);
        _objMgr.Add(_player, g);
        
        // HUD
        _hud.AddHudObject(_lifeBar);
        _hud.AddHudObject(_staminaBar);
        _hud.AddHudObject(_itemBar);
        
        // Map
        _map.Init(_objMgr ,g);
        
        //FIXME: System.IO.InvalidDataException: Could not determine container type!
        /*_song = g.Content.Load<Song>("sounds/test");
        MediaPlayer.Volume = 1.0f;
        MediaPlayer.Play(_song);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;*/
    }

    public override void Update(GameTime gt, Main g)
    {
        Instance.TotalGameTime += gt.ElapsedGameTime.TotalMilliseconds;
        
        var currentlyPressed = Instance.GameKb.GetKeyPressed(KeysList.escape);
        if (currentlyPressed && !_escapePressed)
        {
            if (Instance.SettingsMode) Instance.SettingsMode = false;
            else Instance.SettingsMode = true;
            
            _escapePressed = true;
        }
        else if (!currentlyPressed)
        {
            _escapePressed = false;
        }

        if (!Instance.SettingsMode)
        {
            _cam.Update(g);
            Instance.CameraPos = _player.GetPositionCentered();
            
            _map.Update(gt, g);
            
            _objMgr.Update(gt, g);

            _hud.Update(gt, g);

            _dayTimer.Update(gt);
        }
    }

    public override void Draw(GameTime gt, Main g)
    {
        if (IsLoad)
        {
            Drawing.Draw(gt);
            
            // Player
            g.GraphicsDevice.Clear(_backgroundColor);
        
            Drawing.spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, 
                                      transformMatrix: _cam.GetViewMatrix(), 
                                      samplerState: SamplerState.PointClamp);
        
            _objMgr.Draw(gt, g);

            Drawing.spriteBatch.End();
            
            
            // Static Object Drawing
            Drawing.spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _hud.Draw(gt, g);
            //_pointer.Draw(g);
            Drawing.spriteBatch.End();
        }
    }
    
    void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
    {
        // 0.0f is silent, 1.0f is full volume
        MediaPlayer.Volume -= 0.1f;
        MediaPlayer.Play(_song);
    }
}