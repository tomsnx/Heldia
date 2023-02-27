using Microsoft.Xna.Framework;

namespace Heldia;
public class Camera
{
    // Properties
    public Vector2 position;
    public Matrix transform { get; private set; }
    public float delay { get; set; } = 10.0f;

    // Constructor
    public Camera(Vector2 pos) { this.position = pos; }

    // Update

    public void Update(Vector2 pos, Main g)
    {
        float d = delay * Drawing.delta;

        // Move
        position.X += ((pos.X - position.X) - Drawing.width / 2) * d;
        position.Y += ((pos.Y - position.Y) - Drawing.height / 2) * d;

        transform = Matrix.CreateTranslation((int)-position.X, -position.Y, 0);
    }
}
