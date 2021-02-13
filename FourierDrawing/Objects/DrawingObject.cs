using FourierDrawing.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourierDrawing.Objects
{
    class DrawingObject
    {
        public List<VectorArrow> Arrows { get; set; }
        public Vector2 StartPoint { get; set; }
        public List<Vector2> DrawnLines { get; private set; }
        public bool IsDrawing { get; set; }
        public float Speed { get; set; }


        public DrawingObject()
        {
            Arrows = new List<VectorArrow>();
            DrawnLines = new List<Vector2>();
        }

        public void AddArrow(VectorArrow arrow)
        {
            Arrows.Add(arrow);
        }

        public void AddArrows((float lenght, float speed, float thikness)[] arrows)
        {
            VectorArrow previousArrow = null;

            int count = 0;

            foreach (var a in arrows)
            {
                count++;

                var arrow = new VectorArrow()
                {
                    StartPoint = Arrows.Count < 1 ? StartPoint : previousArrow.EndPoint,
                    Lenght = a.lenght,
                    Speed = a.speed * Speed,
                    Thikness = a.thikness,
                    Color = Color.White,
                };

                Arrows.Add(arrow);

                previousArrow = arrow;
            }
        }


        DateTime lastTime;
        DateTime lastTime2;
        double hue = 0;

        public DrawingObject Draw(SpriteBatch _spriteBatch, GameTime gameTime)
        {

            if (IsDrawing)
            {
                if (((DateTime.Now - lastTime).TotalSeconds >= 0.05 / Speed) && (Arrows.Count > 0))
                {
                    DrawnLines.Add(Arrows[Arrows.Count - 1].EndPoint);
                    lastTime = DateTime.Now;
                }

                if (DrawnLines.Count > 0)
                {
                    _spriteBatch.Begin();

                    var lastVector = DrawnLines[0];
                    /*
                    if (((DateTime.Now - lastTime2).TotalSeconds >= 0.05))
                    {
                        for (int i = 0; i < DrawnLines.Count; i++)
                        {
                            DrawnLines[i] = new Vector2(DrawnLines[i].X - 0.1f, DrawnLines[i].Y);

                        }
                    }
                    */
                    hue = hue > 359 ? 0 : hue + 100 * gameTime.ElapsedGameTime.TotalSeconds;
                    var rainbowColor = ColorUtils.HSVToRGB(new HSV(hue, 1, 1));

                    foreach (var vector in DrawnLines)
                    {
                        _spriteBatch.DrawLine(lastVector, vector, rainbowColor, 1);
                        lastVector = vector;
                    }

                    _spriteBatch.End();
                }
            }

            VectorArrow previousArrow = null;

            for (int i = 0; i < Arrows.Count; i++)
            {
                if (i > 0)
                    Arrows[i].StartPoint = previousArrow.EndPoint;

                previousArrow = Arrows[i].Draw(_spriteBatch, gameTime);
            }

            return this;
        }
    }
}
