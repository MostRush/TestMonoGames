using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame.Services
{
    class Debug : IServiceProvider
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        public Debug(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
        }

        public void DrawText(string text) => DrawText(text, null, null);

        public void DrawText(string text, Vector2 position) => DrawText(text, null, position);

        public void DrawText(string text, Color? color, Vector2? position)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, text, position ?? new Vector2(0, 0), color ?? new Color(200, 200, 200));
            spriteBatch.End();
        }
    }
}
