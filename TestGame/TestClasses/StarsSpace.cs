using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;
using System.Collections.Generic;

namespace TestGame.GameObjects
{
    struct Star
    {
        public Vector2 Position;
        public int Brightness;
        public float Factor;
        public int Size;

        public Star(Vector2 position, int brightness, float factor, int size)
        {
            Brightness = brightness;
            Position = position;
            Factor = factor;
            Size = size;
        }
    }

    class StarsSpace
    {
        public List<Star> Stars { get; private set; }
        public int Density { get; set; }

        public Vector2 Until { get; set; }
        public Vector2 From { get; set; }

        private Random random;

        public StarsSpace(Vector2 from, Vector2 until, int density)
        {
            Density = density;
            Until = until;
            From = from;

            Stars = new List<Star>();
            random = new Random();

            GeneratePositions();
        }

        private void GeneratePositions()
        {
            for (int i = 0; i < Density; i++)
            {
                var x = random.Next((int)From.X, (int)Until.X);
                var y = random.Next((int)From.Y, (int)Until.Y);

                var size = random.Next(1, 3);
                var factor = random.Next(1, 3) * 0.1f;
                var brightness = random.Next(50, 255);

                Stars.Add(new Star(new Vector2(x, y), brightness, factor, size));
            }
        }

        DateTime lastTime = DateTime.Now;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            for (int i = 0; i < Stars.Count; i++)
            {
                var position = Stars[i].Position;
                var brightness = Stars[i].Brightness;
                var factor = Stars[i].Factor;
                var size = Stars[i].Size;

                if ((DateTime.Now - lastTime).TotalMilliseconds >= 0.08)
                {
                    var star = Stars[i];
                    star.Factor = random.Next(1, 3) * 0.1f;
                    Stars[i] = star;

                    lastTime = DateTime.Now;
                }

                var color = (int)(brightness * Stars[i].Factor);

                spriteBatch.DrawCircle(position, size, 6, new Color(color, color, color + 5), size);
            }

            spriteBatch.End();
        }
    }
}
