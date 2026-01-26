using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    Player_Controler PCScript;
    Animator PlayerAnimator;
    int isWalkingBoolean = Animator.StringToHash("IsWalking");

    private void Awake()
    {
        PlayerAnimator = GetComponent<Animator>();
        PCScript = GetComponentInParent<Player_Controler>();
    }

    private void Update()
    {
        PlayerAnimator.SetBool(isWalkingBoolean, PCScript.IsMoving());
    }
}
