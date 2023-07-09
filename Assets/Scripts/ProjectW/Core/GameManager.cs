using System;
using ProjectW.NPCs.Baker;
using ProjectW.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingleton<GameManager>
{
    // public PauseMenu PauseMenu;
    private bool _gamePaused;
    private int _activeScene = 0;

    public Action ResetPause;

    private void OnEnable()
    {
        ResetPause += ResetPauseState;
    }

    private void OnDisable()
    {
        ResetPause -= ResetPauseState;
    }
    
    protected override void Awake()
    {
        initilizeMetaLevels();
        base.Awake();
        _activeScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        if (_activeScene == 1)
        {
            WorldGeneration.Instance.BuildWorld();
            //Player.spawn
            //enemyspawner.start
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        
    }

    private void ResetPauseState()
    {
        _gamePaused = false;
    }
    
    private void PauseGame()
    {
        if (!_gamePaused)
        {
            _gamePaused = true;
            // PauseMenu?.Pause();
        }
        else
        {
            _gamePaused = false;
            // PauseMenu?.Continue();
        }
    }

    public void LoadScene(int buildindex)
    {
        SceneManager.LoadScene(buildindex);
        _activeScene = buildindex;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        _activeScene = SceneManager.GetSceneByName(sceneName).buildIndex;
    }

    public void LoadGame()
    {
        LoadScene(1);
    }


    public void LoadMainMenu()
    {
        Debug.Log("in LoadMainMenu");
        LoadScene(0);

        // test if it is first time user starts game
        if (PlayerPrefs.HasKey("PlayedBefore")){
            Debug.Log("Not First Time Playing");
        } else {
            initilizeMetaLevels();
        }
        // remove later, just for testing!!

    }
    public void initilizeMetaLevels(){
        //set all metalevels to zero

        PlayerPrefs.SetInt("DMG", 0);
        PlayerPrefs.SetInt("ATKSpeed", 0);
        PlayerPrefs.SetInt("KnockBack", 0);

        PlayerPrefs.SetInt("HP", 0);
        PlayerPrefs.SetInt("SuckerRadius", 0);
        PlayerPrefs.SetInt("XPGainSpeed", 0);


        // bonus effect per level
        PlayerPrefs.SetInt("DMG_Level1", 15);
        PlayerPrefs.SetInt("ATKSpeed_Level1", 15);
        PlayerPrefs.SetInt("KnockBack_Level1", 15);
        PlayerPrefs.SetInt("HP_Level1", 15);
        PlayerPrefs.SetInt("SuckerRadius_Level1", 15);
        PlayerPrefs.SetInt("XPGainSpeed_Level1", 15); 

        PlayerPrefs.SetInt("DMG_Level2", 10);
        PlayerPrefs.SetInt("ATKSpeed_Level2", 10);
        PlayerPrefs.SetInt("KnockBack_Level2", 10);
        PlayerPrefs.SetInt("HP_Level2", 10);
        PlayerPrefs.SetInt("SuckerRadius_Level2", 10);
        PlayerPrefs.SetInt("XPGainSpeed_Level2", 10); 

        PlayerPrefs.SetInt("DMG_Level3", 5);
        PlayerPrefs.SetInt("ATKSpeed_Level3", 5);
        PlayerPrefs.SetInt("KnockBack_Level3", 5);
        PlayerPrefs.SetInt("HP_Level3", 5);
        PlayerPrefs.SetInt("SuckerRadius_Level3", 5);
        PlayerPrefs.SetInt("XPGainSpeed_Level3", 5); 

        // Cost per level
        PlayerPrefs.SetInt("DMG_cost_Level1", 100);
        PlayerPrefs.SetInt("ATKSpeed_cost_Level1", 100);
        PlayerPrefs.SetInt("KnockBack_cost_Level1", 100);
        PlayerPrefs.SetInt("HP_cost_Level1", 100);
        PlayerPrefs.SetInt("SuckerRadius_cost_Level1", 100);
        PlayerPrefs.SetInt("XPGainSpeed_cost_Level1", 100); 

        PlayerPrefs.SetInt("DMG_cost_Level2", 150);
        PlayerPrefs.SetInt("ATKSpeed_cost_Level2", 150);
        PlayerPrefs.SetInt("KnockBack_cost_Level2", 150);
        PlayerPrefs.SetInt("HP_cost_Level2", 150);
        PlayerPrefs.SetInt("SuckerRadius_cost_Level2", 150);
        PlayerPrefs.SetInt("XPGainSpeed_cost_Level2", 150); 

        PlayerPrefs.SetInt("DMG_cost_Level3", 200);
        PlayerPrefs.SetInt("ATKSpeed_cost_Level3", 200);
        PlayerPrefs.SetInt("KnockBack_cost_Level3", 200);
        PlayerPrefs.SetInt("HP_cost_Level3", 200);
        PlayerPrefs.SetInt("SuckerRadius_cost_Level3", 200);
        PlayerPrefs.SetInt("XPGainSpeed_cost_Level3", 200); 


        // Starting Bread:
        PlayerPrefs.SetInt("BakedBread",250);

        // Highscore to 0
        PlayerPrefs.SetInt("HighScore", 0);

        PlayerPrefs.Save();
    }

    public int scoreDivider = 100;
    public void OnDeath(int Score){
        if (Score > PlayerPrefs.GetInt("HighScore")){
            PlayerPrefs.SetInt("HighScore",Score);
        }
        PlayerPrefs.SetInt("BakedBread",PlayerPrefs.GetInt("BakedBread")+Score/scoreDivider);
        Debug.Log(PlayerPrefs.GetInt("BakedBread"));

        PlayerPrefs.Save();
    }
}
