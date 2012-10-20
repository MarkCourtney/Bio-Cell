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
    public class PlayerCell : Player    // PlayerCell extends Player class
    {
        DeathMarker deathMarker;

        public override void LoadContent()
        {
            base.LoadContent();

            sprite = Game1.Instance.Content.Load<Texture2D>("Cell");    // Load the image into the sprite
            pos = new Vector2(100, 100);                                // Set position of the player
            radius = sprite.Width / 2;                                  // Radius equal to half the sprite width, used in collision detection
            normalAcceleration = 0;                                     // Normal acceleration is 0
            normalMaxAcceleration = 100;                                // Normal Maximum acceleration is 100
            boostedMaxAcceleration = 200;                               // Boosted Maximum acceleration is 200
            health = 3;                                                 // Player has 3 lives
            Alive = true;                                               // Bool to determine whether the player is alive or dead
            random = new Random();                                      // Random class used in spawning to new location after death
        }


        public override void BoostSpeed()                           // Additional boost in speed for limited time
        {
            if (keyState.IsKeyDown(Keys.Space))                     // When space is pressed, normal acceleration can reach 200
            {
                normalAcceleration += 1;                            // Continualy increase normalAcceleration by 1

                if (normalAcceleration > boostedMaxAcceleration)    // Check if normalAcceleration is greater than the boostedMaxAcceleration
                {
                    normalAcceleration = boostedMaxAcceleration;    //  Then normalAcceleration is equal to boostedMaxAcceleration
                }
            }
            else if (!keyState.IsKeyDown(Keys.Space))               // When space is NOT pressed, decrease normalAcceleration to normalMaxAcceleration
            {
                if (normalAcceleration > normalMaxAcceleration)     // Check if normalAcceleration is greater than normalMaxAcceleration
                {
                    normalAcceleration -= 4;                        // If not then decrease the normalAcceleration
                }
                else
                {
                    normalAcceleration = normalMaxAcceleration;     // Else normalAcceleration is 100
                }
            }
        }


        public override void DecreaseAcceleration(float timeDelta)          // Decrease the acceleration of the player to 0   
        {                                                                   // Slows the player till they stop - speed = 0    
            speed = 0;

            if (normalAcceleration > 0)     // If the normalAcceleration is greater than 0
            {
                normalAcceleration -= 2f;   // Decrease till normalAcceleration is 0
            }

            pos += look * (speed += normalAcceleration) * timeDelta;
        }

        public override void DecreaseHealth()   // Decrease the players health by 1
        {
            health -= 1;
        }


        public override void DisplayDeathPoint()        // Display the vector where the player dies
        {
            deathPos = center;                          // Get the position of the player upon death
            deathMarker = new DeathMarker(deathPos);    // Create new instance of DeathMarker type, set position to death location
            deathMarker.LoadContent();                  // Load content of the DeathMarker
            Game1.Instance.children.Add(deathMarker);   // Add DeathMarker to the list of entities
        }

        public override void IncreaseAcceleration(float timeDelta)      // Increases the acceleration of the player
        {
            speed = 100;
            normalAcceleration += 1;                        // Continualy increase normalAcceleration by 1

            if (normalAcceleration > normalMaxAcceleration) // If the normalAcceleration is greater than normalMaxAcceleration
            {
                BoostSpeed();                               // Then call the BoostSpeed() method
            }

            pos += look * (speed += normalAcceleration) * timeDelta;
        }


        public override void MovementKeys(KeyboardState keyState, GameTime gameTime, float timeDelta)   // Used to determine the keystate and what should be performed
        {
            if (keyState.IsKeyDown(Keys.W))
            {
                IncreaseAcceleration(timeDelta);    // Move the player forward in the direction they're facing
            }
            else if (keyState.IsKeyUp(Keys.W))
            {
                DecreaseAcceleration(timeDelta);    // Slow the player until they fully stop
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                rotation -= (5 * timeDelta);        // Rotate the player left
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                rotation += (5 * timeDelta);        // Rotate the player right
            }
        }


        public void ResetValues()       // Reset the movement values for the player as death occurs
        {
            normalAcceleration = 0;
            speed = 0;
        }


        public override void Respawn()  // Spawn the player in a new location
        {
            Alive = true;                                                       // Set the player to Alive
            pos = new Vector2(random.Next(bgX, bgY), random.Next(bgX, bgY));    // Spawn in new position
        }


        public override void Update(GameTime gameTime)
        {
            if (Alive == false)         // Check if the player is dead
            {
                DisplayDeathPoint();    // Will be used to display the death position of the player
                ResetValues();          // Reset the values
                Respawn();              // Respawn the player
            }


            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            keyState = Keyboard.GetState();


            look.X = (float)Math.Sin(rotation);                                         // Determine the look on the X axis using rotation
            look.Y = (float)-Math.Cos(rotation);                                        // Determine the look on the Y axis using rotation
            center = pos + new Vector2(sprite.Width / 2, sprite.Height / 2);            // Get the center vector of the player
            MovementKeys(keyState, gameTime, timeDelta);                                // Determine what the player pressed

            sphere = new BoundingSphere(new Vector3(center.X, center.Y, 0), radius);    // BoundingSphere used for collisions
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(sprite, pos, Color.White);
        }
    }
}