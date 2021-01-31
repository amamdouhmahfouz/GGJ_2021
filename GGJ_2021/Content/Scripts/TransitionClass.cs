using Microsoft.Xna.Framework;
using MyEngine;

namespace GGJ_2021
{
    public class TransitionClass : GameObjectComponent
    {
        private PropertiesAnimator PA;
        private Panel Panel;

        public override void Start()
        {
            PA = gameObject.GetComponent<PropertiesAnimator>();
            Panel = gameObject.GetComponent<Panel>();
        }

        public override void Update(GameTime gameTime)
        {
            PA.GetKeyFrame("FadeIn").GetFeedback(ref Panel.Color);
        }
    }
}
