using UnityEngine;

public class EnemyData
{
    private string name;
    private GameObject gameObject;
    private int reward;

    public EnemyData(string name, GameObject gameObject, int reward)
    {
        this.name = name;
        this.gameObject = gameObject;
        this.reward = reward;
    }   

    public string Name => name;
    public GameObject GameObject => gameObject; 
    public int Reward => reward;    
}
