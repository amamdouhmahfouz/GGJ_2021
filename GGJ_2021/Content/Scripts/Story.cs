using Microsoft.Xna.Framework;
using MyEngine;

namespace GGJ_2021
{
    public class Story : GameObjectComponent
    {
        private bool Done = false;
        private bool Done2 = false;
        private bool Done3 = false;

        private bool GO2 = false;
        private bool GO3 = false;

        public override void Update(GameTime gameTime)
        {
            if (!Done)
            {
                GameObject GO = SceneManager.ActiveScene.FindGameObjectWithName("HatemScene1");
                GO.Active = true;
                SceneManager.ActiveScene.FindGameObjectWithName("Manuscript1").Active = true;
                SceneManager.ActiveScene.FindGameObjectWithName("CommandTxt").Active = false;
                SceneManager.ActiveScene.FindGameObjectWithName("F1_help").Active = false;
                SceneManager.ActiveScene.FindGameObjectWithName("Screen").Active = false;

                GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle = new Rectangle(GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Width, 0, GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Width, GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Height);
                GO.GetComponent<AudioSource>().LoadSoundEffect("VL2");
                GO.GetComponent<AudioSource>().Play();
                Done = true;

                Threader.Invoke(ContinueGame, (uint)(GO.GetComponent<AudioSource>().ClipLength() + 1) * 1000);
            }

            if (!Done2 && GO2)
            {
                GameObject GO = SceneManager.ActiveScene.FindGameObjectWithName("HatemScene1");
                GO.Active = true;
                SceneManager.ActiveScene.FindGameObjectWithName("Manuscript1").Active = true;
                SceneManager.ActiveScene.FindGameObjectWithName("CommandTxt").Active = false;
                SceneManager.ActiveScene.FindGameObjectWithName("F1_help").Active = false;
                SceneManager.ActiveScene.FindGameObjectWithName("Screen").Active = false;

                GO.GetComponent<SpriteRenderer>().Sprite.Transform = GO.Transform;
                GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle = new Rectangle(GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Width * 2, 0, GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Width, GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Height);
                //GO.GetComponent<AudioSource>().LoadSoundEffect("VL3");
                GO.GetComponent<AudioSource>().LoadSoundEffect("VL2");
                GO.GetComponent<AudioSource>().Play();
                Done2 = true;
                GO.GetComponent<Animator>().Enabled = true;
                GO.GetComponent<Animator>().AnimationClips.Find(item => item.Tag == "PissedOff").OrigSpriteSourceValues = GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Location;

                GO.GetComponent<Animator>().PlayWithTag("PissedOff");

                Threader.Invoke(ContinueGame, (uint)(GO.GetComponent<AudioSource>().ClipLength() + 1) * 1000);
            }

            if (!Done3 && GO3)
            {
                GameObject GO = SceneManager.ActiveScene.FindGameObjectWithName("HatemScene1");
                GO.Active = true;
                SceneManager.ActiveScene.FindGameObjectWithName("Manuscript1").Active = true;
                SceneManager.ActiveScene.FindGameObjectWithName("CommandTxt").Active = false;
                SceneManager.ActiveScene.FindGameObjectWithName("F1_help").Active = false;
                SceneManager.ActiveScene.FindGameObjectWithName("Screen").Active = false;

                GO.GetComponent<Animator>().Enabled = false;
                GO.GetComponent<SpriteRenderer>().Sprite.LoadTexture("SubmittingGame");
                GO.GetComponent<AudioSource>().LoadSoundEffect("VL4");
                GO.GetComponent<AudioSource>().Play();
                Done3 = true;

                Threader.Invoke(CutScene4, (uint)(GO.GetComponent<AudioSource>().ClipLength() + 1) * 1000);
            }
        }

