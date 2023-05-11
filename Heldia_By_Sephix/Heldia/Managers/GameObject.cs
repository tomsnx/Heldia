using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Heldia.Managers;
public abstract class GameObject
{
    //Dimensions Variables
    public float x, y;
    public float xSpeed, ySpeed;
    public int width, height;

    public Vector2 Position { get { return new Vector2(x, y); } set { x = value.X; y = value.Y; } }
    public Vector2 Speed { get { return new Vector2(xSpeed, ySpeed); } set { xSpeed = value.X; ySpeed = value.Y; } }
    public Vector2 Size { get { return new Vector2(width, height); } set { width = (int)value.X; height = (int)value.Y; } }
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
        this.devideSprite = new Rectangle(0, 0, 0, 0);

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
    public abstract void Update(GameTime gt, Main g, List<GameObject> objects);
    public abstract void Draw(Main g);

    protected bool IsTouchingLeft(GameObject obj)
    {
        return this.bounds.Right + this.xSpeed > obj.bounds.Left &&
               this.bounds.Left < obj.bounds.Left &&
               this.bounds.Bottom > obj.bounds.Top &&
               this.bounds.Top < obj.bounds.Bottom;
    }

    protected bool IsTouchingRight(GameObject obj)
    {
        return this.bounds.Left + this.xSpeed < obj.bounds.Right &&
               this.bounds.Right > obj.bounds.Right &&
               this.bounds.Bottom > obj.bounds.Top &&
               this.bounds.Top < obj.bounds.Bottom;
    }

    protected bool IsTouchingTop(GameObject obj)
    {
        return this.bounds.Bottom + this.ySpeed > obj.bounds.Top &&
               this.bounds.Top < obj.bounds.Top &&
               this.bounds.Right > obj.bounds.Left &&
               this.bounds.Left < obj.bounds.Right;
    }

    protected bool IsTouchingBottom(GameObject obj)
    {
        return this.bounds.Top + this.ySpeed < obj.bounds.Bottom &&
               this.bounds.Bottom > obj.bounds.Bottom &&
               this.bounds.Right > obj.bounds.Left &&
               this.bounds.Left < obj.bounds.Right;
    }

    //Sets
    public void SetPosition (float x, float y) { this.x = x; this.y = y; }
    public void SetSpeed (float xs, float ys) { this.xSpeed = xs; this.ySpeed = ys; }
    public void SetSize (int w, int h) { this.width = w; this.height = h; }
    public void SetScale(float s) { this.width = (int)(this.width * s); this.height = (int)(this.height * s); }
    public void SetBounds(float x, float y, int w, int h) { this.bounds = new Rectangle((int)x, (int)y, w, h); }

    //gets
    public int GetId() { return id; }
    public float DistanceTo(Vector2 pos) { return Vector2.Distance(Position, pos); }
    public Vector2 GetPositionCentered() { return new Vector2(x + ((float)width / 2), y + ((float)height / 2)); }
}