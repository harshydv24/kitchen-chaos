using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance{get; private set;}

    [SerializeField] private AudioClipsSO SFX_AudioClips;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySucess += DeliveryManager_OnDeliverySucess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        Counter_Cuttings.OnAnyCut += Counter_Cuttings_OnAnyCut;
        Player_Interactions.Instance.OnPickedUpSomething += Player_OnPickedUpSomething;
        Counter_Base.OnDropSomething += Counter_Base_OnDropSomething;
        Counter_Trash.OnObjectTrashed += Counter_Base_OnObjectTrashed;
    }

    private void Counter_Base_OnObjectTrashed(object sender, EventArgs e)
    {
        Counter_Trash counter_Trash = sender as Counter_Trash;
        PlaySound(SFX_AudioClips.Trash, counter_Trash.transform.position);
    }

    private void Counter_Base_OnDropSomething(object sender, EventArgs e)
    {
        Counter_Base counter_Base = sender as Counter_Base;
        PlaySound(SFX_AudioClips.ObjectDrop, counter_Base.transform.position);
    }

    private void Player_OnPickedUpSomething(object sender, EventArgs e)
    {
        PlaySound(SFX_AudioClips.ObjectPickup, Player_Interactions.Instance.transform.position);
    }

    private void Counter_Cuttings_OnAnyCut(object sender, EventArgs e)
    {
        // in sender we have the info about which cutting counter has fire the OnAnyCut event. Because it is static event.
        Counter_Cuttings counter_Cuttings = sender as Counter_Cuttings;
        PlaySound(SFX_AudioClips.Choping, counter_Cuttings.transform.position);
    }

    private void DeliveryManager_OnDeliverySucess(object sender, EventArgs e)
    {
        PlaySound(SFX_AudioClips.DeliverySucess, Counter_Delivery.Instance.transform.position);
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, EventArgs e)
    {
        PlaySound(SFX_AudioClips.DeliveryFailed, Counter_Delivery.Instance.transform.position);
    }

    // has to add player footstep sound.

    public void PlayCountdownTimerSound()
    {
        PlaySound(SFX_AudioClips.Countdown, Vector3.zero);
    }

    public void playStoveWarningSound(Vector3 position)
    {
        PlaySound(SFX_AudioClips.Warning, position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
