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
    public class DeathMarker : Entity
    {
        Vector2 currentPos;

        public DeathMarker(Vector2 Pos) 
        {
            currentPos = Pos;
            sprite = Game1.Instance.Content.Load<Texture2D>("DeathPosition");
        }

        public override void LoadContent()
        {
            pos = currentPos;
            Alive = true;
        }


        public override void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(sprite, pos, Color.White);
        }
    }
}
