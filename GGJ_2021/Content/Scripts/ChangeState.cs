using Microsoft.Xna.Framework;
using MyEngine;

namespace GGJ_2021
{
    public class ChangeState : GameObjectComponent
    {

        public GameObject gameObjectPlay;
        public GameObject gameObjectCredits;
        public GameObject gameObjectExit;

        public override void Start()
        {
            gameObjectPlay = SceneManager.ActiveScene.FindGameObjectWithName("Play");
            gameObjectCredits = SceneManager.ActiveScene.FindGameObjectWithName("Credits_Text");
            gameObjectExit = SceneManager.ActiveScene.FindGameObjectWithName("ExitTxt");
        }

        public override void Update(GameTime gameTime)
        {
            if (gameObjectPlay.GetComponent<Button>().ClickedOnButton())
            {
                SceneManager.LoadScene(new Scene("MainScene", 0));
                return;
            }

            if (gameObjectCredits.GetComponent<Button>().ClickedOnButton())
            {
                SceneManager.LoadScene(new Scene("Credits", 2));
                return;
            }

            if (gameObjectExit.GetComponent<Button>().ClickedOnButton())
            {
                Setup.Game.Exit();
                return;
            }
        }

    }
}
