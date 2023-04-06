using NUnit.Framework;
using UnityEngine;


public class PunishmentSystemTest
{
    private PunishmentSystem punishmentSystem;
    private ScoreSystem scoreSystem;
    private StrikeSystem strikeSystem;
    private GameObject punishmentSystemGameObject;

    [SetUp]
    public void SetUp()
    {
        // Create a PunishmentSystem game object and add the PunishmentSystem component
        punishmentSystemGameObject = new GameObject();
        punishmentSystem = punishmentSystemGameObject.AddComponent<PunishmentSystem>();

        // Create ScoreSystem and StrikeSystem ScriptableObjects
        scoreSystem = ScriptableObject.CreateInstance<ScoreSystem>();
        strikeSystem = ScriptableObject.CreateInstance<StrikeSystem>();

        // Set the ScoreSystem and StrikeSystem fields in PunishmentSystem
        punishmentSystem.GetType().GetField("scoreSystem", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(punishmentSystem, scoreSystem);
        punishmentSystem.GetType().GetField("strikeSystem", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(punishmentSystem, strikeSystem);
    }

    [Test]
    public void TestPunishmentSystemDecreasesScore()
    {
        // Set initial score to 50
        scoreSystem.Score = 50;

        // Test with a 5-character word
        punishmentSystem.CalculatePunishment("apple");

        // Check if the score has been decreased by the word length (5)
        Assert.AreEqual(0, scoreSystem.GetScore());
    }

    [Test]
    public void TestPunishmentSystemIncreasesStrike()
    {
        // Set initial strikes to 0
        strikeSystem.Strike = 0;

        // Set GameManager.Instance.IsEndless() to return true
        GameManager.Instance.IsEndless = true;
        // Test with a 5-character word
        punishmentSystem.CalculatePunishment("apple");

        // Check if the strike count has been increased by 1
        Assert.AreEqual(1, strikeSystem.Strike);
    }
}
