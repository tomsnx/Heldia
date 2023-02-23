using Microsoft.Xna.Framework;

namespace Heldia
{
    public class Camera
    {
        // properties
        public Vector2 position;
        public Matrix transform { get; private set; }
        public float delay { get; set; } = 6.0f;

        // constructor
        public Camera(Vector2 pos) { this.position = pos; }

        // update

        public void Update(Vector2 pos, Main g)
        {
            float d = delay * Drawing.delta;

            // move
            position.X += ((pos.X - position.X) - Drawing.width / 2) * d;
            position.Y += ((pos.Y - position.Y) - Drawing.height / 2) * d;

            transform = Matrix.CreateTranslation((int)-position.X, -position.Y, 0);
        }
    }
}
