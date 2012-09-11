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

namespace Catte
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Textures and rectangles for our game entities
        Texture2D texCat;
        Texture2D texFurball;
        Texture2D texAlien;

        Rectangle rectCat;
        Rectangle rectFurball;
        Rectangle rectAlien;

        // Target destination for alien movement
        Point alienTarget;

        // Initialise a random number generator
        Random randNum = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set our preferred screen size
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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

            // TODO: use this.Content to load your game content here

            // Load our game sprites, and set up rectangles for each
            texCat = Content.Load<Texture2D>("cat");
            texFurball = Content.Load<Texture2D>("furball");
            texAlien = Content.Load<Texture2D>("alien");

            rectCat = new Rectangle(0, 0, texCat.Width, texCat.Height);
            rectFurball = new Rectangle(0, 0, texFurball.Width, texFurball.Height);
            rectAlien = new Rectangle(0, 0, texAlien.Width, texAlien.Height);

            // Set starting positions for all our stuff
            rectCat.Location = new Point(50, 300);
            rectFurball.Location = new Point(1280, 0);
            rectAlien.Location = new Point(1000, 400);

            // Set starting target for alien
            alienTarget = new Point(1000, randNum.Next(720 - rectAlien.Height));
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

            // TODO: Add your update logic here

            // Cat controls
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.2f || 
                Keyboard.GetState().IsKeyDown(Keys.Up)) rectCat.Offset(0, -4); // Move up
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.2f || 
                Keyboard.GetState().IsKeyDown(Keys.Down)) rectCat.Offset(0, 4); // Move down
            
            // Launch furball
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Space)) 
                rectFurball.Location = new Point(rectCat.Location.X + 100, rectCat.Location.Y + 50); 

            // Move alien toward target
            if (rectAlien.Top > alienTarget.Y + 5) rectAlien.Offset(0, -4);
            else if (rectAlien.Top < alienTarget.Y - 5) rectAlien.Offset(0, 4);
            // Alien has reached target, choose a new one!
            else alienTarget = new Point(1000, randNum.Next(720 - rectAlien.Height));

            // Move furball
            rectFurball.Offset(10, 0);

            // Check for furball/alien collision
            if (rectFurball.Intersects(rectAlien))
            {
                rectFurball.X = 1280;
                rectAlien.Y = -rectAlien.Height;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // Begin drawing!
            spriteBatch.Begin();

            // Draw our three sprites using their corresponding rectangles for positioning
            spriteBatch.Draw(texCat, rectCat, Color.White);
            spriteBatch.Draw(texFurball, rectFurball, Color.White);
            spriteBatch.Draw(texAlien, rectAlien, Color.White);

            // End drawing!
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
