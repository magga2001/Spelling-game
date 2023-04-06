using NUnit.Framework;
using UnityEngine;

public class StrikeSystemTest
{
    private StrikeSystem strikeSystem;
    private VocabularyManager vocabularyManager;
    private SpellingDifficultiesManager spellingDifficultiesManager;

    [SetUp]
    public void SetUp()
    {
        strikeSystem = ScriptableObject.CreateInstance<StrikeSystem>();
        vocabularyManager = ScriptableObject.CreateInstance<VocabularyManager>();
        spellingDifficultiesManager = ScriptableObject.CreateInstance<SpellingDifficultiesManager>();

        strikeSystem.GetType().GetField("vocabularyManager", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(strikeSystem, vocabularyManager);
        strikeSystem.GetType().GetField("spellingDifficultiesManager", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(strikeSystem, spellingDifficultiesManager);

        strikeSystem.SetUp();
    }

    [Test]
    public void TestStrikeSystemIncreasesStrike()
    {
        strikeSystem.IncreaseStrike();
        Assert.AreEqual(1, strikeSystem.Strike);
    }

    [Test]
    public void TestStrikeSystemResetsStrikeAndDemotesDifficulty()
    {
        strikeSystem.GetType().GetField("maxStrike", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(strikeSystem, 3);
        spellingDifficultiesManager.SetUp(Difficulties.MEDIUM);
        Difficulties initialDifficulty = spellingDifficultiesManager.Difficulties;

        for (int i = 0; i < 3; i++)
        {
            strikeSystem.IncreaseStrike();
        }

        Assert.AreEqual(0, strikeSystem.Strike);
        Assert.AreEqual(initialDifficulty - 1, spellingDifficultiesManager.Difficulties);
    }
}
