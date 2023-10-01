using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Heldia.Managers;
public abstract class GameObject
{
    public bool Active { get; set; } = false;
    
    //Dimensions Variables
    public float x, y;
    protected float xSpeed, ySpeed;
    public int width, height;

    public Vector2 Position { get { return new Vector2(x, y); } set { x = value.X; y = value.Y; } }

    public Vector2 Speed
    {
        get
        {
            Vector2 vector = new Vector2(xSpeed, ySpeed);

            return vector;
        }
        set
        {
            xSpeed = value.X; 
            ySpeed = value.Y;
        }
    }
    protected Vector2 Size { get { return new Vector2(width, height); } set { width = (int)value.X; height = (int)value.Y; } }
    protected Rectangle bounds;
    protected Rectangle collisionBounds;
    protected Rectangle devideSprite;

    //Properties
    protected int id;
    protected string text, tag;
    public bool rendered, visible;
    protected bool collision = false, hover;

    //Sprite
    protected Vector2 spritePos;

    public GameObject(int x, int y, int w, int h, int id)
    {
        //Dimensions
        this.x = x;
        this.y = y;
        this.width = w;
        this.height = h;
        this.bounds = new Rectangle(x, y, (int)width, (int)height);
        this.collisionBounds = bounds;
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
    public abstract void Init(Main g, List<GameObject> objects);
    public abstract void Destroy(Main g);
    public abstract void Update(GameTime gt, Main g);
    public abstract void Draw(GameTime gt, Main g);

    public bool IsTouchingLeft(GameObject obj)
    {
        return this.collisionBounds.Right + this.Speed.X > obj.bounds.Left &&
               this.collisionBounds.Left < obj.bounds.Left &&
               this.collisionBounds.Bottom > obj.bounds.Top &&
               this.collisionBounds.Top < obj.bounds.Bottom;
    }

    public bool IsTouchingRight(GameObject obj)
    {
        return this.collisionBounds.Left + this.Speed.X < obj.bounds.Right &&
               this.collisionBounds.Right > obj.bounds.Right &&
               this.collisionBounds.Bottom > obj.bounds.Top &&
               this.collisionBounds.Top < obj.bounds.Bottom;
    }

    public bool IsTouchingTop(GameObject obj)
    {
        return this.collisionBounds.Bottom + this.Speed.Y > obj.bounds.Top &&
               this.collisionBounds.Top < obj.bounds.Top &&
               this.collisionBounds.Right > obj.bounds.Left &&
               this.collisionBounds.Left < obj.bounds.Right;
    }

    public bool IsTouchingBottom(GameObject obj)
    {
        return this.collisionBounds.Top + this.Speed.Y < obj.bounds.Bottom &&
               this.collisionBounds.Bottom > obj.bounds.Bottom &&
               this.collisionBounds.Right > obj.bounds.Left &&
               this.collisionBounds.Left < obj.bounds.Right;
    }

    //Sets
    public void SetPosition (float x, float y) { this.x = x; this.y = y; }
    public void SetSpeed (float xs, float ys) { xSpeed = xs; ySpeed = ys; }
    public void SetSize (int w, int h) { this.width = w; this.height = h; }
    public void SetScale(float s) { this.width = (int)(this.width * s); this.height = (int)(this.height * s); }
    public void SetBounds(float x, float y, int w, int h) { this.bounds = new Rectangle((int)x, (int)y, w, h); }
    public void SetCollisionBounds(float x, float y, int w, int h) { this.collisionBounds = new Rectangle((int)x, (int)y, w, h); }

    //gets
    public int GetId() { return id; }
    
    /// <summary>
    /// Give you if the object is collide or not.
    /// </summary>
    /// <returns>True if object is collide else False</returns>
    public bool GetCollisionState() { return collision; }
    public float DistanceTo(Vector2 pos) { return Vector2.Distance(Position, pos); }
    public Vector2 GetPositionCentered() { return new Vector2(x + (float)width / 2, y + (float)height / 2); }
}