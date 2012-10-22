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
    public class Player : Entity        // Super class for all Player Entities
    {
        public Vector2 deathPos;        // Variable used to determine the position where the character died
        public KeyboardState keyState;  // Get the current key state of the keyboard
        public int health;              // Health of the player
        public GamePadState gamePadState;         // GamePad for XBox 360 controller

        public virtual void BoostSpeed() { }                                                                // Boost ability that increases speed
        public virtual void DisplayDeathPoint() { }                                                         // Called when the player dies (Alive = false)
        public virtual void DecreaseHealth() { }                                                            // Decreases the health of the player when certain actions occur
        public virtual void DecreaseAcceleration(float timeDelta) { }                                       // As movement stops, decrease the player till stop
        public virtual void IncreaseAcceleration(float timeDelta) { }                                       // By default increase the acceleration during movement
        public virtual void MovementKeyboard(KeyboardState keyState, GameTime gameTime, float timeDelta) { }    // Interpret the movement input
        public virtual void MovementGamePad(GamePadState gamePadState, GameTime gameTime, float timeDelta) { }    // Interpret the movement input
        public virtual void Respawn() { }                                                                   // Spawn the player in a new location
    }
}