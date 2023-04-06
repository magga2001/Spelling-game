using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerNarrativeSystem : MonoBehaviour, IObserver<(PlayerAction action, PlayerAnswerData answer)>
{
    [SerializeField] private List<Subject<(PlayerAction action, PlayerAnswerData answer)>> spellingGames;
    [SerializeField] private RewardSystem rewardSystem;
    [SerializeField] private PunishmentSystem punishmentSystem;
    [SerializeField] private PerformanceTracker performanceTracker;
    [SerializeField] private Weapon weapon;
    [SerializeField] private NotificationManager notificationManager;
    [SerializeField] private UpdateUI updateUI;
    private void Start()
    {
        spellingGames.ForEach((game) => game.AddObserver(this));
    }
    public void OnNotify((PlayerAction action, PlayerAnswerData answer) data)
    {
        switch(data.action)
        {
            case PlayerAction.SPELLED_CORRECT:
                Reward(data.answer);
                break;
            case PlayerAction.SPELLED_WRONG:
                Punish(data.answer);
                break;
        }
    }

    private void Reward(PlayerAnswerData answer)
    {
        weapon.Fire();
        rewardSystem.CalculateReward(answer.PlayerAnswer);
        performanceTracker.AddCorrectWord(answer.PlayerAnswer);
        performanceTracker.AddCurrentSessionCorrectWord(answer.PlayerAnswer);
        updateUI.UpdateScore();
        AudioManager.instance.Play("Correct");
        if(answer.SpellingGames == SpellingGames.WORDSEARCH)
        {
            notificationManager.DisplayVocabularyResult(NotificationText.CORRECT);
        }
        else
        {
            notificationManager.DisplayVocabularyResult(NotificationText.CORRECT, answer.CorrectAnswer);
        }
    }

    private void Punish(PlayerAnswerData answer)
    {
        punishmentSystem.CalculatePunishment(answer.PlayerAnswer);
        performanceTracker.AddIncorrectWord(answer.CorrectAnswer);
        performanceTracker.AddCurrentSessionIncorrectWord(answer.CorrectAnswer); 
        updateUI.UpdateScore();
        AudioManager.instance.Play("Incorrect");
        if (answer.SpellingGames == SpellingGames.WORDSEARCH)
        {
            notificationManager.DisplayVocabularyResult(NotificationText.INCORRECT);
        }
        else
        {
            notificationManager.DisplayVocabularyResult(NotificationText.INCORRECT, answer.CorrectAnswer);
        }
    }
}
