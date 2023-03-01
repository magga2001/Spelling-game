using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private bool gameIsOver;
    private bool combat;
    private bool travelling;
    private bool sceneOver;

    public bool GameIsOver { get { return gameIsOver; } set { gameIsOver = value; } }

    [System.Serializable]
    public class Combat
    {
        [SerializeField] private Transform playerLocation;
        [SerializeField] private Transform enemyLocation;
        
        public Transform PlayerLocation { get { return playerLocation; } set { playerLocation = value; } }
        public Transform EnemyLocation { get { return enemyLocation; } set { enemyLocation = value; } }

    }

    [SerializeField] private Player player;
    [SerializeField] private PlayerMotion playerMotion;
    [SerializeField] private Transform portal;
    [SerializeField] private LevelLoader transition;
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

        if(!sceneOver)
        {
            if (!combat && travelling)
            {
                playerMotion.NextLocation(combats[currentCombat].PlayerLocation);
            }

            if (player.transform.position == combats[currentCombat].PlayerLocation.position)
            {
                // So the player can stop at desire location and enter combat mode
                combat = true;
                travelling = false;
                spellingGameManager.GenerateRandomSpellingGame();
                cameraSystem.AdjustFightingCamera(player.transform, combats[currentCombat].EnemyLocation);
                CombatSystem.Instance.SetUpCombat(combats[currentCombat].EnemyLocation);
            }
        }
        else
        {
            if (travelling)
            {
                playerMotion.NextLocation(portal);
            }

            if (player.transform.position == portal.position)
            {
                travelling = false;
            }

        }
    }

    public void ContinueToNextOpponent()
    {
        if(combat && !travelling)
        {
            combat = false;
            StartCoroutine(MovingToNextLocation());
        }
    }

    IEnumerator MovingToNextLocation()
    {
        yield return new WaitForSeconds(3);

        currentCombat++;
        if (currentCombat >= combats.Count)
        {
            sceneOver = true;
            travelling = true;
            cameraSystem.AdjustWalkingCamera(player.transform);
            spellingGameManager.ResetScreen();
            yield return new WaitForSeconds(8);
            //transition.LoadingLevel(SceneManager.GetActiveScene().buildIndex + 1);
            transition.LoadingLevel(0);

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
        sceneOver = false;
        startButton.gameObject.SetActive(false);
    }
}
