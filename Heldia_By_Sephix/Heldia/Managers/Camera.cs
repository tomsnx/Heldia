using Heldia.Engine;
using Microsoft.Xna.Framework;

namespace Heldia.Managers;
public class Camera
{
    // Properties
    public Vector2 position;
    public Matrix Transform { get; private set; }
    public float Delay { get; set; } = 10.0f;

    // Constructor
    public Camera(Vector2 pos) { this.position = pos; }

    // Update

    public void Update(Vector2 pos, Main g)
    {
        float d = Delay * Drawing.delta;

        // Move
        position.X += ((pos.X - position.X) - (float)Drawing.Width / 2) * d;
        position.Y += ((pos.Y - position.Y) - (float)Drawing.Height / 2) * d;

        Transform = Matrix.CreateTranslation((int)-position.X, -position.Y, 0);
    }

    public Matrix GetViewMatrix()
    {
        return Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0));
    }
}
