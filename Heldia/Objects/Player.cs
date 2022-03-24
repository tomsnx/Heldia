using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Heldia
{
    public class Player : GameObject
    {
        //input
        KeyboardState kb;

        // speeds
        public float walkSpeed = 350f;

        //Sprite
        SpriteSheets sprite;
        Animation anim;

        // texture name
        string name = "sprite";

        public static int pWidth = 37;
        public static int pHeight = 45;

        public static int mY = 1;
        
        public Player(int x, int y) : base(x, y, pWidth, pHeight, ObjectID.player)
        {
            
        }

        public override void Init(Main g)
        {
            anim = new Animation();
        }
        public override void Destroy(Main g)
        {
            
        }

        public override void Update(GameTime gt, Main g)
        {
            // input
            kb = Keyboard.GetState();
            MovementInput(g);

            // move
            x += xSpeed;
            y += ySpeed;

            anim.GetAnimRect(16, 24, gt);
            sprite = new SpriteSheets(g, ObjectID.player, name);
            devideSprite = anim.rect;
        }

        public override void Draw(Main g)
        {
            Drawing.FillRect(sprite.GetSheet(), devideSprite, bounds, Color.White, 0, g);
        }

        private void MovementInput(Main g)
        {
            float spd = walkSpeed * Drawing.delta;

            //pressed
            if (kb.IsKeyDown(Keys.Z)) { ySpeed = -spd; }
            if (kb.IsKeyDown(Keys.Q)) { xSpeed = -spd; }
            if (kb.IsKeyDown(Keys.S)) { ySpeed = spd; }
            if (kb.IsKeyDown(Keys.D)) { xSpeed = spd; mY = 2; }

            //released
            if (kb.IsKeyUp(Keys.Z) && kb.IsKeyUp(Keys.S)) { ySpeed = 0; }
            if (kb.IsKeyUp(Keys.Q) && kb.IsKeyUp(Keys.D)) { xSpeed = 0; }

            if (kb.IsKeyUp(Keys.D)) { mY = 1; }
        }
    }
}
