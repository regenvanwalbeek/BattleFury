using BattleFury.EntitySystem;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BattleFury.Entities.Items;

namespace BattleFury.Components.Animated
{
    public class CubeRenderComponent : BasicModelComponent
    {
        public Matrix Transform;

        private BepuPhysicsComponent physicsComponent;

        public CubeRenderComponent(Entity parent, Model model, Matrix transform)
            : base(parent, model)
        {
            this.Transform = transform;
        }

        protected override Matrix GetWorld()
        {
            return Transform * physicsComponent.Box.WorldTransform;
        }

        public override void Start()
        {
            base.Start();
 
            // Get a reference to the transform component
            this.physicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
        }
    }
}
