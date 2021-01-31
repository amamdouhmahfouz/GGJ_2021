using Microsoft.Xna.Framework;
using MyEngine;
using System;

namespace GGJ_2021
{
    public static class Errors
    {
        public static void WindowSpam()
        {
            GameObject Popup = new GameObject();
            Popup.Tag = "PopUp";
            Popup.AddComponent<Transform>(new Transform());
            Popup.AddComponent<SpriteRenderer>(new SpriteRenderer());
            Popup.AddComponent<AudioSource>(new AudioSource("Popup_Sound"));
            Popup.AddComponent<BoxCollider2D>(new BoxCollider2D());
            Popup.GetComponent<SpriteRenderer>().Sprite = new Sprite(Popup.Transform);
            Popup.GetComponent<SpriteRenderer>().Sprite.LoadTexture("PopUpWindow");
            Popup.Layer = 0;

            Popup.Start();

            Popup.Transform.Position = new Vector2(Setup.graphics.PreferredBackBufferWidth / 2, Setup.graphics.PreferredBackBufferHeight / 2);
            Popup.GetComponent<AudioSource>().Play();
            Popup.GetComponent<BoxCollider2D>().Bounds = new Rectangle(Popup.GetComponent<BoxCollider2D>().Bounds.Width - 22, 0, 22, 22);

            GameObject AllPopups = new GameObject();
            AllPopups.AddComponent<Transform>(new Transform());
            AllPopups.Name = "AllPopups";
            AllPopups.AddChild(Popup);

            SceneManager.ActiveScene.AddGameObject(Popup);
            SceneManager.ActiveScene.AddGameObject(AllPopups);

            for(int i=1; i < 25; i++)
            {
                Threader.Invoke(MakeAnotherWindowSpam, (uint)i * 100);
            }
        }

        private static void MakeAnotherWindowSpam()
        {
            Random RandomInstance = new Random();

            GameObject Instance = GameObject.Instantiate(SceneManager.ActiveScene.FindGameObjectWithTag("PopUp"));
            Vector2 NewPosition = new Vector2(Setup.graphics.PreferredBackBufferWidth * MathCompanion.Clamp((float)RandomInstance.NextDouble(), 0.05f, 0.8f), Setup.graphics.PreferredBackBufferHeight * MathCompanion.Clamp((float)RandomInstance.NextDouble(), 0.08f, 0.8f));
            Instance.Transform.Position = NewPosition;
            Instance.Tag = "PopUp";
            Instance.GetComponent<AudioSource>().Play();
        }

        public static int Restart(int NumberOfCommands)
        {
            if (NumberOfCommands > 10)
                return 5;

            return 0;
        }
    }
}
