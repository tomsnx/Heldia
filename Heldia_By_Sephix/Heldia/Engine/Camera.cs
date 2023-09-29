using Microsoft.Xna.Framework;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Engine;
public class Camera
{
    // Properties
    public static Vector2 position;
    public Matrix Transform { get; private set; }
    public float CameraDelay { get; set; } = 1.0f;
    public Rectangle CameraBounds { get; set; }

    // Constructor
    public Camera(Vector2 pos)
    {
        position = pos;
    }
    
    public void Update(Main g)
    {
        float d = Instance.CameraDelay * Drawing.delta * CameraDelay;

        // Move
        position.X += (Instance.CameraPos.X - position.X - (float)Drawing.Width / 2) * d;
        position.Y += (Instance.CameraPos.Y - position.Y - (float)Drawing.Height / 2) * d;
        
        // Update camera bounds
        CameraBounds = new Rectangle((int)position.X, (int)position.Y, Drawing.Width, Drawing.Height);

        Transform = Matrix.CreateTranslation((int)-position.X, -position.Y, 0);

        int syncCam = 50; // Space in which the CameraDelay is fixed to 1 to synchronise the play in the Camera
        int smoothAnim = 400; // Smooth animation around player for beauty

        if (GetPositionCentered().X >= Instance.PlayerX - syncCam &&
            GetPositionCentered().X <= Instance.PlayerX + syncCam &&
            GetPositionCentered().Y >= Instance.PlayerY - syncCam &&
            GetPositionCentered().Y <= Instance.PlayerY + syncCam || 
            Instance.Sprint)
        {
            CameraDelay = 1f;
        }
        else if (GetPositionCentered().X >= Instance.PlayerX - smoothAnim &&
                 GetPositionCentered().X <= Instance.PlayerX + smoothAnim &&
                 GetPositionCentered().Y >= Instance.PlayerY - smoothAnim &&
                 GetPositionCentered().Y <= Instance.PlayerY + smoothAnim)
        {
            CameraDelay = 0.2f;
        }
        
        Instance.Camera = this;
    }

    // Gets
    public Matrix GetViewMatrix()
    {
        return Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0));
    }

    public Vector2 GetPosition()
    {
        return position;
    }
    
    public Vector2 GetPositionCentered()
    {
        return new Vector2(position.X + Drawing.Width/(float)2, position.Y + Drawing.Height/(float)2);
    }

    // Sets
    /// <summary>
    /// Set the position of the camera.
    /// The position will be positioned at the center of the screen.
    /// </summary>
    /// <param name="pos"></param>
    public static void SetPosition(Vector2 pos)
    {
        position = new Vector2(pos.X - Drawing.Width / (float)2, pos.Y - Drawing.Height / (float)2);
    }

    public void Move(Vector2 pos)
    {
        Instance.CameraPos = pos;
    }
}
