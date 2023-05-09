using UnityEngine;

namespace PlayerMovement
{
    public class LeftMovementStrategy: Movable
    {
        PlayerController player;

        public LeftMovementStrategy(PlayerController _player)
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
