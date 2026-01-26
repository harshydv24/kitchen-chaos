using UnityEngine;

public class Player_Controler : MonoBehaviour
{
    [SerializeField] private GameInputs gameInputs;
    [SerializeField] private float MoveSpeed = 7f;
    [SerializeField] private float RotationSpeed = 15f;

    private bool isWalking = false;


    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 InputVector = gameInputs.GetMovementVector();
        Vector3 MoveDir = new Vector3(InputVector.x, 0f, InputVector.y);

        float MoveDistance = MoveSpeed * Time.deltaTime;
        float PlayerHeight = 2f;
        float PlayerRadius = 0.65f;
        bool CanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, MoveDir, MoveDistance);

        if (!CanMove)
        {
            // Try to move only in the X direction
            Vector3 MoveDirX = new Vector3(MoveDir.x, 0f, 0f).normalized;
            CanMove = MoveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, MoveDirX, MoveDistance);

            if (CanMove)
            {
                MoveDir = MoveDirX;
            }
            else
            {
                // Try to move only in the Z direction
                Vector3 MoveDirZ = new Vector3(0f, 0f, MoveDir.z).normalized;
                CanMove = MoveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, MoveDirZ, MoveDistance);

                if (CanMove)
                {
                    MoveDir = MoveDirZ;
                }
            }
        }

        if (CanMove)
        {
            transform.position += MoveDir * MoveDistance;
        }

        isWalking = MoveDir.magnitude > 0 ? true : false;
        transform.forward = Vector3.Slerp(transform.forward, MoveDir, RotationSpeed * Time.deltaTime);
    }

    public bool IsMoving()
    {
        return isWalking;
    }
}
