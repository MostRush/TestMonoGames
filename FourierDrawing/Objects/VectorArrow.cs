using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;

namespace FourierDrawing.Objects
{
    class VectorArrow : ICloneable
    {
        public Vector2 StartPoint;
        public Vector2 EndPoint;
        public float Lenght { get; set; }
        public float Speed { get; set; }
        public Color Color { get; set; }
        public float Angle { get; set; }
        public float Thikness { get; set; }

        public VectorArrow(Vector2 startPoint, float lenght, float angle, float speed, bool isDrawing, Color color, float thickness)
        {
            StartPoint = startPoint;
            Lenght = lenght;
            Speed = speed;
            Color = color;
            Angle = angle;
            Thikness = thickness;
        }

        public VectorArrow() { }

        public VectorArrow Draw(SpriteBatch _spriteBatch, GameTime gameTime)
        {
            _spriteBatch.Begin();

            EndPoint = FindEndPoint();

            Angle = Angle + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            _spriteBatch.DrawLine(StartPoint, EndPoint, Color, Thikness);
            _spriteBatch.DrawCircle(StartPoint, Lenght, (int)(Lenght * 0.6), new Color(50, 50, 50));

            _spriteBatch.End();

            return this;
        }

        public Vector2 FindEndPoint()
        {
            var ang = Angle * (float)Math.PI / 180f;

            var x = Lenght * (float)Math.Sin(ang) + StartPoint.X;
            var y = -Lenght * (float)Math.Cos(ang) + StartPoint.Y;

            return new Vector2(x, y);
        }

        public VectorArrow Clone()
        {
            return (VectorArrow)this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
