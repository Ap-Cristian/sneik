using Logic.Attributes;
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
        private List<Collidable> _collidables = null; 
        private static CollisionSystem _instance;
        public static CollisionSystem Instance 
        { 
            get 
            { if (_instance == null) 
                { 
                    return new CollisionSystem(); 
                }
            return _instance;
            } 
        }
        public void AddCollidables(List<Collidable> collidables)
        {
            if (_collidables != null)
                _collidables.AddRange(collidables);
            else
                _collidables = collidables;
        }
        public void SetCollidables(List<Collidable> collidables)
        {
            _collidables = collidables;
        }
        public void AddCollidable(Collidable collidable)
        {
            if(_collidables != null)
                _collidables.Add(collidable);
            else
                _collidables = new List<Collidable> { collidable};
        }

        public void RemoveCollidable(Collidable collidable)
        {
            if(_collidables != null)
                _collidables.Remove(collidable);
        }

        public void Update()
        {
            if(_collidables != null)
            {
                for(int i = 0; i < _collidables.Count; i++)
                {
                    for(int j = 0; j < _collidables.Count && j != i; j++)
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
