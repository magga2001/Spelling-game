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
        notificationManager.DisplayVocabularyResult(NotificationText.CORRECT);
    }

    private void Punish(PlayerAnswerData answer)
    {
        punishmentSystem.CalculatePunishment(answer.PlayerAnswer);
        performanceTracker.AddIncorrectWord(answer.PlayerAnswer);
        notificationManager.DisplayVocabularyResult(NotificationText.INCORRECT);
    }
}
