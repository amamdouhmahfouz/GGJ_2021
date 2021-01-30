//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using MyEngine;

//namespace GGJ_2021
//{
//    public enum Shapes { None, Tile, Rectangle, Circle };
//    public class WritableCommand : GameObjectComponent
//    {



//        public string textCommand;
//        public SpriteFont Font;
//        public Vector2 Origin;
//        public Color Color;
//        public bool CustomOrigin = false;
//        public SpriteEffects spriteEffects;
//        public string[] splitCommands;

//        private Transform transform;
//        private float prevTime;
//        private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZD0D1D2D3D4D5D6D7D8D9OemSemicolonOemPlus";

//        private bool prevKeyShift = false;
//        private Vector2 shapePosition;
//        private Color shapeColor = Color.Green;
//        private bool threadFinished = false;
//        private Shapes Shape;
//        private bool threadInProgress = false;
//        private Shapes currCommand = Shapes.None;


//        public WritableCommand(SpriteFont font)
//        {
//            textCommand = "";
//            if (gameObject != null)
//                transform = gameObject.Transform;
//            Origin = Vector2.Zero;
//            Color = Color.Green;
//            Font = font;
//            spriteEffects = SpriteEffects.None;
//            prevTime = 0.0f;
//        }


//        public override void Start()
//        {
//            gameObject.Layer = LayerUI.GetLayer("Text");
//            if (transform == null)
//                transform = gameObject.Transform;

//            //transform.Position = new Vector2(Setup.graphics.PreferredBackBufferWidth / 2, Setup.graphics.PreferredBackBufferHeight / 2);
//        }

//        public override void Draw(SpriteBatch spriteBatch)
//        {
//            if (!CustomOrigin)
//                Origin = Font.MeasureString(textCommand) * 0.5f * transform.Scale;



//            spriteBatch.DrawString(Font, textCommand, transform.Position, Color, MathHelper.ToRadians(transform.Rotation), Origin, transform.Scale, spriteEffects, gameObject.Layer);
//        }

//        public void LoadFont(string Name)
//        {
//            Font = Setup.Content.Load<SpriteFont>(Name);
//        }



//        public override void Update(GameTime gameTime)
//        {
//            var keyboardState = Keyboard.GetState();
//            var keys = keyboardState.GetPressedKeys();
//            float nowTime = (float)gameTime.TotalGameTime.TotalSeconds;

//            // ============================ Drawing Shapes ===============================
//            if (threadFinished)
//            {
//                //Draw Commands
//                switch (Shape)
//                {
//                    case Shapes.Tile:
//                        Commands.DrawRectangle(new Rectangle((int)shapePosition.X, (int)shapePosition.Y, 26, 26), shapeColor);
//                        threadFinished = false;
//                        Threader.Invoke(SetShapePosition, 0);
//                        break;

//                    case Shapes.Rectangle:
//                        Commands.DrawRectangle(new Rectangle((int)shapePosition.X, (int)shapePosition.Y, 26, 4 * 26), shapeColor);
//                        threadFinished = false;
//                        Threader.Invoke(SetShapePosition, 0);
//                        break;

//                    case Shapes.Circle:
//                        Commands.DrawCircle(new Vector2((int)shapePosition.X, (int)shapePosition.Y), 15, shapeColor);
//                        threadFinished = false;
//                        Threader.Invoke(SetShapePosition, 0);
//                        break;

//                    default:
//                        break;
//                }


//                threadFinished = false;
//                threadInProgress = false;
//                return;
//            }
//            // ===========================================================================


//            // ============================ Get input commands ===============================
//            if (keys.Length > 0 && (nowTime - prevTime) >= 0.19)
//            {
//                prevTime = nowTime;
//                var keyValue = keys[0].ToString();
//                //System.Console.WriteLine(keyValue);

//                // A check for parentheses
//                if (keyValue == "LeftShift")
//                {
//                    prevKeyShift = true;
//                    return;
//                }

//                // ===================================== Enter Pressed =========================================
//                if (keyValue == "Enter") // new line, i.e new command
//                {
//                    /*   Check if the command written is correct, else erase that line
//                     *   Split the textCommand into a list of commands
//                     */


//                    // Split the whole textCommand split into separate lines
//                    string[] stringSeparators = new string[] { "\n" };
//                    splitCommands = textCommand.Split(stringSeparators, StringSplitOptions.None);

//                    System.Console.WriteLine("Printing splitCommands");
//                    for (int i = 0; i < splitCommands.Length; i++)
//                    {
//                        System.Console.WriteLine(splitCommands[i]);
//                    }
//                    System.Console.WriteLine("End Printing splitCommands\n");

