using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerPlayModeTests
{
    private GameObject playerGameObject;
    private Player player;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load a test scene or create necessary objects in the scene
        // If you have a test scene, you can load it with SceneManager.LoadSceneAsync()
        // Otherwise, create a player object in the scene
        playerGameObject = new GameObject();
        player = playerGameObject.AddComponent<Player>();

        yield return null;
    }

    [UnityTest]
    public IEnumerator Player_Health_Increases()
    {
        //player.SetUpPlayer();
        int initialHealth = player.CurrentHealth;
        int healthIncrease = 10;

        player.IncreaseHealth(healthIncrease);

        yield return null;

        Assert.AreEqual(initialHealth + healthIncrease, player.CurrentHealth, "Player's health should increase by the health increase amount.");
    }

    [UnityTest]
    public IEnumerator Player_Dies()
    {
        //player.SetUpPlayer();
        int initialLives = player.Lives;

        player.TakeDamage(player.MaxHealth);

        yield return null;

        Assert.AreEqual(initialLives - 1, player.Lives, "Player's lives should decrease by 1 after dying.");
    }

    // Add more tests as needed
}
