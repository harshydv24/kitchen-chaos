using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraVisuals : MonoBehaviour
{
    [SerializeField] private Vector3 FinalPostion_Multiplayer;
    [SerializeField] private Vector3 FinalPostion_Singleplayer;
    [SerializeField] private Vector3 FinalPostion_PausedGame;
    [SerializeField] private Vector3 FinalPostion_GameOver;
    [SerializeField] private Vector3 DefaultPostion;
    [SerializeField] private float PanTime;
    [SerializeField] private float PanTimeAlt;

    private CinemachineCamera cinemachineCamera;

    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    private void Start()
    {
        cinemachineCamera.transform.position = DefaultPostion;
        GameManager.Instance.OnGameStateChange += GameManager_OnGameStateChange;
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResume += GameManager_OnGameResume;
    }

    private void GameManager_OnGameResume(object sender, EventArgs e)
    {
        PanCamera(FinalPostion_Singleplayer, PanTimeAlt);
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        PanCamera(FinalPostion_PausedGame, PanTimeAlt);
    }

    private void GameManager_OnGameStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            PanCamera(FinalPostion_Singleplayer, PanTime);
        }

        if (GameManager.Instance.IsGameOverActive())
        {
            PanCamera(FinalPostion_GameOver, PanTimeAlt);
        }
    }

    private void PanCamera(Vector3 finalPosition, float time)
    {
        cinemachineCamera.transform.LeanMoveY(finalPosition.y, time).setEaseOutSine().setIgnoreTimeScale(true);
        cinemachineCamera.transform.LeanMoveZ(finalPosition.z, time).setEaseOutSine().setIgnoreTimeScale(true);
    }
}
