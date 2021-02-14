using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;
using System.Collections.Generic;
using System.Text;

namespace FractalsDrawing.Fractals
{
    class Circle
    {
        public Vector2 position;
        public float thickness;
        public float radius;
        public Color color;
        public int size;

        public Circle(Vector2 position, float radius, int size, float thickness, Color color)
        {
            this.thickness = thickness;
            this.position = position;
            this.radius = radius;
            this.color = color;
            this.size = size;
        }

        public Circle Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawCircle(position, radius, size, color);
            spriteBatch.End();

            return this;
        }
    }

    class AppoloCircles
    {
        public List<Circle> circles;
        public GameServiceContainer Services;

        SpriteBatch _spriteBatch;
        GraphicsDeviceManager _graphics;

        public AppoloCircles(GameServiceContainer services)
        {
            Services = services;

            _spriteBatch = Services.GetService<SpriteBatch>();
            _graphics = Services.GetService<GraphicsDeviceManager>();

            var w = _graphics.PreferredBackBufferWidth;
            var h = _graphics.PreferredBackBufferHeight;

            circles = new List<Circle>();

            //var radius = h * 0.45f;

            float radius = w * 0.1f;
            float x = w / 2;
            float y = h / 2 - radius;

            var c1 = new Circle(new Vector2(x, y), radius, (int)(radius * 0.5f), 1, Color.White);

            x -= radius;
            y += (float)(radius * Math.Sqrt(3));

            var c2 = new Circle(new Vector2(x, y), radius, (int)(radius * 0.5f), 1, Color.White);

            x += 2 * radius;

            var c3 = new Circle(new Vector2(x, y), radius, (int)(radius * 0.5f), 1, Color.White);

            circles.Add(c1);
            circles.Add(c2);
            circles.Add(c3);

            for (int i = 0; i < 1; i++)
            {
                var firstCircle = circles[0];

                var fr = firstCircle.radius;
                var fp = firstCircle.position;

                radius = fr * 0.5f;

                var pos = new Vector2(x, y);

                var circle = new Circle(pos, radius, (int)(radius * 0.5f), 1, Color.White);

                //circles.Add(circle);
            }

            var d = SolveTheApollonius(1, c1, c2, c3);

            circles.Add(d);
            circles.Add(SolveTheApollonius(5, c1, c2, d));
        }

        public AppoloCircles Draw()
        {
            foreach (var c in circles)
                c.Draw(_spriteBatch);

            return this;
        }

        private static Circle SolveTheApollonius(int calcCounter, Circle c1, Circle c2, Circle c3)
        {
            float s1 = 1;
            float s2 = 1;
            float s3 = 1;

            if (calcCounter == 2)
            {
                s1 = -1;
                s2 = -1;
                s3 = -1;
            }
            else if (calcCounter == 3)
            {
                s1 = 1;
                s2 = -1;
                s3 = -1;
            }
            else if (calcCounter == 4)
            {
                s1 = -1;
                s2 = 1;
                s3 = -1;
            }
            else if (calcCounter == 5)
            {
                s1 = -1;
                s2 = -1;
                s3 = 1;
            }
            else if (calcCounter == 6)
            {
                s1 = 1;
                s2 = 1;
                s3 = -1;
            }
            else if (calcCounter == 7)
            {
                s1 = -1;
                s2 = 1;
                s3 = 1;
            }
            else if (calcCounter == 8)
            {
                s1 = 1;
                s2 = -1;
                s3 = 1;
            }

            float x1 = c1.position.X;
            float y1 = c1.position.Y;
            float r1 = c1.radius;    
                       
            float x2 = c2.position.X;
            float y2 = c2.position.Y;
            float r2 = c2.radius;

            float x3 = c3.position.X;
            float y3 = c3.position.Y;
            float r3 = c3.radius;

            //This calculation to solve for the solution circles is cited from the Java version 
            float v11 = 2 * x2 - 2 * x1;
            float v12 = 2 * y2 - 2 * y1;
            float v13 = x1 * x1 - x2 * x2 + y1 * y1 - y2 * y2 - r1 * r1 + r2 * r2;
            float v14 = 2 * s2 * r2 - 2 * s1 * r1;

            float v21 = 2 * x3 - 2 * x2;
            float v22 = 2 * y3 - 2 * y2;
            float v23 = x2 * x2 - x3 * x3 + y2 * y2 - y3 * y3 - r2 * r2 + r3 * r3;
            float v24 = 2 * s3 * r3 - 2 * s2 * r2;

            float w12 = v12 / v11;
            float w13 = v13 / v11;
            float w14 = v14 / v11;

            float w22 = v22 / v21 - w12;
            float w23 = v23 / v21 - w13;
            float w24 = v24 / v21 - w14;

            float P = -w23 / w22;
            float Q = w24 / w22;
            float M = -w12 * P - w13;
            float N = w14 - w12 * Q;

            float a = N * N + Q * Q - 1;
            float b = 2 * M * N - 2 * N * x1 + 2 * P * Q - 2 * Q * y1 + 2 * s1 * r1;
            float c = x1 * x1 + M * M - 2 * M * x1 + P * P + y1 * y1 - 2 * P * y1 - r1 * r1;

            float D = b * b - 4 * a * c;

            var rs = (-b - float.Parse(Math.Sqrt(D).ToString())) / (2 * float.Parse(a.ToString()));
            var xs = M + N * rs;
            var ys = P + Q * rs;

            return new Circle(new Vector2(xs, ys), rs, (int)(rs * 0.5f), 1, Color.White);
        }
    }
}
