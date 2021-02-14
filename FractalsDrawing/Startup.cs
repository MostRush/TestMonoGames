using FractalsDrawing.Fractals;
using FractalsDrawing.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FractalsDrawing
{
    public class Startup : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        private AppoloCircles fractal;

        public Startup()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;

            Window.IsBorderless = true;

            _graphics.SynchronizeWithVerticalRetrace = false;
            base.IsFixedTimeStep = false;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("debug");

            Services.AddService<SpriteBatch>(_spriteBatch);
            Services.AddService<GraphicsDeviceManager>(_graphics);
            Services.AddService<Debug>(new Debug(_spriteBatch, _spriteFont));
            Services.AddService<FrameRate>(new FrameRate());

            fractal = new AppoloCircles(Services);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Services.GetService<Debug>().DrawText(fractal.circles[0].position.ToString());

            fractal.Draw();

            #region SHOW FRAME RATE

            var frameCouneter = Services.GetService<FrameRate>();
            var frameRateString = "fps: " + frameCouneter.FrameUpdated().ToString();
            Services.GetService<Debug>().DrawText(frameRateString, new Color(0, 200, 0), new Vector2(5, 5));

            #endregion

            base.Draw(gameTime);
        }
    }
}