        private void ContinueGame()
        {
            if (GO2)
                GO3 = true;

            SceneManager.ActiveScene.FindGameObjectWithName("Screen").Active = true;
            SceneManager.ActiveScene.FindGameObjectWithName("CommandTxt").Active = true;
            SceneManager.ActiveScene.FindGameObjectWithName("F1_help").Active = true;
            if (SceneManager.ActiveScene.FindGameObjectWithName("AllPopups") != null)
                SceneManager.ActiveScene.FindGameObjectWithName("AllPopups").Active = true;

            SceneManager.ActiveScene.FindGameObjectWithName("HatemScene1").Active = false;
            SceneManager.ActiveScene.FindGameObjectWithName("Manuscript1").Active = false;

            SceneManager.ActiveScene.FindGameObjectWithName("STORY").Active = false;
            GO2 = true;
        }

        private void CutScene4()
        {
            GameObject GO = SceneManager.ActiveScene.FindGameObjectWithName("HatemScene1");
            GO.GetComponent<SpriteRenderer>().Sprite.LoadTexture("SeeingReviews");

            Threader.Invoke(CutScene5, 4000);
        }

        private void CutScene5()
        {
            GameObject GO = SceneManager.ActiveScene.FindGameObjectWithName("HatemScene1");
            SceneManager.ActiveScene.FindGameObjectWithName("Manuscript1").Active = false;
            GO.Transform.Scale = 0.95f * Vector2.One; 
            GO.GetComponent<SpriteRenderer>().Sprite.LoadTexture("SteamPage");

            GO.GetComponent<Animator>().Enabled = true;
            GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Size = new Point(GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Width/2, GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Height);


            Animation AM = new Animation(GO.GetComponent<SpriteRenderer>().Sprite, 2);
            AM.Tag = "STEAM";
            AM.IsLooping = false;
            AM.Speed = 3;
            AM.OrigSpriteSourceValues = Point.Zero;
            GO.GetComponent<Animator>().AnimationClips.Add(AM);
            GO.GetComponent<Animator>().PlayWithTag("STEAM");

            

            Threader.Invoke(CutScene6, 6500);
        }

        private void CutScene6()
        {
            GameObject GO = SceneManager.ActiveScene.FindGameObjectWithName("HatemScene1");
            GO.Transform.Scale = 1.5f * Vector2.One;
            SceneManager.ActiveScene.FindGameObjectWithName("Manuscript1").Active = true;
            GO.GetComponent<SpriteRenderer>().Sprite.LoadTexture("Hatem_Celebrating-Sheet (1)");

            

            GO.GetComponent<Animator>().Enabled = true;
            GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Size = new Point(GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Width / 5, GO.GetComponent<SpriteRenderer>().Sprite.SourceRectangle.Height);

            Animation AM = new Animation(GO.GetComponent<SpriteRenderer>().Sprite, 5);
            AM.Tag = "Surprise";
            AM.IsLooping = false;
            AM.Speed = 1;
            AM.OrigSpriteSourceValues = Point.Zero;
            GO.GetComponent<Animator>().AnimationClips.Add(AM);
            GO.GetComponent<Animator>().PlayWithTag("Surprise");

            Threader.Invoke(CutScene7, 5000);
        }

        private void CutScene7()
        {
            GameObject GO = SceneManager.ActiveScene.FindGameObjectWithName("HatemScene1");
            //GO.GetComponent<AudioSource>().LoadSoundEffect("VL5");
            GO.GetComponent<AudioSource>().LoadSoundEffect("VL2");

            GO.GetComponent<AudioSource>().Play();

            Threader.Invoke(CutScene8, (uint)(GO.GetComponent<AudioSource>().ClipLength() + 1) * 1000);
        }

        private void CutScene8()
        {
            SceneManager.ActiveScene.FindGameObjectWithName("Finisher").Active = true;

            SceneManager.ActiveScene.FindGameObjectWithName("HatemScene1").Active = false;
            SceneManager.ActiveScene.FindGameObjectWithName("Manuscript1").Active = false;

            Threader.Invoke(InvokeCredits, 12000);

        }

        private void InvokeCredits()
        {
            SceneManager.LoadScene(new Scene("Credits", 2));
        }

    }
}
