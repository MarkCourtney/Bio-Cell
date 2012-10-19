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
    public class Enemy : Entity     // Super class for all Enemy Entities
    {
        public float distance;      // calculate distance between player and collectable
        public float comboRadius;   // combines the radius of the player and collectable

        public virtual void CheckDistanceToPlayer() { } // Perform an action based on the distance to the player
    }
}