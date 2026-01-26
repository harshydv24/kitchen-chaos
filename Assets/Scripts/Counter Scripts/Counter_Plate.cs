using System;
using UnityEngine;

public class Counter_Plate : Counter_Base
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectsSO kitchenObjectSO;
    
    private float PlateSpawnTime;
    private float MaxPlateSpawnTime = 3f;
    private int PlateSpawnAmount;
    private int MaxPlateSpawnAmount = 5;

    private void Update()
    {
        PlateSpawnTime += Time.deltaTime;
        if(PlateSpawnTime > MaxPlateSpawnTime)
        {
            PlateSpawnTime = 0f;
            if(GameManager.Instance.IsGameIsPlayingActive() && PlateSpawnAmount < MaxPlateSpawnAmount)
            {
                PlateSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void CounterInteraction(Player_Interactions player)
    {
        if(!player.HasKitchenObject())
        {
            if(PlateSpawnAmount > 0)
            {
                PlateSpawnAmount--;
                KitchenObjects.SpawnKitchenObject(kitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Debug.Log("No plates available!");
            }
        }
        else
        {
            Debug.Log("Player is already holding something!");
        }
    }
}
