using System;

namespace FractalsDrawing.Services
{
    class FrameRate
    {
        public int Counter { get; private set; }

        DateTime lastTime;
        int framesRendered;

        public int FrameUpdated()
        {
            framesRendered++;

            if ((DateTime.Now - lastTime).TotalSeconds >= 1)
            {
                Counter = framesRendered;
                framesRendered = 0;
                lastTime = DateTime.Now;
            }

            return Counter;
        }
    }
}
