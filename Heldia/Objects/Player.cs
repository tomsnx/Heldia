using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Heldia
{
    public class Player : GameObject
    {
        // input
        KeyboardState kb;

        // speeds
        public float walkSpeed = 350f;

        // sprite
        SpriteSheets sprite;

        // animation
        Animation anim;

        // texture name
        string name = "sprite";

        // set dimensions
        public static int playerWidth = 37;
        public static int playerHeight = 45;

        // init the started animation
        public static int column = 3; // Started to count on column 0
        public static int line = 1; // Started on line 0 but here is useless
        
        public Player(int x, int y) : base(x, y, playerWidth, playerHeight, ObjectID.player)
        {
            
        }

        public override void Init(Main g)
        {
            // init anim
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

            // Set variable devideSprite to a X and Y Value of the TileSet
            anim.GetAnimRect(16, 24, gt);
            devideSprite = anim.rect;

            sprite = new SpriteSheets(g, ObjectID.player, name);
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
            if (kb.IsKeyDown(Keys.Q)) { xSpeed = -spd; line = 2; }
            if (kb.IsKeyDown(Keys.S)) { ySpeed = spd; }
            if (kb.IsKeyDown(Keys.D)) { xSpeed = spd; line = 2; }

            //released
            if (kb.IsKeyUp(Keys.Z) && kb.IsKeyUp(Keys.S)) { ySpeed = 0; }
            if (kb.IsKeyUp(Keys.Q) && kb.IsKeyUp(Keys.D)) { xSpeed = 0; }

            // set idle animation with line here :
            if (kb.IsKeyUp(Keys.Q) && kb.IsKeyUp(Keys.D)) { line = 1; }
        }
    }
}
