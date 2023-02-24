using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Heldia;
public abstract class GameObject
{
    //Dimensions Variables
    public float x, y;
    public float xSpeed, ySpeed;
    public int width, height;

    public Vector2 position { get { return new Vector2(x, y); } set { x = value.X; y = value.Y; } }
    public Vector2 speed { get { return new Vector2(xSpeed, ySpeed); } set { xSpeed = value.X; ySpeed = value.Y; } }
    public Vector2 size { get { return new Vector2(width, height); } set { width = (int)value.X; height = (int)value.Y; } }
    public Rectangle bounds;
    public Rectangle devideSprite;

    //Properties
    public int id;
    public string text, tag;
    public bool rendered, visible;
    public bool collision, hover;

    //Sprite
    public Vector2 spritePos;

    public GameObject(int x, int y, int w, int h, int id)
    {
        //Dimensions
        this.x = x;
        this.y = y;
        this.width = w;
        this.height = h;
        this.bounds = new Rectangle(x, y, width, height);
        this.devideSprite = new Rectangle(0, 24, 16, 24);

        //Properties
        this.id = id;
        this.rendered = true;
        this.visible = true;
        this.collision = false;
        this.hover = false;
        this.spritePos = new Vector2(0, 0);
    }

    //Abstracts Methods
    public abstract void Init(Main g);
    public abstract void Destroy(Main g);
    public abstract void Update(GameTime gt, Main g);
    public abstract void Draw(Main g);

    //Sets
    public void SetPosition (float x, float y) { this.x = x; this.y = y; }
    public void SetSpeed (float xs, float ys) { this.xSpeed = xs; this.ySpeed = ys; }
    public void SetSize (int w, int h) { this.width = w; this.height = h; }
    public void SetScale(int s) { this.width = this.width * s; this.height = this.height * s ; }
    public void SetBounds(float x, float y, int w, int h) { this.bounds = new Rectangle((int)x, (int)y, w, h); }

    //gets
    public int GetId() { return id; }
    public float DistanceTo(Vector2 pos) { return Vector2.Distance(position, pos); }
    public Vector2 GetPositionCentered() { return new Vector2(x + (width / 2), y + (height / 2)); }
}