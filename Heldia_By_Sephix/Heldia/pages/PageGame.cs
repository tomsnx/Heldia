using Heldia.Engine;
using Heldia.Managers;
using Heldia.Objects;
using Heldia.Objects.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Overlay = Heldia.Managers.Overlay;

namespace Heldia.Pages;
public class PageGame : Page
{
    Color _backgroundColor = Color.Azure;

    public ObjectManager objMgr;
    public Map map;
    public Player player;
    
    private Overlay _overlay;
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
        _overlay = new Overlay(cam);
    }

    public override void Init(Main g)
    {
        // Map
        map.Init(objMgr ,g);
        
        // Player
        player.SetScale(4);
        objMgr.Add(player, g);
        
        // UI
        _overlay.AddUiObject(_lifeBar);
        _overlay.AddUiObject(_staminaBar);
    }

    public override void Update(GameTime gt, Main g)
    {
        cam.Update(player.GetPositionCentered(), g);
        player.GetPositionCentered();
        objMgr.Update(gt, g);
        _overlay.Update(gt, g);
    }

    public override void Draw(Main g)
    {
        // Player
        g.GraphicsDevice.Clear(_backgroundColor);
        Drawing.spriteBatch.Begin(transformMatrix: cam.GetViewMatrix(), samplerState: SamplerState.PointClamp);
        objMgr.Draw(g);

        // end
        Drawing.spriteBatch.End();
        
        
        // Static Object Drawing
        Drawing.spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _overlay.Draw(g);
        Drawing.spriteBatch.End();
    }

    public override void Destroy(Main g)
    {
        
    }
}