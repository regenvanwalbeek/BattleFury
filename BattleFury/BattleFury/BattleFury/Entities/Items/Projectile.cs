using BattleFury.Components.Animated;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;

namespace BattleFury.Entities.Items
{
    public class Projectile : Item
    {




        public Projectile(Vector3 spawnPosition, Vector3 velocity)
            : base(new Box(spawnPosition, 1, 1, 1))
        {
            this.bepuPhysicsComponent.Box.LinearVelocity = velocity;

            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(1, 1, 1);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, scaling);
            this.AttachComponent(drawComponent);
        }

    }
}
