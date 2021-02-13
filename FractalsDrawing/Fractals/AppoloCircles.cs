using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FractalsDrawing.Fractals
{
    class AppoloCircles
    {
        class Circle
        {
            public Vector2 position;
            public float radius;
            public float thickness;

            public Circle(Vector2 position, float radius, float thickness)
            {
                this.position = position;
                this.radius = radius;
                this.thickness = thickness;
            }


        }

    }
}
