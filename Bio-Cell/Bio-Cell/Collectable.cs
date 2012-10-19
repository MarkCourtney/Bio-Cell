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
    public class Collectable : Entity       // Super class for all Collectable Entities
    {
        public float distance;              // Calculate distance between player and collectable
        public float comboRadius;           // Combines the radius of the player and collectable

        public void TrackPlayer() { }       // Track the player position in relation to the collectable
    }
}