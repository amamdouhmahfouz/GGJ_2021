using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine;

namespace GGJ_2021
{
    public class WritableCommand : GameObjectComponent
    {

        public enum Shapes { Rectangle, Circle};

        public string textCommand;
        public SpriteFont Font;
        public Vector2 Origin;
        public Color Color;
        public bool CustomOrigin = false;
        public SpriteEffects spriteEffects;
        public string[] splitCommands;

        private Transform transform;
        private float prevTime;
        private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZD0D1D2D3D4D5D6D7D8D9OemSemicolon";

        private bool prevKeyShift = false;
        private Vector2 shapePosition;
        private bool threadFinished = false;
        private Shapes Shape;

        public WritableCommand(SpriteFont font)
        {
            textCommand = "";
            if (gameObject != null)
                transform = gameObject.Transform;
            Origin = Vector2.Zero;
            Color = Color.Green;
            Font = font;
            spriteEffects = SpriteEffects.None;
            prevTime = 0.0f;
        }


        public override void Start()
        {
            gameObject.Layer = LayerUI.GetLayer("Text");
            if (transform == null)
                transform = gameObject.Transform;

            //transform.Position = new Vector2(Setup.graphics.PreferredBackBufferWidth / 2, Setup.graphics.PreferredBackBufferHeight / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!CustomOrigin)
                Origin = Font.MeasureString(textCommand) * 0.5f * transform.Scale;


            
            spriteBatch.DrawString(Font, textCommand, transform.Position, Color, MathHelper.ToRadians(transform.Rotation), Origin, transform.Scale, spriteEffects, gameObject.Layer);
        }

        public void LoadFont(string Name)
        {
            Font = Setup.Content.Load<SpriteFont>(Name);
        }

        
 
        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var keys = keyboardState.GetPressedKeys();
            float nowTime = (float)gameTime.TotalGameTime.TotalSeconds;

            // ============================ Drawing Shapes ===============================
            if (threadFinished)
            {
                //Draw Commands
                switch (Shape)
                {
                    case Shapes.Rectangle:
                        Commands.DrawRectangle(new Rectangle((int)shapePosition.X, (int)shapePosition.Y, 100, 50), Color.Green);
                        break;

                    case Shapes.Circle:
                        Commands.DrawCircle(new Vector2((int)shapePosition.X, (int)shapePosition.Y), 50, Color.Blue);
                        break;

                    default:
                        break;
                }

                
                threadFinished = false;
                return;
            }
            // ===========================================================================


            // ============================ Get input commands ===============================
            if (keys.Length > 0 && (nowTime-prevTime) >= 0.19)
            {
                prevTime = nowTime;
                var keyValue = keys[0].ToString();
                System.Console.WriteLine(keyValue);

                // A check for parentheses
                if (keyValue == "LeftShift")
                {
                    prevKeyShift = true;
                    return;
                }


                if (keyValue == "Enter") // new line, i.e new command
                {
                    /*   Check if the command written is correct, else erase that line
                     *   Split the textCommand into a list of commands
                     */


                    // Split the whole textCommand split into separate lines
                    string[] stringSeparators = new string[] { "\n" };
                    splitCommands = textCommand.Split(stringSeparators, StringSplitOptions.None);

                    // Add newline
                    textCommand += "\n";
                    System.Console.WriteLine(textCommand);

                    int index = 0;
                    // Find index of second to last '\n' 
                    for (int i = textCommand.Length - 3; i >= 0; i--)
                    {
                        if (textCommand[i] == '\n')
                        {
                            index = i;
                            break;
                        }
                    }

                    if (splitCommands[splitCommands.Length - 1] == "DRAWRECTANGLE();")
                    {
                        Shape = Shapes.Rectangle;
                        Threader.Invoke(SetShapePosition, 0);
                    }
                    else if (splitCommands[splitCommands.Length - 1] == "DRAWCIRCLE();")
                    {
                        Shape = Shapes.Circle;
                        Threader.Invoke(SetShapePosition, 0);
                    }
                    //else if (splitCommands[splitCommands.Length - 1] == "DRAWCIRCLE();")
                    //{
                    //    Commands.DrawATexture
                    //}
                    else
                    {
                        if (splitCommands.Length > 1)
                            textCommand = textCommand.Remove(index+1);
                        else
                            textCommand = textCommand.Remove(index);
                        textCommand += "INVALID COMMAND! u IDIOT\n";
                    }


                    // Check if command written by user is an actual command
                    //if (Array.Exists(splitCommands, element => element == "DRAWRECTANGLE();"))
                    //{
                    //    Commands.DrawRectangle(new Rectangle(100, 0, 100, 50), Color.Green);
                    //}

                }
                else if (keyValue == "Back") //delete a character
                {
                    if (textCommand.Length != 0 && textCommand[textCommand.Length - 1] != '\n')
                        textCommand = textCommand.Remove(textCommand.Length-1);

                    string[] stringSeparators = new string[] { "\n" };
                    splitCommands = textCommand.Split(stringSeparators, StringSplitOptions.None);


                }

                else if (keyValue == "OemSemicolon")
                {
                    textCommand += ";";
                }

                else if (alphabet.Contains(keyValue)) //append the actual character to be drawn on screen
                {

                    if (keyValue.Length == 2)
                    {
                        if (keyValue[1].ToString() == "9" && prevKeyShift)
                        {
                            textCommand += "(";
                            prevKeyShift = false;
                        } 
                        else if (keyValue[1].ToString() == "0" && prevKeyShift)
                        {
                            textCommand += ")";
                            prevKeyShift = false;
                        }

                        else
                        {
                            textCommand += keyValue[1].ToString();
                        }
                         

                    }
                    else
                    {
                        textCommand += keyValue.ToString();

                    }

                }
                
            }
            // ============================== End Get input Commands ==================================

        }


        private void SetShapePosition()
        {
            /////////////////////////////////////
            // Set position of the drawn shape 
            // Use the mouse to get the position
            /////////////////////////////////////

            while (!(Mouse.GetState().LeftButton == ButtonState.Pressed)) { }

            Vector2 Temp = Vector2.One;
            Temp.X = Setup.graphics.PreferredBackBufferWidth;
            Temp.Y = Setup.graphics.PreferredBackBufferHeight;
            shapePosition =  MathCompanion.Clamp(Setup.resolutionIndependentRenderer.ScaleMouseToScreenCoordinates(Mouse.GetState().Position.ToVector2()), Vector2.Zero, Temp);
            System.Console.WriteLine(shapePosition);

            threadFinished = true;
        }


        public override GameObjectComponent DeepCopy(GameObject Clone)
        {
            WritableCommand clone = this.MemberwiseClone() as WritableCommand;
            clone.transform = Clone.Transform;
            Clone.Layer = LayerUI.GetLayer("WritableCommand");

            return clone;
        }


    }
}
