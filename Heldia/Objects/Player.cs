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

        // texture name
        string name = "whiteherst";
        
        public Player(int x, int y) : base(x, y, 37, 45, ObjectID.player)
        {

        }

        public override void Init(Main g)
        {

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

            sprite = new SpriteSheets(g, ObjectID.player, name);
        }

        public override void Draw(Main g)
        {
            Drawing.FillRect(sprite.GetSheet(), bounds, Color.White, 0, g);
        }

        private void MovementInput(Main g)
        {
            float spd = walkSpeed * Drawing.delta;

            //pressed
            if (kb.IsKeyDown(Keys.Z)) { ySpeed = -spd; }
            if (kb.IsKeyDown(Keys.Q)) { xSpeed = -spd; }
            if (kb.IsKeyDown(Keys.S)) { ySpeed = spd; }
            if (kb.IsKeyDown(Keys.D)) { xSpeed = spd; }

            //released
            if (kb.IsKeyUp(Keys.Z) && kb.IsKeyUp(Keys.S)) { ySpeed = 0; }
            if (kb.IsKeyUp(Keys.Q) && kb.IsKeyUp(Keys.D)) { xSpeed = 0; }
        }
    }
}
