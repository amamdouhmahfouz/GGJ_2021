using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyEngine;

namespace GGJ_2021
{
    public class ClosePopup: GameObjectComponent
    {
        public int countPopups = 25;
        public override void Update(GameTime gameTime)
        {
            GameObject[] Popups = SceneManager.ActiveScene.FindGameObjectsWithTag("PopUp");

            if (Popups != null)
            {
                int Count = Popups.Length - 1;

                if (Input.GetMouseClickDown(MouseButtons.LeftClick))
                    for (int i = Count; i >= 0; i--)
                        if (Popups[Count - i].GetComponent<BoxCollider2D>().Contains(Input.GetMousePosition()))
                        {
                            Popups[Count - i].ShouldBeDeleted = true;
                            countPopups--;
                        }
            }
        }
    }
}
