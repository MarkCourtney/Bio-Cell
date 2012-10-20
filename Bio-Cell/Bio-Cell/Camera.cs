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
    public class Camera
    {
        Vector2 centerSprite;
        Stopwatch stopWatch = new Stopwatch();
        bool keyPressed;

        public Camera(Vector2 CenterSprite, Stopwatch Stopwatch)
        {
            centerSprite = CenterSprite;
            Zoom = 1f;
            stopWatch = Stopwatch;
            keyPressed = false;
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }

        public Matrix TransformMatrix
        {
            get
            {
                KeyboardState keyState = Keyboard.GetState();
                GamePadState gamePad = GamePad.GetState(PlayerIndex.One);

                if ((keyState.IsKeyDown(Keys.U) || gamePad.Buttons.Y == ButtonState.Pressed) && keyPressed == false)
                {
                    if (stopWatch.Elapsed.Seconds >= 3)
                    {
                        Zoom = 1;
                        keyPressed = true;

                        if (stopWatch.Elapsed.Seconds >= 13)
                        {
                            stopWatch.Reset();
                            keyPressed = false;
                        }

                        return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(-centerSprite.X + 400, -centerSprite.Y + 300, 0);
                    }
                    else
                    {
                        Zoom = 0.3333f;
                        return Matrix.CreateScale(Zoom);
                    }


                    
                }
                else
                {
                    Zoom = 1f;
                    return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(-centerSprite.X + 400, -centerSprite.Y + 300, 0);
                }
            }
        }
    }
}