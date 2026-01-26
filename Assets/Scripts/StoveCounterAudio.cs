using System;
using UnityEngine;

public class StoveCounterAudio : MonoBehaviour
{
    [SerializeField] private Counter_Stove counter_Stove;

    private AudioSource audioSource;
    private bool IsStoveBurning = false;
    private float WarningSoundTimer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        counter_Stove.OnStateChanged += counter_Stove_OnStateChanged;
        counter_Stove.OnStoveProgressChanged += counter_OnStoveProgressChanged;
    }

    private void counter_OnStoveProgressChanged(object sender, Counter_Stove.OnStoveProgressChangedEventArgs e)
    {
        IsStoveBurning = e.StoveProgress >= 0.5f && counter_Stove.IsFriedStateActive();
    }

    private void counter_Stove_OnStateChanged(object sender, Counter_Stove.OnStateChangedEventArgs e)
    {
        bool playsound = e.state == Counter_Stove.StoveState.Frying || e.state == Counter_Stove.StoveState.Fried;
        if (playsound)
        {
            audioSource.Play();

        }
        else
        {
            audioSource.Pause();
        }
    }

    private void Update()
    {
        StoveBurningSound();
    }

    private void StoveBurningSound()
    {
        if (IsStoveBurning)
        {
            WarningSoundTimer -= Time.deltaTime;
            if(WarningSoundTimer <= 0f)
            {
                float WarningSoundTimerMax = 0.2f;
                WarningSoundTimer = WarningSoundTimerMax;
                SoundManager.Instance.playStoveWarningSound(counter_Stove.transform.position);
            }
        }
    }
}
