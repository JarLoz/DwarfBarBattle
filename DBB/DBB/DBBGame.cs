using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DBB
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DBBGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dwarf player1;

        KeyboardState currentKeyBoardState;
        KeyboardState previousKeyBoardState;

        public DBBGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            player1 = new Dwarf();

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Vector2 playerPosition = new
            Vector2(GraphicsDevice.Viewport.TitleSafeArea.X
            + GraphicsDevice.Viewport.TitleSafeArea.Width / 2,
            GraphicsDevice.Viewport.TitleSafeArea.Y
            + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            player1.Initialize(Content.Load<Texture2D>("small_dorf"), playerPosition);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            previousKeyBoardState = currentKeyBoardState;

            currentKeyBoardState = Keyboard.GetState();

            GatherInput();
            MoveEntities(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);
            spriteBatch.Begin();
            player1.Draw(spriteBatch);
            spriteBatch.End();


            base.Draw(gameTime);
        }

        private void GatherInput()
        {
            if (currentKeyBoardState.IsKeyDown(Keys.A))
            {
                player1.moveLeft();
            }
            else if (currentKeyBoardState.IsKeyDown(Keys.D))
            {
                player1.moveRight();
            }
            else if (!player1.jumping)
            {
                player1.stop();
            }

            if (currentKeyBoardState.IsKeyDown(Keys.Space) && previousKeyBoardState.IsKeyUp(Keys.Space))
            {
                player1.jump();
            }
        }

        private void MoveEntities(GameTime gameTime)
        {
            player1.Update(gameTime);
            player1.position.X = MathHelper.Clamp(player1.position.X,
                0, GraphicsDevice.Viewport.Width - player1.Width);
            player1.position.Y = MathHelper.Clamp(player1.position.Y,
                0, GraphicsDevice.Viewport.Height - player1.Height);
            if (player1.position.Y == GraphicsDevice.Viewport.Height - player1.Height)
            {
                player1.jumping = false;
                player1.doublejumping = false;
            }
        }
    }
}
