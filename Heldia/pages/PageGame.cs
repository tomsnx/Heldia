using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia
{
    public class PageGame : Page
    {
        public ObjectManager objMgr = new ObjectManager();
        public Player player = new Player(150, 150);

        public Tree tree = new Tree(50, 50);
        public Tree tree2 = new Tree(50, 100);
        public Tree tree3 = new Tree(-50, 50);
        public Tree tree4 = new Tree(-50, 100);
        public Tree tree5 = new Tree(-150, 50);
        public Tree tree6 = new Tree(-150, 100);
        public Tree tree7 = new Tree(-250, 50);
        public Tree tree8 = new Tree(-250, 100);
        public Tree tree9 = new Tree(-350, 50);
        public Tree tree10 = new Tree(-350, 100);

        public Camera cam = new Camera(new Vector2(0, 0));

        public PageGame() : base(PageID.game)
        {
            
        }

        public override void Init(Main g)
        {
            tree.SetScale(4);
            objMgr.Add(tree, g);

            tree2.SetScale(4);
            objMgr.Add(tree2, g);

            tree3.SetScale(4);
            objMgr.Add(tree3, g);

            tree4.SetScale(4);
            objMgr.Add(tree4, g);

            tree5.SetScale(4);
            objMgr.Add(tree5, g);

            tree6.SetScale(4);
            objMgr.Add(tree6, g);

            tree7.SetScale(4);
            objMgr.Add(tree7, g);

            tree8.SetScale(4);
            objMgr.Add(tree8, g);

            tree9.SetScale(4);
            objMgr.Add(tree9, g);

            tree10.SetScale(4);
            objMgr.Add(tree10, g);

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
}
