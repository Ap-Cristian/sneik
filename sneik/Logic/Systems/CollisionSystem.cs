using Logic.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Systems
{
    internal class CollisionSystem
    {
        // ECS based collision system
        // check this out --> https://en.wikipedia.org/wiki/Entity_component_system
        // and this https://chat.openai.com/share/e2c763f7-942a-4670-ab5f-13020f021bc0

        private List<Collidable> _collidables = null; 
        public CollisionSystem(List<Collidable> collidables)
        {
            _collidables = collidables;
        }

        public void update()
        {
            if(_collidables != null)
            {
                for(int i = 0; i < _collidables.Count - 1; i++)
                {
                    for(int j = i + 1; j < _collidables.Count; j++)
                    {
                        _collidables[i].CheckCollision(_collidables[j]);
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
