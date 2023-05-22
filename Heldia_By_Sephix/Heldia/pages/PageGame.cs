using Heldia.Engine;
using Heldia.Managers;
using Heldia.Objects;
using Heldia.Objects.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overlay = Heldia.Managers.Overlay;

namespace Heldia.Pages;
public class PageGame : Page
{
    Color _backgroundColor = Color.Azure;

    public ObjectManager objMgr;
    public Map map;
    public Player player;
    
    private Overlay _hud;
    private LifeBar _lifeBar;
    private StaminaBar _staminaBar;

    public Camera cam;

    /// <summary>
    /// States which display the game and made all calculations
    /// </summary>
    public PageGame() : base(PageId.Game)
    {
        objMgr = new ObjectManager();
        map = new Map();
        player = new Player(-100, -100);
        cam = new Camera(new Vector2(0, 0));
        _lifeBar = new LifeBar(10, 10, player);
        _staminaBar = new StaminaBar(10, LifeBar.barHeight * 2, player);
        _hud = new Overlay(cam, player);
    }

    public override void Init(Main g)
    {
        // Map
        map.Init(objMgr ,g);
        
        // Player
        player.SetScale(4);
        objMgr.Add(player, g);
        
        // HUD
        _hud.AddHudObject(_lifeBar);
        _hud.AddHudObject(_staminaBar);
        
        // HUD (Debug)
    }

    public override void Update(GameTime gt, Main g)
    {
        cam.Update(player.GetPositionCentered(), g);
        player.GetPositionCentered();
        objMgr.Update(gt, g);
        _hud.Update(gt, g);
    }

    public override void Draw(Main g)
    {
        // Player
        g.GraphicsDevice.Clear(_backgroundColor);
        
        Drawing.spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, 
                                  transformMatrix: cam.GetViewMatrix(), 
                                  samplerState: SamplerState.PointClamp);
        
        objMgr.Draw(g);
        
        Drawing.spriteBatch.End();
        
        
        // Static Object Drawing
        Drawing.spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _hud.Draw(g);
        Drawing.spriteBatch.End();
    }

    public override void Destroy(Main g)
    {
        
    }
}