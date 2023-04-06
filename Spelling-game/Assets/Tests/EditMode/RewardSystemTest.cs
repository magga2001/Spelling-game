using NUnit.Framework;
using UnityEngine;

public class RewardSystemTest
{
    private RewardSystem rewardSystem;
    private ScoreSystem scoreSystem;
    private GameObject rewardSystemGameObject;

    [SetUp]
    public void SetUp()
    {
        // Create a RewardSystem game object and add the RewardSystem component
        rewardSystemGameObject = new GameObject();
        rewardSystem = rewardSystemGameObject.AddComponent<RewardSystem>();

        // Create a ScoreSystem ScriptableObject
        scoreSystem = ScriptableObject.CreateInstance<ScoreSystem>();
        rewardSystem.GetType().GetField("scoreSystem", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(rewardSystem, scoreSystem);
    }

    [Test]
    public void RewardSystem_CalculateReward_WordLength()
    {
        string word = "example";
        int expectedScore = word.Length;

        rewardSystem.CalculateReward(word);

        Assert.AreEqual(expectedScore * scoreSystem.GetScale(), scoreSystem.GetScore(), "Reward should be equal to the word length.");
    }

    [Test]
    public void RewardSystem_CalculateReward_IntValue()
    {
        int reward = 10;
        int expectedScore = reward;

        rewardSystem.CalculateReward(reward);

        Assert.AreEqual(expectedScore * scoreSystem.GetScale(), scoreSystem.GetScore(), "Reward should be equal to the input value.");
    }
}
