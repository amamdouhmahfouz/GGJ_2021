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
        private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZD0D1D2D3D4D5D6D7D8D9OemSemicolonOemPlusF1EscapeLeftShiftRightShift";

        private Vector2 shapePosition;
        private Color shapeColor = Color.Green;
        private Shapes Shape;

        private bool restartScreen = false;
        private bool submitted = false;
        private int countCharacters = 0;
        private bool restartScreenOnce = false; //to see if restart screen appeared once 
        private bool popupsOnce = false; //to see if popups  appeared once 
        private bool documentationOpened = false;
        private bool isOutlineEnabledOnce = false;
        private const int NUM_COMMAND_FOR_POPUPS = 5;
        private int countCommandsForPopups = 0; //include count for opening and closing documentation, and any actual command
        private int countDrawCommands = 0;

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
            //gameObject.Layer = LayerUI.GetLayer("WritableCommand");
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
            gameObject.GetComponent<DrawGrid>().Enabled = true;

            if (restartScreen || (gameObject.GetComponent<ClosePopup>().countPopups >= 1 && gameObject.GetComponent<ClosePopup>().Enabled))
            {
                gameObject.GetComponent<OutlineGrid>().Enabled = false;
                return;
            }
 
            if ((restartScreenOnce || popupsOnce) && isOutlineEnabledOnce)
                gameObject.GetComponent<OutlineGrid>().Enabled = true;
            SceneManager.ActiveScene.FindGameObjectWithTag("BlueDeathScreen").Active = false;

            // ============================ Get input commands ===============================
            if (keys.Length > 0 && (nowTime - prevTime) >= 0.19)
            {
                prevTime = nowTime;
                var keyValue = keys[0].ToString();

                // ===================================== Enter Pressed =========================================
                if (keyValue == "Enter") // new line, i.e new command
                {
                    countCommandsForPopups++;
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

                    if (splitCommands2.Count == 21)
                    {
                        textCommand = "";
                    }
                    if (countCommandsForPopups ==5)/*else if (splitCommands2.Count == 5)*/
                    {
                        popupsOnce = true;
                        Errors.WindowSpam();
                        gameObject.GetComponent<ClosePopup>().Enabled = true;
                        gameObject.GetComponent<OutlineGrid>().Enabled = false;
                        Threader.Invoke(ContinueStory, 3000);
                    } 

                    if (splitCommands2.Count >= 1 && splitCommands2[splitCommands2.Count - 1] == "DRAWTILE();")
                    {
                        gameObject.GetComponent<DrawGrid>().Enabled = true;
                        gameObject.GetComponent<OutlineGrid>().Enabled = true;

                        gameObject.GetComponent<OutlineGrid>().Color = shapeColor;
                        gameObject.GetComponent<OutlineGrid>().Shape = Shapes.Tile;
                        isOutlineEnabledOnce = true;
                        countDrawCommands++;
                    }

                    else if (splitCommands2.Count >= 1 && splitCommands2[splitCommands2.Count - 1] == "DRAWCIRCLE();")
                    {
                        gameObject.GetComponent<DrawGrid>().Enabled = true;
                        gameObject.GetComponent<OutlineGrid>().Enabled = true;

                        gameObject.GetComponent<OutlineGrid>().Color = shapeColor;
                        gameObject.GetComponent<OutlineGrid>().Shape = Shapes.Circle;
                        isOutlineEnabledOnce = true;
                        countDrawCommands++;
                    }
                    else if (splitCommands2.Count >= 1 && splitCommands2[splitCommands2.Count - 1] == "PUBLISH();")
                    {
                        if (!submitted && countCommandsForPopups != 5 && countDrawCommands >= 1)
                        {
                            if (!popupsOnce) 
                            {
                                popupsOnce = true;
                                Errors.WindowSpam();
                                gameObject.GetComponent<ClosePopup>().Enabled = true;
                                gameObject.GetComponent<OutlineGrid>().Enabled = false;
                                Threader.Invoke(ContinueStory, 3000);
                                return;
                            }

                            SceneManager.ActiveScene.FindGameObjectWithTag("BlueDeathScreen").Active = true;
                            restartScreen = true;
                            Threader.Invoke(SleepRestart, 0);

                            

                            restartScreenOnce = true;

                            if (splitCommands.Length >= 2)
                            {
                                GameObject[] gameObjects = SceneManager.ActiveScene.FindGameObjectsWithTag("Command");
                                for (int i = gameObjects.Length - 1; i >= (int)(gameObjects.Length * 0.7); i--)
                                    gameObjects[i].ShouldBeDeleted = true;
                            }

                        } else if (submitted && countDrawCommands >= 1)
                        {
                            SceneManager.ActiveScene.FindGameObjectWithName("STORY").Active = true;
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
                else if (keyValue == "F1")
                {
                    countCommandsForPopups++;
                    if (countCommandsForPopups == 5)
                    {
                        popupsOnce = true;
                        Errors.WindowSpam();
                        gameObject.GetComponent<ClosePopup>().Enabled = true;
                        gameObject.GetComponent<OutlineGrid>().Enabled = false;
                        Threader.Invoke(ContinueStory, 3000);
                        return;
                    }

                    SceneManager.ActiveScene.FindGameObjectWithName("DocumentationBook").Active = !SceneManager.ActiveScene.FindGameObjectWithName("DocumentationBook").Active;
                    if (SceneManager.ActiveScene.FindGameObjectWithName("DocumentationBook").Active)
                    {
                        documentationOpened = true;
                        gameObject.GetComponent<OutlineGrid>().drawEnable = false;
                    }
                    else
                    {
                        documentationOpened = false;
                        gameObject.GetComponent<OutlineGrid>().drawEnable = true;
                    }
                    
                } 
                else if (keyValue == "Escape")
                {
                    countCommandsForPopups++;
                    
                    documentationOpened = false;
                    SceneManager.ActiveScene.FindGameObjectWithName("DocumentationBook").Active = false;
                    gameObject.GetComponent<OutlineGrid>().drawEnable = true;

                    if (countCommandsForPopups == 5)/*else if (splitCommands2.Count == 5)*/
                    {
                        popupsOnce = true;
                        Errors.WindowSpam();
                        gameObject.GetComponent<ClosePopup>().Enabled = true;
                        gameObject.GetComponent<OutlineGrid>().Enabled = false;
                        Threader.Invoke(ContinueStory, 3000);
                    }

                }
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

                    if (countCharacters < 26)
                    {
                        if (Input.GetKey(Keys.LeftShift) || Input.GetKey(Keys.RightShift))
                        {
                            if (Input.GetKey(Keys.D9))
                            {
                                //System.Console.WriteLine("Left+9 pressed");
                                textCommand += "(";
                                countCharacters++;
                            }
                            else if (Input.GetKey(Keys.D0))
                            {
                                textCommand += ")";
                                countCharacters++;
                            }
                        }
                        else
                        {                           
                            if (keyValue.ToString().Length == 1)
                                textCommand += keyValue.ToString();
                            else
                                textCommand += keyValue.ToString()[1];
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
            SceneManager.ActiveScene.FindGameObjectWithName("STORY").Active = true;
        }

        private void ContinueStory()
        {
            SceneManager.ActiveScene.FindGameObjectWithName("AllPopups").Active = false;

            SceneManager.ActiveScene.FindGameObjectWithName("STORY").Active = true;

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
