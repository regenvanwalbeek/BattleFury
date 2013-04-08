﻿using System;
using BEPUphysics;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using BattleFury.Entities;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleFury.PhysicsEngine
{
    // TODO should this extend Entity? Probably could...

    public class Physics
    {
        private Space space;
        private Model unitcube;

        BepuPhysicsEntity ground;

        BepuPhysicsEntity stuff1;

        BepuPhysicsEntity stuff2;

        BepuPhysicsEntity stuff3;

        public Physics()
        {
            space = new Space();

           
        }

        private Model cube2;

        public void LoadContent(ContentManager content)
        {
            unitcube = content.Load<Model>("meshes/cube");

            // This would probably be good for initialize in an entity
            ground = new BepuPhysicsEntity(new Box(Vector3.Zero, 30, 1, 30), unitcube);

            stuff1 = new BepuPhysicsEntity(new Box(new Vector3(0, 4, 0), 1, 1, 1, 1), unitcube);
            stuff2 = new BepuPhysicsEntity(new Box(new Vector3(0, 8, 0), 1, 1, 1, 1), unitcube);
            stuff3 = new BepuPhysicsEntity(new Box(new Vector3(0, 12, 0), 1, 1, 1, 1), unitcube);

            space.Add(ground.GetBox());
            space.Add(stuff1.GetBox());
            space.Add(stuff2.GetBox());
            space.Add(stuff3.GetBox());

            ground.Initialize();
            stuff1.Initialize();
            stuff2.Initialize();
            stuff3.Initialize();

            space.ForceUpdater.Gravity = new Vector3(0, -9.81f, 0);
            cube2 = content.Load<Model>("meshes/unitcube");

        }

        public void Update()
        {
            space.Update();
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            ground.Draw(gameTime, view, projection);
            stuff1.Draw(gameTime, view, projection);
            stuff2.Draw(gameTime, view, projection);
            stuff3.Draw(gameTime, view, projection);
        }

        public void Add(ISpaceObject spaceObject)
        {
            space.Add(spaceObject);
        }
    }
}
