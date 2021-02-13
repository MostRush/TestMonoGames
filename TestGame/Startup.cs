using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using System;
using System.Collections.Generic;
using TestGame.GameObjects;
using TestGame.Services;
using TestGame.TestClasses;
using TestGame.Utils;

namespace TestGame
{
    enum TestGameState
    {
        Drawing,
        SerpTriangle
    }

    public class Startup : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private TestGameState gameState;

        Figure figure;
        SerpinskiTriangle serpinskiTriangle;



        private IServiceCollection services;

        public Startup(IServiceCollection services)
        {
            this.services = services;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.IsBorderless = true;
            IsMouseVisible = true;

            this.graphics.SynchronizeWithVerticalRetrace = false;
            base.IsFixedTimeStep = false;

            figure = new Figure();
            serpinskiTriangle = new SerpinskiTriangle();
        }

        private void ServiceInitialize()
        {
            var bufferWidtt = graphics.PreferredBackBufferWidth;
            var bufferHeight = graphics.PreferredBackBufferHeight;

            services.Add<FrameRate>();
            services.Add<Debug>(new object[] { spriteBatch, spriteFont });
            services.Add<ScreenGrid>(new object[] { 10, new Color(10, 10, 10), new Vector2(0, 0), new Vector2(bufferWidtt, bufferHeight) });

            Services.AddService<StarsSpace>(new StarsSpace(new Vector2(0, 0), new Vector2(bufferWidtt, bufferHeight), 4000));
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            figure.BaryCenter = new Vector2(graphics.PreferredBackBufferWidth * 0.5f, graphics.PreferredBackBufferHeight * 0.5f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("debug");

            this.ServiceInitialize();
        }

        Vector2 pointPos { get; set; }
        DateTime lastTime = DateTime.Now;

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D1)) gameState = TestGameState.Drawing;
            if (Keyboard.GetState().IsKeyDown(Keys.D2)) gameState = TestGameState.SerpTriangle;

            if (gameState == TestGameState.Drawing)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    figure.ClearVectors();
                }

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if ((DateTime.Now - lastTime).TotalSeconds >= 0.02)
                    {
                        pointPos = Mouse.GetState().Position.ToVector2();
                        figure.AddVector(pointPos);
                        lastTime = DateTime.Now;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    figure.Rotate(-5f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    figure.Rotate(5f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            }

            if (gameState == TestGameState.SerpTriangle)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if ((DateTime.Now - lastTime).TotalSeconds >= 0.1)
                    {
                        pointPos = Mouse.GetState().Position.ToVector2();
                        serpinskiTriangle.AddPoint(pointPos);
                        lastTime = DateTime.Now;
                    }
                }

                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    if ((DateTime.Now - lastTime).TotalSeconds >= 0.1)
                    {
                        serpinskiTriangle.IsBaked = true;
                        lastTime = DateTime.Now;
                    }
                }
            }

            base.Update(gameTime);
        }

        double hue = 0;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Services.GetService<StarsSpace>().Draw(spriteBatch);
            services.GetService<ScreenGrid>().Draw(spriteBatch);

            var newMousePos = Mouse.GetState().Position.ToVector2();

            spriteBatch.Begin();

            hue = hue > 359 ? 0 : hue + 100 * gameTime.ElapsedGameTime.TotalSeconds;
            var rainbowColor = ColorUtils.HSVToRGB(new HSV(hue, 1, 1));

            for (int i = 0; i < 5; i++)
            {
                spriteBatch.DrawCircle(newMousePos.X, newMousePos.Y, i, 16, rainbowColor);
                spriteBatch.DrawCircle(figure.BaryCenter, i, 16, rainbowColor);
            }

            spriteBatch.DrawLine(pointPos, Mouse.GetState().Position.ToVector2(), rainbowColor);

            spriteBatch.End();

            figure.Color = rainbowColor;
            figure.Draw(spriteBatch);

            serpinskiTriangle.Color = rainbowColor;
            serpinskiTriangle.Draw(spriteBatch);
            serpinskiTriangle.CalculatePoints();

            services.GetService<Debug>().DrawText(serpinskiTriangle.points.Count.ToString(), new Color(100, 100, 100), new Vector2(5, 100));
            services.GetService<Debug>().DrawText(serpinskiTriangle.IsBaked.ToString(), new Color(100, 100, 100), new Vector2(5, 120));

            services.GetService<Debug>().DrawText(DateTime.Now.ToString(), new Color(100, 100, 100), new Vector2(5, 25));
            services.GetService<Debug>().DrawText(hue.ToString(), new Color(100, 100, 100), new Vector2(5, 70));
            services.GetService<Debug>().DrawText(Mouse.GetState().Position.ToString(), new Color(100, 100, 100), new Vector2(5, 50));

            #region SHOW FRAME RATE

            var frameCouneter = services.GetService<FrameRate>();
            var frameRateString = "fps: " + frameCouneter.FrameUpdated().ToString();
            services.GetService<Debug>().DrawText(frameRateString, new Color(0, 200, 0), new Vector2(5, 5));

            #endregion

            base.Draw(gameTime);
        }
    }
}
