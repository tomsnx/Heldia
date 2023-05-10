using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia;
public class PageGame : Page
{
    public ObjectManager objMgr = new ObjectManager();
    public Map map = new Map(2);
    public Player player = new Player(-100, -100);

    public Camera cam = new Camera(new Vector2(0, 0));

    public PageGame() : base(PageID.game)
    {
        
    }

    public override void Init(Main g)
    {
        map.Init(objMgr ,g);

        player.SetScale(2);
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
        Drawing.spriteBatch.Begin(transformMatrix: cam.transform, samplerState: SamplerState.PointClamp);

        //objs
        objMgr.Draw(g);

        // end
        Drawing.spriteBatch.End();
    }
}