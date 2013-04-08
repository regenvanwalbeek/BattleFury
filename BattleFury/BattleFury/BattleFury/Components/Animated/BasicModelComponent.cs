using BattleFury.Components.CameraComponents;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BattleFury.Entities;

namespace BattleFury.Components.Animated
{
    public class BasicModelComponent : DrawableComponent
    {
        private Model model;

        private ViewProjectionComponent viewProjectionComponent;

        private EntityProvider<Camera> camera;

        public BasicModelComponent(Entity parent, Model model, Camera camera)
            : base(parent, "BasicModelComponent")
        {
            this.model = model;
            this.camera = new EntityProvider<Camera>(camera);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.Projection = viewProjectionComponent.Projection;
                    effect.View = viewProjectionComponent.View;
                    effect.World = GetWorld() * mesh.ParentBone.Transform;
                }
                mesh.Draw();
            }
        }

        public override void Start()
        {
            // Gather reference to Camera Drawing this entity
            viewProjectionComponent = (ViewProjectionComponent) camera.Entity.GetComponent("ViewProjectionComponent");
        }

        public override void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Gets the model's world matrix
        /// </summary>
        /// <returns>The model's world</returns>
        public virtual Matrix GetWorld()
        {
            return Matrix.Identity;
        }


        public override void LoadContent()
        {
        }

        public override void UnloadContent()
        {
        }

        public override void Initialize()
        {
        }
    }
}
