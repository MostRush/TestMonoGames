using FourierDrawing.Objects;
using FourierDrawing.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using MonoGame;

namespace FourierDrawing
{
    public class Startup : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        private DrawingObject drawingObject;
        private DrawingObject drawingObject2;
        private DrawingObject drawingObject3;

        public Startup()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            drawingObject = new DrawingObject();
            drawingObject2 = new DrawingObject();
            drawingObject3 = new DrawingObject();

            _graphics.SynchronizeWithVerticalRetrace = false;
            base.IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            var width = _graphics.PreferredBackBufferWidth;
            var height = _graphics.PreferredBackBufferHeight;

            var arrows = new (float lenght, float speed, float thikness)[500];

            for (int i = 0; i < arrows.Length; i++)
                arrows[i] = (Rand(5, 50) * 0.2f, Rand(-10, 5), 1);

            var arrs = new (float, float, float)[]
            {
                (50, 15, 1),
                (25, -5, 1),
                (25, -10, 1),
                (75, -10, 1),
                (10, 5, 1),
                (7, -5, 1),
            };

            drawingObject.Speed = 20;
            drawingObject.StartPoint = new Vector2(width * 0.5f, height * 0.5f);
            drawingObject.AddArrows(arrows);

            var arrows2 = new (float lenght, float speed, float thikness)[5];

            for (int i = 0; i < arrows2.Length; i++)
                arrows2[i] = (Rand(5, 50) * 0.5f, Rand(-10, 10), 1);

            drawingObject2.Speed = 100;
            drawingObject2.StartPoint = new Vector2(width * 0.9f, height * 0.3f);
            drawingObject2.AddArrows(arrows2);

            var arrows3 = new (float lenght, float speed, float thikness)[5];

            for (int i = 0; i < arrows3.Length; i++)
                arrows3[i] = (Rand(5, 50) * 0.5f, Rand(-10, 10), 1);

            drawingObject3.Speed = 100;
            drawingObject3.StartPoint = new Vector2(width * 0.9f, height * 0.7f);
            drawingObject3.AddArrows(arrows3);

            Services.AddService<FrameRate>(new FrameRate());
            Services.AddService<ScreenGrid>(new ScreenGrid(10, new Color(10, 10, 10), new Vector2(0, 0), new Vector2(width, height)));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("Consolas");

            Services.AddService<Debug>(new Debug(_spriteBatch, _spriteFont));
            // TODO: use this.Content to load your game content here
        }

        DateTime lastTime = DateTime.Now;

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                drawingObject.IsDrawing = true;
                drawingObject2.IsDrawing = true;
                drawingObject3.IsDrawing = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Services.GetService<ScreenGrid>().Draw(_spriteBatch);

            drawingObject.Draw(_spriteBatch, gameTime);
            /*drawingObject2.Draw(_spriteBatch, gameTime);
            drawingObject3.Draw(_spriteBatch, gameTime);*/
            #region SHOW FRAME RATE

            var frameCouneter = Services.GetService<FrameRate>();
            var frameRateString = "fps: " + frameCouneter.FrameUpdated().ToString();
            Services.GetService<Debug>().DrawText(frameRateString, new Color(0, 200, 0), new Vector2(5, 5));

            #endregion

            base.Draw(gameTime);
        }

        private float Rand(float minimum, float maximum)
        {
            Random random = new Random();
            return (float)random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
