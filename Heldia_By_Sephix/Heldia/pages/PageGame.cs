using System;
using Heldia.Engine;
using Heldia.Managers;
using Heldia.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia.Pages;
public class PageGame : Page
{
    public ObjectManager objMgr;
    public Map map;
    public Player player;

    public Camera cam;

    public PageGame() : base(PageId.Game)
    {
        objMgr = new ObjectManager();
        map = new Map();
        player = new Player(-100, -100);
        cam = new Camera(new Vector2(0, 0));
    }

    public override void Init(Main g)
    {
        map.Init(objMgr ,g);

        player.SetScale(4);
        objMgr.Add(player, g);
    }

    public override void Update(GameTime gt, Main g)
    {
        cam.Update(player.GetPositionCentered(), g);
        player.GetPositionCentered();
        objMgr.Update(gt, g);
    }

    public override void Draw(Main g)
    {
        //clear
        g.GraphicsDevice.Clear(Color.Green);

        // begin
        Drawing.spriteBatch.Begin(transformMatrix: cam.Transform, samplerState: SamplerState.PointClamp);

        //objs
        objMgr.Draw(g);

        // end
        Drawing.spriteBatch.End();
    }

    public override void Destroy(Main g)
    {
        
    }
}