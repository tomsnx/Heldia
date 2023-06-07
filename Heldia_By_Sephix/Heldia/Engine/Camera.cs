using Heldia.Engine;
using Microsoft.Xna.Framework;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Engine;
public class Camera
{
    // Properties
    public Vector2 position;
    public Matrix Transform { get; private set; }
    public float CameraDelay { get; set; } = 1.0f;
    
    public Rectangle CameraBounds { get; set; }

    // Constructor
    public Camera(Vector2 pos) { this.position = pos; }

    // Update

    public void Update(Vector2 pos, Main g)
    {
        float d = Instance.CameraDelay * Drawing.delta * CameraDelay;

        // Move
        position.X += ((pos.X - position.X) - (float)Drawing.Width / 2) * d;
        position.Y += ((pos.Y - position.Y) - (float)Drawing.Height / 2) * d;
        
        // Update camera bounds
        CameraBounds = new Rectangle((int)position.X, (int)position.Y, Drawing.Width, Drawing.Height);

        Transform = Matrix.CreateTranslation((int)-position.X, -position.Y, 0);
        
        Instance.Camera = this;
    }

    public Matrix GetViewMatrix()
    {
        return Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0));
    }
}
