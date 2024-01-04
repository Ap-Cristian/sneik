
using Logic.Interfaces;
using Logic.Models;
using System;
using System.Collections.Generic;

namespace Logic.Systems
{
    public sealed class CollisionSystem
    {
        // ECS based collision system
        // check this out --> https://en.wikipedia.org/wiki/Entity_component_system
        // and this https://chat.openai.com/share/e2c763f7-942a-4670-ab5f-13020f021bc0
        //
        // If you wish to create a collidable, first derive it from Collidable and then add it to the _collidables list, the system "should" handle the rest on update:)

        private CollisionSystem() { }
        private List<ICollidable> _collidables = null;
        private static CollisionSystem _instance;
        public static CollisionSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CollisionSystem();
                }
                return _instance;
            }
        }
        public void AddCollidables(List<ICollidable> collidables)
        {
            if (_collidables != null)
                _collidables.AddRange(collidables);
            else
                _collidables = collidables;
        }
        public void SetCollidables(List<ICollidable> collidables)
        {
            _collidables = collidables;
        }
        public void AddCollidable(ICollidable collidable)
        {
            if (_collidables != null)
                _collidables.Add(collidable);
            else
                _collidables = new List<ICollidable> { collidable };
        }

        public void RemoveCollidable(ICollidable collidable)
        {
            if (_collidables != null)
                _collidables.Remove(collidable);
        }
        public void AddObstacles(ICollidable[,] obstacles)
        {
            if (_collidables == null)
            {
                _collidables = new List<ICollidable>();
            }
            for (int i = 0; i < obstacles.GetLength(0); i++)
            {
                for (int j = 0; j < obstacles.GetLength(1); j++)
                {
                    if (obstacles[i, j] != null)
                    {
                        _collidables.Add(obstacles[i, j]);
                    }
                }
            }
        }

        public void Update()
        {
            if (_collidables != null)
            {
                for (int i = 0; i < _collidables.Count; i++)
                {
                    for (int j = 0; j < _collidables.Count && j != i; j++)
                    {
                        _collidables[i].CheckCollision(_collidables[j]);
                        _collidables[j].CheckCollision(_collidables[i]);
                    }
                }
            }
            else
            {
                throw new Exception("[ERR! COLL_SYS] Collidables list is undefined!");
            }
        }
    }
}
