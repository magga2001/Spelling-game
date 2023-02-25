using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private bool gameIsOver;
    private bool combat;
    private bool travelling;
    private bool gameStart;

    public bool GameIsOver { get { return gameIsOver; } set { gameIsOver = value; } }

    [System.Serializable]
    public class Combat
    {
        [SerializeField] private Transform playerLocation;
        [SerializeField] private GameObject enemy;
        public Transform PlayerLocation { get { return playerLocation; } set { playerLocation = value; } }
        public GameObject Enemy { get { return enemy; } set { enemy = value; } }

    }

    [SerializeField] private Player player;
    [SerializeField] private PlayerMotion playerMotion;
    [SerializeField] private CameraSystem cameraSystem;
    [SerializeField] private List<Combat> combats;
    [SerializeField] private SpellingGameManager spellingGameManager;
    [SerializeField] private Button startButton;

    private int currentCombat = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;
        combat = false;
        travelling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Lives <= 0)
        {
            GameIsOver = true;
        }

        if(CombatSystem.Instance.CheckIsCombatSession())
        {
            CombatSystem.Instance.EndCombat();
        }

        if(!combat && travelling)
        {
            playerMotion.NextLocation(combats[currentCombat].PlayerLocation);
        }

        if(player.transform.position == combats[currentCombat].PlayerLocation.position)  
        {
            // So the player can stop at desire location and enter combat mode
            combat = true;
            travelling = false;
            spellingGameManager.GenerateRandomSpellingGame();
            cameraSystem.AdjustFightingCamera(player.transform, combats[currentCombat].Enemy.transform);
            CombatSystem.Instance.SetUpCombat(combats[currentCombat].Enemy);
        }   
    }

    public void ContinueToNextOpponent()
    {
        if(combat && !travelling)
        {
            combat = false;
            StartCoroutine(MovingToNextOpponent());
        }
    }

    IEnumerator MovingToNextOpponent()
    {
        yield return new WaitForSeconds(3);

        currentCombat++;
        if (currentCombat >= combats.Count)
        {
            //Fade in fade out and restart...
            Debug.Log("Reached the end");
        }
        else
        {
            cameraSystem.AdjustWalkingCamera(player.transform);
            spellingGameManager.ResetScreen();
            travelling = true;
        }
    }

    public void StartButton()
    {
        travelling = true;
        gameStart = true;
        startButton.gameObject.SetActive(false);
    }
}
