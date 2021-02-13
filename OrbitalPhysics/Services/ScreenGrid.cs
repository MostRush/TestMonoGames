using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;

namespace OrbitalPhysics.Services
{
    class ScreenGrid
    {
        public int Size { get; set; }
        public Color Color { get; set; }
        public Vector2 Vector1 { get; set; }
        public Vector2 Vector2 { get; set; }

        public ScreenGrid(int size, Color color, Vector2 vector1, Vector2 vector2)
        {
            Size = size;
            Color = color;
            Vector1 = vector1;
            Vector2 = vector2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            var widght = Math.Abs(Vector1.X - Vector2.X);
            var height = Math.Abs(Vector1.Y - Vector2.Y);

            var xPos = Vector1.X;
            var yPos = Vector1.Y;

            for (int i = 0; i < widght / Size; i++)
            {
                spriteBatch.DrawLine(xPos, Vector1.Y, xPos, Vector2.Y, Color, 1);

                xPos += Size;
            }

            for (int i = 0; i < height / Size; i++)
            {
                spriteBatch.DrawLine(Vector1.X, yPos, Vector2.X, yPos, Color, 1);

                yPos += Size;
            }

            spriteBatch.End();
        }
    }
}
