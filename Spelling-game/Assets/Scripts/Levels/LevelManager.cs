using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        combatting = false;
        travelling = false;
        enemySpawned = false;
    }

    void Update()
    {
        if (combatSystem.CheckIsCombatSession())
        {
            ContinueToNextOpponent();
        }

        //If the scene is not over, travel to next combat position
        if (!isSceneOver)
        {
            //Player keeps moving until reach combat position
            if (!combatting && travelling)
            {
                playerMotion.NextLocation(combats[currentCombat].PlayerLocation);
            }

            //If combat position is reached, combat will be set up
            if (player.transform.position == combats[currentCombat].PlayerLocation.position)
            {
                // So the player can stop at desire location and enter combat mode
                combatting = true;
                travelling = false;
                spellingGameManager.DisplaySpellingGame(); //Display the spelling game chosen 
                cameraSystem.AdjustFightingCamera(player.transform, combats[currentCombat].EnemyLocation);
                player.gameObject.GetComponentInChildren<Animator>().SetBool("Walking", false);
                //Check has the enemy been spawned. The goal is to restrict only one enemy per combat
                if (!enemySpawned)
                {
                    //If it's a boss level, then spawn the boss. Otherwise normal enemy
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
            //Move to portal to teleport to next scene
            if (travelling)
            {
                playerMotion.NextLocation(portal);
            }

            //When player reached portal, start the teleporting animation
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

    //The setting when playing is currently moving to the next location, either next combat or portal
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
            //Disable the spelling game while travelling 
            spellingGameManager.ResetScreen();
            yield return new WaitForSeconds(4);

            //Save the game state before exiting the current scene
            RealTimeSavingManager.Instance.SaveCurrentState();

            //If the game is endless, then dont go to boss
            if(GameManager.Instance.IsEndless)
            {
                if(SceneManager.GetActiveScene().buildIndex == 4)
                {
                    transition.LoadingLevel(SceneName.sceneOne);
                }
                else
                {
                    transition.LoadingLevel(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            //If the game is not endless, then go to boss level for the last level to complete the adventure
            else if(!bossLevel && !GameManager.Instance.IsEndless)
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
            //Continue moving to next combat location
            cameraSystem.AdjustWalkingCamera(player.transform);
            spellingGameManager.ResetScreen();
            travelling = true;
        }
    }

    //This start button only appear at the
    //start of the scene to trigger the scene to start its visual automation
    public void StartButton()
    {
        travelling = true;
        isSceneOver = false;
        startButton.gameObject.SetActive(false);
        player.gameObject.GetComponentInChildren<Animator>().SetBool("Walking", true);

    }
}
