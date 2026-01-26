using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public enum TutorialStage
    {
        T_orders,
        T_stats,
    }

    private TutorialStage currentStage;

    private void Start()
    {
        currentStage = TutorialStage.T_orders;
    }

    private void Update()
    {
        // testing input
        if(Input.GetKeyDown(KeyCode.N))
        {
            currentStage++;
        }

        switch (currentStage)
        {
            case TutorialStage.T_orders:
                break;
            case TutorialStage.T_stats:
                // Implement logic for stats tutorial
                break;
        }
    }

    public void NextStage()
    {
        currentStage++;
    }
}
