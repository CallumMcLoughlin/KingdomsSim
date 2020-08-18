using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace KingdomsSim
{
    public class Camera
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }

        private float zoom, previousZoom;

        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = 1f;
            Position = Vector2.Zero;
        }


        private void UpdateVisibleArea()
        {
            Matrix inverseViewMatrix = Matrix.Invert(Transform);

            Vector2 tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            Vector2 tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            Vector2 bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            Vector2 br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

            Vector2 min = new Vector2(MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))), MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            Vector2 max = new Vector2(MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))), MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        private void UpdateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) * Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition)
        {
            Vector2 newPosition = Position + movePosition;
            Position = newPosition;
        }

        public void AdjustZoom(float zoomAmount)
        {
            Zoom += zoomAmount;
            if (Zoom < .35f)
                Zoom = .35f;
            if (Zoom > 2f)
                Zoom = 2f;
        }

        public void UpdateCamera(Viewport bounds, Input input)
        {
            Bounds = bounds.Bounds;
            UpdateMatrix();

            Vector2 cameraMovement = Vector2.Zero;
            int moveSpeed;

            if (Zoom > .8f)
                moveSpeed = 15;
            else if (Zoom < .8f && Zoom >= .6f)
                moveSpeed = 20;
            else if (Zoom < .6f && Zoom > .35f)
                moveSpeed = 25;
            else if (Zoom <= .35f)
                moveSpeed = 30;
            else
                moveSpeed = 10;


            if (input.KeyboardState.IsKeyDown(Keys.W) || input.KeyboardState.IsKeyDown(Keys.Up))
                cameraMovement.Y = -moveSpeed;

            if (input.KeyboardState.IsKeyDown(Keys.S) || input.KeyboardState.IsKeyDown(Keys.Down))
                cameraMovement.Y = moveSpeed;

            if (input.KeyboardState.IsKeyDown(Keys.A) || input.KeyboardState.IsKeyDown(Keys.Left))
                cameraMovement.X = -moveSpeed;

            if (input.KeyboardState.IsKeyDown(Keys.D) || input.KeyboardState.IsKeyDown(Keys.Right))
                cameraMovement.X = moveSpeed;

            if (input.IsNewScrollWheelUp())
            {
                AdjustZoom(.05f);
            }

            if (input.IsNewScrollWheelDown())
            {
                AdjustZoom(-.05f);
            }

            previousZoom = zoom;
            zoom = Zoom;

            MoveCamera(cameraMovement);
        }
    }
}
