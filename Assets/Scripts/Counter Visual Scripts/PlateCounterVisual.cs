using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private Counter_Plate counterPlate;
    [SerializeField] private Transform PlatVisual;
    [SerializeField] private Transform PlateSpawnPoint;

    private float PlatSpawnOffset = 0.1f;
    private List<GameObject> PlateVisualList;

    private void Awake()
    {
        PlateVisualList = new List<GameObject>();
    }

    private void Start()
    {
        counterPlate.OnPlateSpawned += CounterPlate_OnPlateSpawned;
        counterPlate.OnPlateRemoved += CounterPlate_OnPlateRemoved;
    }

    private void CounterPlate_OnPlateRemoved(object sender, System.EventArgs e)
    {
        RemovePlateVisual();
    }

    private void CounterPlate_OnPlateSpawned(object sender, System.EventArgs e)
    {
        SpawnPlateVisual();
    }

    private void SpawnPlateVisual()
    {
        Transform plateVisualTransform = Instantiate(PlatVisual, PlateSpawnPoint);
        plateVisualTransform.localPosition = new Vector3(0f, PlatSpawnOffset * PlateVisualList.Count, 0f);
        PlateVisualList.Add(plateVisualTransform.gameObject);
    }

    private void RemovePlateVisual()
    {
        GameObject plateVisualGameObject = PlateVisualList[PlateVisualList.Count - 1];
        PlateVisualList.Remove(plateVisualGameObject);
        Destroy(plateVisualGameObject);
    }
}
