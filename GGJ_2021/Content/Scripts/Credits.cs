using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine;

namespace GGJ_2021
{
    public class Credits : GameObjectComponent
    {
        public override void Update(GameTime gameTime)
        {
            if (Input.GetKey(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                SceneManager.LoadScene(new Scene("MainMenu", 1));
            }
        }
    }
}
