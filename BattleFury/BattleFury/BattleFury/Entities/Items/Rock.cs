using BattleFury.Components;
using BattleFury.Components.Animated;
using BattleFury.Components.Movement;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using BattleFury.EntitySystem;

namespace BattleFury.Entities.Items
{
    public class Rock : Item
    {
        private const int MASS = 1;

        public Rock(Vector3 spawnPosition, Environment environment)
            : base(new Box(spawnPosition, 1, 1, 1, MASS))
        {

            //SelfDestructOnImpactComponent selfDestructComponent = new SelfDestructOnImpactComponent(this, environment, false);
            //this.AttachComponent(selfDestructComponent);

            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(1, 1, 1);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, scaling);
            this.AttachComponent(drawComponent);

            GrabbableComponent grabbable = new GrabbableComponent(this);
            this.AttachComponent(grabbable);
            
        }


    }

}
