using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine;

namespace GGJ_2021
{
    public class OutlineGrid: GameObjectComponent
    {
        private Rectangle[] Rects;
        private int Rows = 12;
        private int Columns = 14;
        private int Size = 50;

        public override void Start()
        {
            Rects = new Rectangle[Rows * Columns];
            Vector2 InitPos = gameObject.GetComponent<DrawGrid>().InitialPosition;

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    Rects[i * Columns + j] = new Rectangle((int)InitPos.X + j * Size, (int)InitPos.Y + i * Size, Size, Size);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Rectangle R in Rects)
            {
                if (R.Contains(Input.GetMousePosition()))
                {
                    HitBoxDebuger.DrawNonFilledRectangle(R);
                    break;
                }
            }


            foreach (Rectangle R in Rects)
            {
                if (Input.GetMouseClick(MouseButtons.LeftClick))
                {
                    GameObject[] GOs = SceneManager.ActiveScene.GameObjects.FindAll(Item => Item.Tag == "Command").ToArray();

                    if (R.Contains(Input.GetMousePosition()))
                    {
                        bool BREAK = false;
                        foreach (GameObject GO in GOs)
                            if (GO.GetComponent<SpriteRenderer>().Sprite.DynamicScaledRect().Intersects(R))
                            {
                                BREAK = true;
                                break;
                            }

                        if (BREAK)
                            break;

                        Commands.DrawRectangle(R, Color.Red);
                        //Commands.DrawCircle(R.Location.ToVector2(), R.Size.X/2, Color.Red);
                    }
                }
                else if(Input.GetMouseClick(MouseButtons.RightClick))
                {
                    GameObject[] GOs = SceneManager.ActiveScene.GameObjects.FindAll(Item => Item.Tag == "Command").ToArray();

                    if (R.Contains(Input.GetMousePosition()))
                    {
                        bool BREAK = false;
                        foreach (GameObject GO in GOs)
                            if (GO.GetComponent<SpriteRenderer>().Sprite.DynamicScaledRect().Intersects(R))
                            {
                                BREAK = true;
                                GO.ShouldBeDeleted = true;
                                break;
                            }

                        if (BREAK)
                            break;
                    }
                }
            }
        }
    }
}
