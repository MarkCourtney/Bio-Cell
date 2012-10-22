using System;
using System.Diagnostics;
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
    public class CollectableGlucose : Collectable
    {

        int checkNegative;

        float randomLook1, randomLook2;

        Stopwatch s = new Stopwatch();
        KeyboardState keyState;
        PlayerCell playerCell;

        public override void LoadContent()
        {
            base.LoadContent();

            playerCell = new PlayerCell();

            random = new Random();
            pos = new Vector2(random.Next(10, 500), random.Next(10, 500));
            sprite = Game1.Instance.Content.Load<Texture2D>("Protein");
            speed = random.Next(35, 40);
            randomLook1 = random.Next(10, 500);  // Number between 1 and -1 where the X axis looks
            randomLook2 = random.Next(10, 500);  // Number between 1 and -1 where the Y axis looks
            radius = sprite.Width / 2;
            Alive = true;

        }

        public override void TrackPlayer()
        {
            target = new Vector2(Game1.Instance.playerCell.center.X - center.X, Game1.Instance.playerCell.center.Y - center.Y);

            rotation = (float)Math.Acos(Vector2.Dot(basis, target) / target.Length());

            if (Game1.Instance.playerCell.center.X < center.X)
            {
                rotation = -rotation;
            }

            look.X = (float)Math.Sin(rotation);
            look.Y = (float)-Math.Cos(rotation);

            comboRadius = (Game1.Instance.playerCell.radius + radius);
        }


        public override void Update(GameTime gameTime)
        {
            s.Start();

            sphere = new BoundingSphere(new Vector3(center.X, center.Y, 0), radius);

            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float timeBetweenDirection = s.Elapsed.Seconds;

            keyState = Keyboard.GetState();

            center = pos + new Vector2(sprite.Width / 2, sprite.Height / 2);

            distance = Vector2.Distance(Game1.Instance.playerCell.center, center) - comboRadius;

            if (!keyState.IsKeyDown(Keys.J) || distance >= 200)  // when Q is NOT pressed OR the distance between Glucode and Player is greater than 200
            {
                Vector2 newRandomTarget = new Vector2(randomLook1 - center.X, randomLook2 - center.Y);   // take positions away to get the newly translated vector

                rotation = (float) Math.Acos(Vector2.Dot(basis, Vector2.Normalize(newRandomTarget)));

                Console.WriteLine(checkNegative);
                if (timeBetweenDirection > 5)
                {
                    checkNegative = random.Next(-2, 2); // Chooses either -1 or 0


                    s.Reset();
                    randomLook1 = (float) random.Next(0, 2400);  // Number between 1 and -1 where the X axis looks
                    randomLook2 = (float) random.Next(0, 1800);  // Number between 1 and -1 where the Y axis looks
                }

                if (checkNegative < 0)       // If checkNegative is -1, it changes the rotation
                {
                    rotation = -rotation;
                }

                look.X = (float)Math.Sin(rotation);
                look.Y = (float)-Math.Cos(rotation);

                

                pos += look * speed * timeDelta;
            }




            if (keyState.IsKeyDown(Keys.J) && distance < 200)
            {
                TrackPlayer();

                pos += look * speed * timeDelta;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(sprite, pos, Color.White);
        }
    }
}
