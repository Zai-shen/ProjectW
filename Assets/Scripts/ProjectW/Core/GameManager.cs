using System;
using OPlan.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingleton<GameManager>
{
    // public PauseMenu PauseMenu;
    private bool _gamePaused;
    private int _activeScene = 0;

    public Action ResetPause;

    #region Singleton
    
    private static GameManager _gameManager;
    public static GameManager Instance
    {
        get
        {
            if (!_gameManager)
            {
                _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (!_gameManager)
                    Debug.LogError("There needs to be one active GameManager script on a GameObject in your scene.");
            }

            return _gameManager;
        }
        private set
        {
            
        }
    }

    #endregion
    
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
        LoadScene(0);
    }
}
