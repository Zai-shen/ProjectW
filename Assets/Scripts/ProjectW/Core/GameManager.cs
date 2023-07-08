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
    public Baker ABaker;

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
        base.Awake();
        _activeScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        if (_activeScene == 1)
        {
            ABaker = FindObjectOfType<Baker>();
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
        LoadScene(0);

        // test if it is first time user starts game
        if (PlayerPrefs.HasKey("PlayedBefore")){
            Debug.Log("Not First Time Playing");
        } else {
            initilizeMetaLevels();
        }
    }
    public void initilizeMetaLevels(){
        //set all metalevels to zero
        PlayerPrefs.SetInt("MetaHPLevel", 0);
        PlayerPrefs.SetInt("MetaDMGLevel", 0);
        PlayerPrefs.SetInt("MetaSuckerRadiusLevel", 0);
        PlayerPrefs.SetInt("MetaSuckerConeLevel", 0);
        PlayerPrefs.SetInt("MetaATKSpeedLevel", 0);
        PlayerPrefs.SetInt("MetaKnockBackLevel", 0);


        // Starting Powder:
        PlayerPrefs.SetInt("BakedBread",0);


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
