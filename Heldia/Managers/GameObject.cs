using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Heldia
{
    public abstract class GameObject
    {
        //dimensions
        public float x, y;
        public float xSpeed, ySpeed;
        public int width, height;
        public int column, line;

        public Vector2 position { get { return new Vector2(x, y); } set { x = value.X; y = value.Y; } }
        public Vector2 speed { get { return new Vector2(xSpeed, ySpeed); } set { xSpeed = value.X; ySpeed = value.Y; } }
        public Vector2 size { get { return new Vector2(width, height); } set { width = (int)value.X; height = (int)value.Y; } }
        public Rectangle bounds;
        public Rectangle devideSprite;

        //properties
        public int id;
        public string text, tag;
        public bool rendered, visible;
        public bool collision, hover;

        //sprite
        public Vector2 spritePOS;

        public GameObject(int x, int y, int w, int h, int id)
        {
            //dimensions
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
            bounds = new Rectangle(x, y, width, height);
            devideSprite = new Rectangle(0, 24, 16, 24);

            //properties
            this.id = id;
            rendered = true;
            visible = true;
            collision = false;
            hover = false;
            spritePOS = new Vector2(0, 0);
        }

        // abstracts
        public abstract void Init(Main g);
        public abstract void Destroy(Main g);
        public abstract void Update(GameTime gt, Main g);
        public abstract void Draw(Main g);

        // sets
        public void SetPosition (float x, float y) { this.x = x; this.y = y; }
        public void SetSpeed (float xs, float ys) { this.xSpeed = xs; this.ySpeed = ys; }
        public void SetSize (int w, int h) { this.width = w; this.height = h; }
        public void SetScale(int s) { this.width = this.width * s; this.height = this.height * s ; }
        public void SetBounds(float x, float y, int w, int h) { this.bounds = new Rectangle((int)x, (int)y, w, h); }

        //gets
        public int GetID() { return id; }
        public float DistanceTo(Vector2 pos) { return Vector2.Distance(position, pos); }
        public Vector2 GetPositionCentered() { return new Vector2(x + (width / 2), y + (height / 2)); }
    }
}
