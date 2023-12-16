using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Attributes;
using Logic.Systems;
using Logic.Models;
using System.Xml.Linq;
namespace sneikTest.SystemsTest
{
    [TestClass]
    public class CollisionSystemTest
    {
        List<Collidable> collidableList = new List<Collidable>();

        bool colliderACollided = false;
        bool colliderBCollided = false;
        bool colliderCCollided = false;
        

        private void onColliderACollided(object sender, EventArgs args)
        {
            colliderACollided = true;
        }
        private void onColliderBCollided(object sender, EventArgs args)
        {
            colliderBCollided = true;
        }
        private void onColliderCCollided(object sender, EventArgs args)
        {
            colliderCCollided = true;
        }


        [TestMethod]
        public void TestColliding()
        {
            Obstacle obstacleA = new Obstacle(new Point(1, 10), new Size(6, 6));
            Obstacle obstacleB = new Obstacle(new Point(4, 8), new Size(6, 6));
            Obstacle obstacleC = new Obstacle(new Point(100, 100), new Size(6, 6));
            
            collidableList.Add(obstacleA);
            collidableList.Add(obstacleB);
            collidableList.Add(obstacleC);

            collidableList[0].CollisionDelegate += onColliderACollided;
            collidableList[1].CollisionDelegate += onColliderBCollided;
            collidableList[2].CollisionDelegate += onColliderCCollided;

            CollisionSystem collisionSystem = new(collidableList);

            collisionSystem.Update();

            Assert.AreEqual(true, colliderACollided);
            Assert.AreEqual(true, colliderBCollided);
            Assert.AreEqual(false, colliderCCollided);
        }

        [TestMethod]
        public void TestNotColliding()
        {

            Obstacle obstacleA = new Obstacle(new Point(0, 0), new Size(6, 6));
            Obstacle obstacleB = new Obstacle(new Point(10, 10), new Size(6, 6));
            Obstacle obstacleC = new Obstacle(new Point(100, 100), new Size(6, 6));

            collidableList.Add(obstacleA);
            collidableList.Add(obstacleB);
            collidableList.Add(obstacleC);

            collidableList[0].CollisionDelegate += onColliderACollided;
            collidableList[1].CollisionDelegate += onColliderBCollided;
            collidableList[2].CollisionDelegate += onColliderCCollided;

            CollisionSystem collisionSystem = new(collidableList);

            collisionSystem.Update();

            Assert.AreEqual(false, colliderACollided);
            Assert.AreEqual(false, colliderBCollided);
            Assert.AreEqual(false, colliderCCollided);
        }

        private void CollisionSystemTest_CollisionDelegate(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
