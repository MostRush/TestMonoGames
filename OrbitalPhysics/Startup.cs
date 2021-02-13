using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OrbitalPhysics.GameObjects;
using OrbitalPhysics.Services;

namespace OrbitalPhysics
{
    public class Startup : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;

        public Startup()
        {
            graphics = new GraphicsDeviceManager(this);

            Window.IsBorderless = true;
            IsMouseVisible = true;
            /*
            this.graphics.SynchronizeWithVerticalRetrace = false;
            base.IsFixedTimeStep = false;
            */
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("debug");

            var screenWidth = graphics.PreferredBackBufferWidth;
            var screenHeight = graphics.PreferredBackBufferHeight;

            var gridFromVector = new Vector2(0, 0);
            var gridToVector = new Vector2(screenWidth, screenHeight);

            Services.AddService(new ScreenGrid(5, new Color(10, 10, 10), gridFromVector, gridToVector));
            Services.AddService(new Debug(spriteBatch, spriteFont));
            Services.AddService(new FrameRate());

            var from = new Vector2(0, 0);
            var until = new Vector2(screenWidth, screenHeight);

            Services.AddService(new StarsSpace(from, until, 4000));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Services.GetService<ScreenGrid>().Draw(spriteBatch);
            Services.GetService<FrameRate>().FrameUpdated();
            Services.GetService<StarsSpace>().Draw(spriteBatch);

            var frameRateString = "fps: " + Services.GetService<FrameRate>().FrameUpdated().ToString();
            Services.GetService<Debug>().DrawText(frameRateString, new Color(0, 200, 0), new Vector2(5, 5));

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
