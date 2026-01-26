using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        Counter_Cuttings.ResetStaticData();
        Counter_Base.ResetStaticData();
        Counter_Trash.ResetStaticData();
    }
}
