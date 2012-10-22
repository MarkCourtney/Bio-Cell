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
    public class EnemyVirus : Enemy
    {
        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            sprite = Game1.Instance.Content.Load<Texture2D>("Virus");       // Load the Virus image into the sprite
            random = new Random();                                          // Random class used in spawning to new location after death
            pos = new Vector2(random.Next(10, 800), random.Next(10, 800));  // Set position to random location on map
            radius = sprite.Width / 2;                                      // Radius equal to half the sprite width, used in collision detection
            Alive = true;                                                   // Bool to determine whether the player is alive or dead
        }


        public void SetDeath()
        {
            Alive = false;
        }

        public void CheckDistance(float timeDelta)
        {
            if (distance > 100)
            {
                speed = 10;
                pos += look * 3 * timeDelta;
            }
            else if (distance <= 100 && distance > 0)
            {
                speed = 100;
                pos += look * speed * timeDelta;
            }
        }


        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            center = pos + new Vector2(sprite.Width / 2, sprite.Height / 2);

            sphere = new BoundingSphere(new Vector3(center.X, center.Y, 0), radius);


            target = new Vector2(Game1.Instance.PlayerCell.center.X - center.X, Game1.Instance.PlayerCell.center.Y - center.Y);

            comboRadius = (Game1.Instance.PlayerCell.radius + radius);
            distance = Vector2.Distance(Game1.Instance.PlayerCell.center, center) - comboRadius;


            look.X = (float)Math.Sin(rotation);
            look.Y = (float)-Math.Cos(rotation);

            rotation = (float)Math.Acos(Vector2.Dot(basis, target) / target.Length());

            if (Game1.Instance.PlayerCell.center.X < center.X)
            {
                rotation = -rotation;
            }

            CheckDistance(timeDelta);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(sprite, pos, Color.White);
        }
    }
}