//                    // Add newline
//                    textCommand += "\n";
//                    //System.Console.WriteLine(textCommand);

//                    //System.Console.WriteLine("splitCommands Length: " + splitCommands.Length);

//                    int index = 0;
//                    // Find index of second to last '\n' 
//                    for (int i = textCommand.Length - 3; i >= 0; i--)
//                    {
//                        if (textCommand[i] == '\n')
//                        {
//                            index = i;
//                            break;
//                        }
//                    }

//                    if (splitCommands.Length == 24)
//                    {
//                        textCommand = "";
//                    }

//                    if (splitCommands[splitCommands.Length - 1] == "DRAWTILE();")
//                    {
//                        gameObject.GetComponent<DrawGrid>().Enabled = true;
//                        gameObject.GetComponent<OutlineGrid>().Enabled = true;

//                        gameObject.GetComponent<OutlineGrid>().Color = shapeColor;
//                        gameObject.GetComponent<OutlineGrid>().Shape = Shapes.Tile;
//                        //Shape = Shapes.Tile;
//                        //threadInProgress = true;
//                        //Threader.Invoke(SetShapePosition, 0);
//                    }

//                    else if (splitCommands[splitCommands.Length - 1] == "DRAWCIRCLE();")
//                    {
//                        gameObject.GetComponent<DrawGrid>().Enabled = true;
//                        gameObject.GetComponent<OutlineGrid>().Enabled = true;

//                        gameObject.GetComponent<OutlineGrid>().Color = shapeColor;
//                        gameObject.GetComponent<OutlineGrid>().Shape = Shapes.Circle;

//                        //Shape = Shapes.Circle;
//                        //threadInProgress = true;
//                        //Threader.Invoke(SetShapePosition, 0);
//                    }
//                    else if (splitCommands.Length > 1 && splitCommands[splitCommands.Length - 1].Length > 4 && splitCommands[splitCommands.Length - 1].Substring(0, splitCommands[splitCommands.Length - 1].Length - 2) == "COLOR="
//                        && splitCommands[splitCommands.Length - 1].Substring(splitCommands[splitCommands.Length - 1].Length - 1) == ";"
//                        && splitCommands[splitCommands.Length - 1].Length == 8)
//                    {


//                        switch (splitCommands[splitCommands.Length - 1].Substring(splitCommands[splitCommands.Length - 1].Length - 2)[0])
//                        {
//                            case '0':
//                                shapeColor = Color.Orange;
//                                break;
//                            case '1':
//                                shapeColor = Color.White;
//                                break;
//                            case '2':
//                                shapeColor = Color.Green;
//                                break;
//                            case '3':
//                                shapeColor = Color.Blue;
//                                break;
//                            case '4':
//                                shapeColor = Color.Red;
//                                break;
//                            case '5':
//                                shapeColor = Color.Yellow;
//                                break;
//                            case '6':
//                                shapeColor = Color.Cyan;
//                                break;
//                            case '7':
//                                shapeColor = Color.Aquamarine;
//                                break;


//                            default:
//                                break;

//                        }

//                        gameObject.GetComponent<OutlineGrid>().Color = shapeColor;

//                    }
//                    else
//                    {
//                        if (splitCommands.Length > 1)
//                            textCommand = textCommand.Remove(index + 1);
//                        else
//                            textCommand = textCommand.Remove(index);
//                        textCommand += "INVALID COMMAND! u IDIOT\n";
//                    }


//                    // Check if command written by user is an actual command
//                    //if (Array.Exists(splitCommands, element => element == "DRAWRECTANGLE();"))
//                    //{
//                    //    Commands.DrawRectangle(new Rectangle(100, 0, 100, 50), Color.Green);
//                    //}

//                }
//                // ===================================== End Enter Pressed =========================================

//                else if (keyValue == "Back") //delete a character
//                {
//                    if (textCommand.Length != 0 && textCommand[textCommand.Length - 1] != '\n')
//                        textCommand = textCommand.Remove(textCommand.Length - 1);

//                    string[] stringSeparators = new string[] { "\n" };
//                    splitCommands = textCommand.Split(stringSeparators, StringSplitOptions.None);


//                }

//                else if (keyValue == "OemSemicolon")
//                {
//                    textCommand += ";";
//                }
//                else if (keyValue == "OemPlus")
//                {
//                    textCommand += "=";
//                }
//                else if (alphabet.Contains(keyValue)) //append the actual character to be drawn on screen
//                {

