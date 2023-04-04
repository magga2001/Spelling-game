using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNarrativeSystem : MonoBehaviour, IObserver<(GameEvent gameEvent, EnemyData enemy)>
{
    [SerializeField] private Enemy enemy;
    private RewardSystem rewardSystem;
    private UpdateUI updateUI;

    private void OnEnable()
    {
        rewardSystem = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RewardSystem>();
        updateUI = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpdateUI>();
        enemy.RemoveObserver(this);
        enemy.AddObserver(this);
    }

    public void OnNotify((GameEvent gameEvent, EnemyData enemy) data)
    {
        switch (data.gameEvent)
        {
            case GameEvent.ENEMY_DIE:
                Reward(data.enemy);
                break;
            default:
                break;

        }
    }
    private void Reward(EnemyData enemy)
    {
        rewardSystem.CalculateReward(enemy.Reward);
        updateUI.UpdateScore();
        enemy.GameObject.SetActive(false);  
    }
}
