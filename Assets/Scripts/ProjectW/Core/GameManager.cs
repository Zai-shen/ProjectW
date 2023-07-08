using System;
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
