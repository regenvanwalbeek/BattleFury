using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.Entities.Hitboxes;
using Microsoft.Xna.Framework;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework.Graphics;
using BattleFury.Components.Animated;

namespace BattleFury.Entities.Items
{
    public class Rock : Item
    {
        private const int MASS = 1;

        public Rock(Vector3 spawnPosition)
            : base(new Box(spawnPosition, 1, 1, 1, MASS))
        {
            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(1, 1, 1);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, scaling);
            this.AttachComponent(drawComponent);
        }


    }
}