//                    if (keyValue.Length == 2)
//                    {
//                        if (keyValue[1].ToString() == "9" && prevKeyShift)
//                        {
//                            textCommand += "(";
//                            prevKeyShift = false;
//                        }
//                        else if (keyValue[1].ToString() == "0" && prevKeyShift)
//                        {
//                            textCommand += ")";
//                            prevKeyShift = false;
//                        }

//                        else
//                        {
//                            textCommand += keyValue[1].ToString();
//                        }


//                    }
//                    else
//                    {
//                        textCommand += keyValue.ToString();

//                    }

//                }

//            }
//            // ============================== End Get input Commands ==================================

//        }


//        private void SetShapePosition()
//        {
//            /////////////////////////////////////
//            // Set position of the drawn shape 
//            // Use the mouse to get the position
//            /////////////////////////////////////


//            while (!(Mouse.GetState().LeftButton == ButtonState.Pressed)) { }

//            Vector2 Temp = Vector2.One;
//            Temp.X = Setup.graphics.PreferredBackBufferWidth;
//            Temp.Y = Setup.graphics.PreferredBackBufferHeight;
//            shapePosition = MathCompanion.Clamp(Setup.resolutionIndependentRenderer.ScaleMouseToScreenCoordinates(Mouse.GetState().Position.ToVector2()), Vector2.Zero, Temp);
//            System.Console.WriteLine(shapePosition);

//            threadFinished = true;
//        }


//        public override GameObjectComponent DeepCopy(GameObject Clone)
//        {
//            WritableCommand clone = this.MemberwiseClone() as WritableCommand;
//            clone.transform = Clone.Transform;
//            Clone.Layer = LayerUI.GetLayer("WritableCommand");

//            return clone;
//        }


//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine;

namespace GGJ_2021
{
    public enum Shapes { None, Tile, Rectangle, Circle };
    public class WritableCommand : GameObjectComponent
    {



        public string textCommand;
        public SpriteFont Font;
        public Vector2 Origin;
        public Color Color;
        public bool CustomOrigin = false;
        public SpriteEffects spriteEffects;
        public string[] splitCommands;
        public List<string> splitCommands2;

        private Transform transform;
        private float prevTime;
        private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZD0D1D2D3D4D5D6D7D8D9OemSemicolonOemPlus";

        private bool prevKeyShift = false;
        private Vector2 shapePosition;
        private Color shapeColor = Color.Green;
        private bool threadFinished = false;
        private Shapes Shape;
        private bool threadInProgress = false;
        private Shapes currCommand = Shapes.None;
        private bool restartScreen = false;
        private bool submitted = false;
        private int countCharacters = 0;

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

            splitCommands2 = new List<string>();
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


            if (restartScreen || (gameObject.GetComponent<ClosePopup>().countPopups >= 1 && gameObject.GetComponent<ClosePopup>().Enabled)) return;

