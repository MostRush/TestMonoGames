using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.TestClasses
{
    class SerpinskiTriangle
    {
        public List<Vector2> points { get; private set; }
        public bool IsBaked { get; set; }
        public int Depth { get; set; }
        public Color Color { get; set; }

        private int currentPointIndex = 0;

        public SerpinskiTriangle(params Vector2[] points)
        {
            this.points = points.ToList();
        }

        public SerpinskiTriangle()
        {
            this.points = new List<Vector2>();
        }

        public void CalculatePoints()
        {
            const int qdots = 3;

            if (!IsBaked || points.Count < qdots) return;

            var random = new Random();

            var lastPointIndex = random.Next(qdots, points.Count);

            var p1 = points[random.Next(0, qdots)];
            var p2 = points[currentPointIndex];

            currentPointIndex = lastPointIndex;

           var newPoint = new Vector2((p1.X + p2.X) * 0.5f, (p1.Y + p2.Y) * 0.5f);
            //var newPoint = new Vector2(((p1.X) + 2 * p2.X) / 3, ((p1.Y) + 2 * p2.Y) / 3);

            if (points.Any(p => p.X != newPoint.X && p.Y != newPoint.X))
            {
                points.Add(newPoint);
            }
        }

        public void AddPoint(Vector2 vector)
        {
            points.Add(vector);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //if (points.Count < 4) return;

            spriteBatch.Begin();

            foreach (var point in points)
            {
                spriteBatch.DrawCircle(point, 1, 3, Color);
            }

            spriteBatch.End();
        }
    }
}
