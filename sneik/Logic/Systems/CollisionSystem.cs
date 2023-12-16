using Logic.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Systems
{
    public class CollisionSystem
    {
        // ECS based collision system
        // check this out --> https://en.wikipedia.org/wiki/Entity_component_system
        // and this https://chat.openai.com/share/e2c763f7-942a-4670-ab5f-13020f021bc0
        //
        // If you wish to create a collidable, first derive it from Collidable and then add it to the _collidables list, the system "should" handle the rest on Update:)

        private List<Collidable> _collidables = null; 
        public CollisionSystem(List<Collidable> collidables)
        {
            _collidables = collidables;
        }

        public void AddCollidable(Collidable collidable)
        {
            _collidables.Add(collidable);
        }

        public void RemoveCollidable(Collidable collidable)
        {
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
