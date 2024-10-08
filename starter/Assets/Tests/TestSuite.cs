using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

    public class TestSuite
    {
        private Game game;

        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject =
                Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
            game = gameGameObject.GetComponent<Game>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(game.gameObject);
        }

        [UnityTest]
        public IEnumerator AsteroidsMoveDown()
        {
            GameObject asteroid = game.GetSpawner().SpawnAsteroid();
            float initialYPos = asteroid.transform.position.y;
            yield return new WaitForSeconds(0.1f);
            Assert.Less(asteroid.transform.position.y, initialYPos);
        }

        [UnityTest]
        public IEnumerator GameOverOccursOnAsteroidCollision()
        {
            GameObject asteroid = game.GetSpawner().SpawnAsteroid();
            asteroid.transform.position = game.GetShip().transform.position;
            yield return new WaitForSeconds(0.1f);
            Assert.True(game.isGameOver);
        }

    //1
    [Test]
    public void NewGameRestartsGame()
    {
        //2
        game.isGameOver = true;
        game.NewGame();
        //3
        Assert.False(game.isGameOver);
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
    }

    [UnityTest]

    public IEnumerator PlayerMovesLeft()
    {
        float initialXPos = game.GetShip().transform.position.x;
        // 1
        game.GetShip().MoveLeft();
        // 2
        
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Less(game.GetShip().transform.position.x, initialXPos);
    }
    [UnityTest]
    public IEnumerator PlayerMovesRight()
    {
        float initialXPos = game.GetShip().transform.position.x;
        // 1
        game.GetShip().MoveRight();
        // 2

        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(game.GetShip().transform.position.x, initialXPos);
    }

    [Test]
    public void NewGameSetsScoreToZero()
    {
        //2
        game.isGameOver = true;
        game.NewGame();
        //3
        Assert.AreEqual(game.score, 0);
    }



    [UnityTest]
    public IEnumerator RotateLeftTest()
    {
        //Should start as 0
        float starterZRotation = game.GetShip().transform.rotation.z;

        game.GetShip().rotateLeft();


        yield return new WaitForSeconds(0.1f);

        Assert.Equals(game.GetShip().transform.rotation.y, starterZRotation - 15);
    }

    [UnityTest]
    public IEnumerator RotateRightTest()
    {
        //Should start as 0
        float starterZRotation = game.GetShip().transform.rotation.z;

        game.GetShip().rotateRight();


        yield return new WaitForSeconds(0.1f);

        Assert.Equals(game.GetShip().transform.rotation.y, starterZRotation + 15);
    }
}
