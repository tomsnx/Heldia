using System;
using Heldia.Engine;
using Heldia.Managers;
using Heldia.Objects;
using Heldia.Objects.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Pages;
public class PageGame : Page
{
    Color _backgroundColor = Color.Azure;

    private ObjectManager _objMgr;
    
    private Map _map;
    private Player _player;
    
    private Managers.Overlay _hud;
    private LifeBar _lifeBar;
    private StaminaBar _staminaBar;

    private Camera _cam;

    private Timer _dayTimer;

    private Song _song;

    private Pointer _pointer;
    
    // Others
    private bool escapePressed;
    
    //TODO: Faire une variable KeyBoard pour tous le jeux et pas une private à chaque fois
    private KeyboardState _kb;

    /// <summary>
    /// States which display the game and made all calculations
    /// </summary>
    public PageGame() : base(PageId.Game)
    {
        _objMgr = new ObjectManager();
        _player = new Player(500, 500);
        _cam = new Camera(new Vector2(0, 0));
        _map = new Map(Instance.GameScale);
        _lifeBar = new LifeBar(10, 10, _player);
        _staminaBar = new StaminaBar(10, LifeBar.barHeight * 2, _player);
        _hud = new Managers.Overlay(_player);

        _pointer = new Pointer();

        _dayTimer = new Timer(960, () =>
        {
            Instance.Day++;
        }, true);
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
        
        // Map
        _map.Init(_objMgr ,g);
        
        //FIXME: Mettre le fichier test en .xnb
        /*_song = g.Content.Load<Song>("Sounds/test");
        MediaPlayer.Play(_song);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;*/
    }

    public override void Update(GameTime gt, Main g)
    {
        Instance.TotalGameTime += gt.ElapsedGameTime.TotalMilliseconds;
        
        var currentlyPressed = Instance.Kb.IsKeyDown(Keys.Escape);
        if (currentlyPressed && !escapePressed)
        {
            if (Instance.SettingsMode) Instance.SettingsMode = false;
            else Instance.SettingsMode = true;
            
            escapePressed = true;
        }
        else if (!currentlyPressed)
        {
            escapePressed = false;
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
        
            _objMgr.Draw(g);

            Drawing.spriteBatch.End();

            
            // Static Object Drawing
            Drawing.spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _hud.Draw(g);
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