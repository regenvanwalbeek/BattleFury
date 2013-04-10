using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.Components;
using Microsoft.Xna.Framework;
using BattleFury.Components.Animated;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework.Graphics;

namespace BattleFury.Entities.Characters
{
    public class FightingRobot : Character
    {
        

        public FightingRobot(int lives, Vector3 spawnPosition, Model model)
            : base("FightingRobot", lives, new Box(spawnPosition, 1, 1, 1, 1))
        {
            
            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(1, 1, 1);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, model, scaling);

            this.AttachComponent(drawComponent);

            // TODO create specific move components and attach them here.
        }

  
    }
}
