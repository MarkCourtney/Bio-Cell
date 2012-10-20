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

namespace Bio_Cell
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Game1 Instance;

        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        PlayerCell playerCell;

        public List<Entity> children = new List<Entity>();  // List of entities that will be loaded into the world 
       
        public Game1()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            playerCell = new PlayerCell();

            children.Add(playerCell);       // Add the player to the List of children

            base.Initialize();
        }


        // LoadContent of all the entities in the List of children
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < children.Count; i++)
            {
                children[i].LoadContent();
            }
        }


        // Draw all the entities in the List of children
        protected override void Update(GameTime gameTime)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Update(gameTime);
                if (!children[i].Alive)
                {
                    children.Remove(children[i]);       // Remove children from the list if Alive = false
                }
            }
        }


        // Draw all the entities in the List of children
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Draw(gameTime);
            }
            spriteBatch.End();
        }
    }
}