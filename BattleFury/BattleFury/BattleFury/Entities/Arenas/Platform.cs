using BattleFury.EntitySystem;
using BattleFury.Components;
using Microsoft.Xna.Framework;
using BEPUphysics.Entities.Prefabs;
using BattleFury.Components.Animated;
using Microsoft.Xna.Framework.Graphics;

namespace BattleFury.Entities.Arenas
{
    public class Platform : Entity
    {
        private const float DEFAULT_WIDTH = 5.0f;

        private const float DEFAULT_HEIGHT = 0.5f;

        private const float DEFAULT_LENGTH = 3.0f;

        private float width;

        private float height;

        private float length;

        protected BepuPhysicsComponent bepuPhysicsComponent;

        public Platform(Vector3 position, Model model):this(position, DEFAULT_WIDTH, DEFAULT_HEIGHT, DEFAULT_LENGTH, model){
        }

        public Platform(Vector3 position, float width, float height, float length, Model model)
        {
            this.width = width;
            this.height = height;
            this.length = length;

            // Create the physics Component.
            this.bepuPhysicsComponent = new BepuPhysicsComponent(this, new Box(position, width, height, length));
            
            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(width, height, length);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, model, scaling);

            this.AttachComponent(bepuPhysicsComponent);
            this.AttachComponent(drawComponent);
        }

        public Box GetBox()
        {
            return bepuPhysicsComponent.Box;
        }
    }
}
