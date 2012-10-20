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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Entity
    {
        public Vector2 center, look, pos, target;                                       // Vector positions for center of a sprite, looking direction, current position, target position
        public float rotation, radius, speed;                                           // Floats for rotation, radius of sprite, speed and acceleration of entities
        public float normalAcceleration, normalMaxAcceleration, boostedMaxAcceleration; // Floats for normal acceleration and maximum acceleration values
        public int bgX, bgY;                                                            // Integers forlength background X and Y axis
        public Texture2D sprite;                                                        // Texture2D used in loading an image
        public Vector2 basis = new Vector2(0, -1);                                      // Vector based on calculating rotation
        public bool Alive;                                                              // Boolean for current state of entity
        public BoundingSphere sphere;                                                   // BoundingSphere used in collision detection
        public Random random;                                                           // Random class used to create a random value 


        // Create virtual functions for Initialize, LoadContent, UnloadContent, Update, Draw
        // These can be overriden in each of the inherting classes to describe that component
        // Initialize these basic values of all the inherting game components before the game begins
        public virtual void Initialize()
        {
            rotation = 0.0f;
            look = new Vector2(0, -1);      // default look is North facing
            center = new Vector2(sprite.Width / 2, sprite.Height / 2);  // center equals middle point sprite 
            speed = 100;
            Alive = true;
            bgX = 2400;     // background X axis is 2400 pixels long
            bgY = 1800;     // background Y axis is 1800 pixels long

            sphere = new BoundingSphere(new Vector3(center.X, center.Y, 0), radius);    // Vector3 used as it's required
        }

        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
    }
}
