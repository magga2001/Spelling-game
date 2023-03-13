using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    private bool combatting;
    private bool travelling;
    private bool isSceneOver;

    [System.Serializable]
    public class LevelCheckPoint
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
    [SerializeField] private CombatSystem combatSystem;
    [SerializeField] private CameraSystem cameraSystem;
    [SerializeField] private List<LevelCheckPoint> combats;
    [SerializeField] private SpellingGameManager spellingGameManager;
    [SerializeField] private Button startButton;

    [SerializeField] private bool bossLevel;

    private bool enemySpawned;

    private int currentCombat = 0;

    // Start is called before the first frame update
    void Start()
    {
        combatting = false;
        travelling = false;
        enemySpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (combatSystem.CheckIsCombatSession())
        {
            ContinueToNextOpponent();
        }

        if (!isSceneOver)
        {
            if (!combatting && travelling)
            {
                playerMotion.NextLocation(combats[currentCombat].PlayerLocation);
            }

            if (player.transform.position == combats[currentCombat].PlayerLocation.position)
            {
                // So the player can stop at desire location and enter combat mode
                combatting = true;
                travelling = false;
                spellingGameManager.DisplaySpellingGame();
                cameraSystem.AdjustFightingCamera(player.transform, combats[currentCombat].EnemyLocation);
                player.gameObject.GetComponentInChildren<Animator>().SetBool("Walking", false);
                if (!enemySpawned)
                {
                    if(!bossLevel)
                    {
                        combatSystem.SetUpCombat(combats[currentCombat].EnemyLocation);
                        enemySpawned = true;
                    }
                    else
                    {
                        combatSystem.SetUpBossCombat(combats[currentCombat].EnemyLocation);
                        enemySpawned = true;
                    }
                }
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
                player.gameObject.GetComponentInChildren<Animator>().SetBool("Walking", false);
            }
        }
    }

    public void ContinueToNextOpponent()
    {
        if (combatting && !travelling)
        {
            combatting = false;
            enemySpawned = false;
            StartCoroutine(MovingToNextLocation());
        }
    }

    IEnumerator MovingToNextLocation()
    {
        yield return new WaitForSeconds(3);
        player.gameObject.GetComponentInChildren<Animator>().SetBool("Walking", true);

        currentCombat++;
        if (currentCombat >= combats.Count)
        {
            isSceneOver = true;
            travelling = true;
            cameraSystem.AdjustWalkingCamera(player.transform);
            spellingGameManager.ResetScreen();
            yield return new WaitForSeconds(8);
            RealTimeSavingManager.Instance.SaveCurrentState();

            if(!bossLevel)
            {
                transition.LoadingLevel(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                GameManager.Instance.IsLevelComplete = true;
            }

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
        isSceneOver = false;
        startButton.gameObject.SetActive(false);
        player.gameObject.GetComponentInChildren<Animator>().SetBool("Walking", true);

    }
}
