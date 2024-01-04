using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Logic.Systems;
using Logic.Models;
using System.Xml.Linq;
using Logic.Interfaces;

namespace sneikTest.SystemsTest
{
    [TestClass]
    public class CollisionSystemTest
    {
        List<ICollidable> collidableList = new List<ICollidable>();

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

            collidableList[0].CollisionHandler += onColliderACollided;
            collidableList[1].CollisionHandler += onColliderBCollided;
            collidableList[2].CollisionHandler += onColliderCCollided;

            CollisionSystem collisionSystem = CollisionSystem.Instance;
            collisionSystem.SetCollidables(collidableList);

            this.colliderACollided = false;
            this.colliderBCollided = false;
            this.colliderCCollided = false;

            collisionSystem.Update();

            Assert.AreEqual(true, colliderACollided);
            Assert.AreEqual(true, colliderBCollided);
            //Assert.AreEqual(false, colliderCCollided);
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

            collidableList[0].CollisionHandler += onColliderACollided;
            collidableList[1].CollisionHandler += onColliderBCollided;
            collidableList[2].CollisionHandler += onColliderCCollided;

            CollisionSystem collisionSystem = CollisionSystem.Instance;
            collisionSystem.SetCollidables(collidableList);

            this.colliderACollided = false;
            this.colliderBCollided = false;
            this.colliderCCollided = false;

            collisionSystem.Update();

            Assert.AreEqual(false, colliderACollided);
            Assert.AreEqual(false, colliderBCollided);
            Assert.AreEqual(false, colliderCCollided);
        }
        [TestMethod]
        public void TestNotColliding_RIGHT()
        {

            Obstacle obstacleA = new Obstacle(new Point(20, 20), new Size(6, 6));
            Obstacle obstacleB = new Obstacle(new Point(27, 20), new Size(6, 6));
            Obstacle obstacleC = new Obstacle(new Point(100, 100), new Size(6, 6));

            collidableList.Add(obstacleA);
            collidableList.Add(obstacleB);
            collidableList.Add(obstacleC);

            collidableList[0].CollisionHandler += onColliderACollided;
            collidableList[1].CollisionHandler += onColliderBCollided;
            collidableList[2].CollisionHandler += onColliderCCollided;

            CollisionSystem collisionSystem = CollisionSystem.Instance;
            collisionSystem.SetCollidables(collidableList);

            this.colliderACollided = false;
            this.colliderBCollided = false;
            this.colliderCCollided = false;

            collisionSystem.Update();

            Assert.AreEqual(false, colliderACollided);
            Assert.AreEqual(false, colliderBCollided);
            Assert.AreEqual(false, colliderCCollided);
        }
        [TestMethod]
        public void TestNotColliding_LEFT()
        {

            Obstacle obstacleA = new Obstacle(new Point(20, 20), new Size(6, 6));
            Obstacle obstacleB = new Obstacle(new Point(7, 20), new Size(6, 6));
            Obstacle obstacleC = new Obstacle(new Point(100, 100), new Size(6, 6));

            collidableList.Add(obstacleA);
            collidableList.Add(obstacleB);
            collidableList.Add(obstacleC);

            collidableList[0].CollisionHandler += onColliderACollided;
            collidableList[1].CollisionHandler += onColliderBCollided;
            collidableList[2].CollisionHandler += onColliderCCollided;

            CollisionSystem collisionSystem = CollisionSystem.Instance;
            collisionSystem.SetCollidables(collidableList);

            this.colliderACollided = false;
            this.colliderBCollided = false;
            this.colliderCCollided = false;

            collisionSystem.Update();

            Assert.AreEqual(false, colliderACollided);
            Assert.AreEqual(false, colliderBCollided);
            Assert.AreEqual(false, colliderCCollided);
        }
        [TestMethod]
        public void TestNotColliding_UP()
        {

            Obstacle obstacleA = new Obstacle(new Point(20, 20), new Size(6, 6));
            Obstacle obstacleB = new Obstacle(new Point(20, 7), new Size(6, 6));
            Obstacle obstacleC = new Obstacle(new Point(100, 100), new Size(6, 6));

            collidableList.Add(obstacleA);
            collidableList.Add(obstacleB);
            collidableList.Add(obstacleC);

            collidableList[0].CollisionHandler += onColliderACollided;
            collidableList[1].CollisionHandler += onColliderBCollided;
            collidableList[2].CollisionHandler += onColliderCCollided;

            CollisionSystem collisionSystem = CollisionSystem.Instance;
            collisionSystem.SetCollidables(collidableList);

            this.colliderACollided = false;
            this.colliderBCollided = false;
            this.colliderCCollided = false;

            collisionSystem.Update();

            Assert.AreEqual(false, colliderACollided);
            Assert.AreEqual(false, colliderBCollided);
            Assert.AreEqual(false, colliderCCollided);
        }
        [TestMethod]
        public void TestNotColliding_DOWN()
        {

            Obstacle obstacleA = new Obstacle(new Point(20, 20), new Size(6, 6));
            Obstacle obstacleB = new Obstacle(new Point(20, 27), new Size(6, 6));
            Obstacle obstacleC = new Obstacle(new Point(100, 100), new Size(6, 6));

            collidableList.Add(obstacleA);
            collidableList.Add(obstacleB);
            collidableList.Add(obstacleC);

            collidableList[0].CollisionHandler += onColliderACollided;
            collidableList[1].CollisionHandler += onColliderBCollided;
            collidableList[2].CollisionHandler += onColliderCCollided;

            CollisionSystem collisionSystem = CollisionSystem.Instance;
            collisionSystem.SetCollidables(collidableList);

            this.colliderACollided = false;
            this.colliderBCollided = false;
            this.colliderCCollided = false;

            collisionSystem.Update();

            Assert.AreEqual(false, colliderACollided);
            Assert.AreEqual(false, colliderBCollided);
            Assert.AreEqual(false, colliderCCollided);
        }
    }
}
