using UnityEngine;

namespace PlayerMovement
{
    public class RightMovementStrategy : Movable
    {
        Vector3 targetPosition = new Vector3(0, 1, 0);
        PlayerController player;
        bool targetIsSet;

        public RightMovementStrategy(PlayerController _player)
        {
            player = _player;
        }

        public override void Move(CharacterController controller, Vector3 targetPosition)
        {
            Vector3 diff = targetPosition - player.transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }
    }
}