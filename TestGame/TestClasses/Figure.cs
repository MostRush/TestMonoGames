using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.TestClasses
{
    class Figure
    {
        public Vector2 BaryCenter { get; set; }
        public float Rotation { get; set; }
        public Color Color { get; set; }
        public float Tinkness { get; set; } = 1;

        List<Vector2> vectors { get; set; }

        public Figure()
        {
            vectors = new List<Vector2>();
        }

        public Figure(Color? color, float? rotation, params Vector2[] vectors)
        {
            this.vectors = vectors.ToList();
            Color = color ?? Color.White;
            Rotation = rotation ?? 0;
            FindBaryCenter();
        }

        public void AddVector(Vector2 vector)
        {
            vectors.Add(vector);
        }

        public void ClearVectors()
        {
            vectors.Clear();
        }

        public void FindBaryCenter()
        {
            float sumX = 0;
            float sumY = 0;

            foreach (var vector in vectors)
            {
                sumX += vector.X;
                sumY += vector.Y;
            }

            BaryCenter = new Vector2(sumX / vectors.Count, sumY / vectors.Count);
        }

        public void Rotate(float f)
        {
            var newVectors = new List<Vector2>();

            foreach (var v in vectors)
            {
                var x = (v.X - BaryCenter.X);
                var y = (v.Y - BaryCenter.Y);

                var x1 = x * Math.Cos(f) - y * Math.Sin(f) + BaryCenter.X;
                var y1 = x * Math.Sin(f) + y * Math.Cos(f) + BaryCenter.Y;

                newVectors.Add(new Vector2((float)x1, (float)y1));
            }

            vectors = newVectors;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (vectors.Count < 1) return;

            //FindBaryCenter();

            spriteBatch.Begin();

            var lastVector = vectors[0];

            foreach (var vector in vectors)
            {
                spriteBatch.DrawLine(lastVector, vector, Color, Tinkness);
                lastVector = vector;
            }

            spriteBatch.End();
        }
    }
}