            gameObject.GetComponent<OutlineGrid>().Enabled = true;
            SceneManager.ActiveScene.FindGameObjectWithTag("BlueDeathScreen").Active = false;
            // ============================ Get input commands ===============================
            if (keys.Length > 0 && (nowTime - prevTime) >= 0.19)
            {
                prevTime = nowTime;
                var keyValue = keys[0].ToString();

                // A check for parentheses
                if (keyValue == "LeftShift")
                {
                    prevKeyShift = true;
                    return;
                }

                // ===================================== Enter Pressed =========================================
                if (keyValue == "Enter") // new line, i.e new command
                {
                    countCharacters = 0;
                    // Split the whole textCommand split into separate lines
                    string[] stringSeparators = new string[] { "\n" };
                    splitCommands = textCommand.Split(stringSeparators, StringSplitOptions.None);
                    List<string> splits = textCommand.Split(stringSeparators, StringSplitOptions.None).ToList<string>();

                    

                    if (splits.Count >= 1)
                    {
                        System.Console.WriteLine("Count= " + splits.Count);
                        splitCommands2.Add(splits[splits.Count - 1]);

                        System.Console.WriteLine("Printing splitCommands2");
                        for (int i = 0; i < splitCommands2.Count; i++)
                        {
                            System.Console.WriteLine(splitCommands2[i]);
                        }
                        System.Console.WriteLine("End Printing splitCommands2\n");
                    }

                    // Add newline
                    textCommand += "\n";

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

                    if (splitCommands.Length == 21)
                    {
                        textCommand = "";
                    } else if (splitCommands.Length == 5)
                    {
                        Errors.WindowSpam();
                        gameObject.GetComponent<ClosePopup>().Enabled = true;
                        gameObject.GetComponent<OutlineGrid>().Enabled = false;
                    } 

                    if (splitCommands2.Count >= 1 && splitCommands2[splitCommands2.Count - 1] == "DRAWTILE();")
                    {
                        gameObject.GetComponent<DrawGrid>().Enabled = true;
                        gameObject.GetComponent<OutlineGrid>().Enabled = true;

                        gameObject.GetComponent<OutlineGrid>().Color = shapeColor;
                        gameObject.GetComponent<OutlineGrid>().Shape = Shapes.Tile;

                    }

                    else if (splitCommands2.Count >= 1 && splitCommands2[splitCommands2.Count - 1] == "DRAWCIRCLE();")
                    {
                        gameObject.GetComponent<DrawGrid>().Enabled = true;
                        gameObject.GetComponent<OutlineGrid>().Enabled = true;

                        gameObject.GetComponent<OutlineGrid>().Color = shapeColor;
                        gameObject.GetComponent<OutlineGrid>().Shape = Shapes.Circle;

                    }
                    else if (splitCommands2.Count >= 1 && splitCommands2[splitCommands2.Count - 1] == "PUBLISH();")
                    {
                        if (!submitted)
                        {
                            SceneManager.ActiveScene.FindGameObjectWithTag("BlueDeathScreen").Active = true;
                            restartScreen = true;
                            Threader.Invoke(SleepRestart, 0);

                            if (splitCommands.Length >= 2)
                            {
                                GameObject[] gameObjects = SceneManager.ActiveScene.FindGameObjectsWithTag("Command");
                                for (int i = gameObjects.Length - 1; i >= (int)(gameObjects.Length * 0.7); i--)
                                    gameObjects[i].ShouldBeDeleted = true;
                            }

                        }


                        // TODO: final submit is here
                        
                    }
                    else if (splitCommands2[splitCommands2.Count - 1].Length == 8 && splitCommands2[splitCommands2.Count - 1].Substring(0, splitCommands2[splitCommands2.Count - 1].Length - 2) == "COLOR="
                        && splitCommands2[splitCommands2.Count - 1].Substring(splitCommands2[splitCommands2.Count - 1].Length - 1) == ";")
                    {


                        switch (splitCommands2[splitCommands2.Count - 1].Substring(splitCommands2[splitCommands2.Count - 1].Length - 2)[0])
                        {
                            case '0':
                                shapeColor = Color.Orange;
                                break;
                            case '1':
                                shapeColor = Color.White;
                                break;
                            case '2':
                                shapeColor = Color.Green;
                                break;
                            case '3':
                                shapeColor = Color.Blue;
                                break;
                            case '4':
                                shapeColor = Color.Red;
                                break;
                            case '5':
                                shapeColor = Color.Yellow;
                                break;
                            case '6':
                                shapeColor = Color.Cyan;
                                break;
                            case '7':
                                shapeColor = Color.Aquamarine;
                                break;


                            default:
                                break;

                        }

                        gameObject.GetComponent<OutlineGrid>().Color = shapeColor;

                    }
                    else
                    {
                        if (splitCommands2.Count > 1 && textCommand.Length >= index+1)
                            textCommand = textCommand.Remove(index + 1);
                        else if (textCommand.Length >= index)
                            textCommand = textCommand.Remove(index);
                        textCommand += "INVALID COMMAND!\n";
                    }


                }
                // ===================================== End Enter Pressed =========================================

                else if (keyValue == "Back") //delete a character
                {
                    if (textCommand.Length != 0 && textCommand[textCommand.Length - 1] != '\n')
                        textCommand = textCommand.Remove(textCommand.Length - 1);

                    string[] stringSeparators = new string[] { "\n" };
                    splitCommands = textCommand.Split(stringSeparators, StringSplitOptions.None);

                    countCharacters--;
                }

                else if (keyValue == "OemSemicolon")
                {
                    textCommand += ";";
                }
                else if (keyValue == "OemPlus")
                {
                    textCommand += "=";
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
                        
                        if (countCharacters >= 28)
                        {

                        }
                        else
                        {
                            textCommand += keyValue.ToString();
                            countCharacters++;
                        }

                    }

                }

            }
            // ============================== End Get input Commands ==================================

        }

        private void SleepRestart()
        {
            Thread.Sleep(5000);
            restartScreen = false;
            submitted = true;
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
            shapePosition = MathCompanion.Clamp(Setup.resolutionIndependentRenderer.ScaleMouseToScreenCoordinates(Mouse.GetState().Position.ToVector2()), Vector2.Zero, Temp);
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
