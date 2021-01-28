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

        public string textCommand;
        public SpriteFont Font;
        public Vector2 Origin;
        public Color Color;
        public bool CustomOrigin = false;
        public SpriteEffects spriteEffects;
        public string[] splitCommands;

        private Transform transform;
        private float prevTime;
        private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZD0D1D2D3D4D5D6D7D8D9";


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

            if (keys.Length > 0 && (nowTime-prevTime) >= 0.17)
            {
                prevTime = nowTime;
                var keyValue = keys[0].ToString();
                System.Console.WriteLine(keyValue);
                if (keyValue == "Enter") // new line, i.e new command
                {
                    // Split the whole textCommand split into separate lines
                    string[] stringSeparators = new string[] { "\n" };
                    splitCommands = textCommand.Split(stringSeparators, StringSplitOptions.None);

                    // Add newline
                    textCommand += "\n";
                    System.Console.WriteLine(textCommand);

                    // Check if command written by user is an actual command
                    bool a = Array.Exists(splitCommands, element => element == "DRAWRECTANGLE");

                }
                else if (keyValue == "Back") //delete a character
                {
                    if (textCommand.Length != 0 && textCommand[textCommand.Length - 1] != '\n')
                        textCommand = textCommand.Remove(textCommand.Length-1);

                    string[] stringSeparators = new string[] { "\n" };
                    splitCommands = textCommand.Split(stringSeparators, StringSplitOptions.None);
                }

                else if (alphabet.Contains(keyValue))
                {
                    if (keyValue.Length == 2)
                        textCommand += keyValue[1].ToString();
                    else
                        textCommand += keyValue.ToString();
                }
                
            }
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
