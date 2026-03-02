using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{get; private set;}

    public event EventHandler OnGameStateChange;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResume;

    private enum GameStates
    {
        WaitingToStart,
        CountdownToStart,
        GameIsPlaying,
        GameOver,
    }

    private GameStates CurrentGameState;
    private float CountdownToStartTimer = 3f;
    private float GameIsPlayingTimer;
    private float GameIsPlayingTimerMax = 90f;
    private bool IsPaused = false;

    private void Awake()
    {
        Instance = this;
        CurrentGameState = GameStates.WaitingToStart;
    }

    private void Start()
    {
        GameInputs.Instance.OnPauseAction += GameInputs_OnPauseAction;
        GameInputs.Instance.OnInteractAction += GameInputs_OnInteractAction;
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, EventArgs e)
    {
        GameIsPlayingTimer += 10f;
    }

    private void GameInputs_OnInteractAction(object sender, EventArgs e)
    {
        if(CurrentGameState == GameStates.WaitingToStart)
        {
            CurrentGameState = GameStates.CountdownToStart;
            OnGameStateChange?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInputs_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGamePause();
    }

    private void Update()
    {
        switch (CurrentGameState)
        {
            case GameStates.WaitingToStart:
                break;
            case GameStates.CountdownToStart:
                CountdownToStartTimer -= Time.deltaTime;
                if(CountdownToStartTimer <= 0f)
                {
                    GameIsPlayingTimer = GameIsPlayingTimerMax;
                    CurrentGameState = GameStates.GameIsPlaying;
                    OnGameStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameStates.GameIsPlaying:
                GameIsPlayingTimer -= Time.deltaTime;
                if(GameIsPlayingTimer <= 0f)
                {
                    CurrentGameState = GameStates.GameOver;
                    OnGameStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameStates.GameOver:
                break;
        }
    }

    public void ToggleGamePause()
    {
        if(CurrentGameState == GameStates.GameIsPlaying)
        {
            if (IsPaused)
            {
                Time.timeScale = 1f;
                OnGameResume?.Invoke(this, EventArgs.Empty);
                IsPaused = false;
            }
            else
            {
                Time.timeScale = 0f;
                OnGamePaused?.Invoke(this, EventArgs.Empty);
                IsPaused = true;
            }
        }

        OnGameStateChange?.Invoke(this, EventArgs.Empty);
    }

    public bool IsGamePlaying()
    {
        return CurrentGameState == GameStates.GameIsPlaying;
    }

    public float GetCountDownTimer()
    {
        return CountdownToStartTimer;
    }

    public bool IsCountdownToStartActive()
    {
        return CurrentGameState == GameStates.CountdownToStart;
    }

    public bool IsGameOverActive()
    {
        return CurrentGameState == GameStates.GameOver;
    }

    public bool IsGameIsPlayingActive()
    {
        return CurrentGameState == GameStates.GameIsPlaying;
    }

    public float GetGameIsPlayingTimerNormalised()
    {
        // doing this to get a value between 0 and 1, so we can use it for the timer UI fill amount!
        return GameIsPlayingTimer / GameIsPlayingTimerMax;
    }

    public float GetGameIsPlayingTimerRaw()
    {
        return GameIsPlayingTimerMax;
    }

    public bool IsGamePaused()
    {
        return IsPaused;
    }

}
