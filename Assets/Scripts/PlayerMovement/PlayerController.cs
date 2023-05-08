using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerMovement
{
    public class PlayerController : MonoBehaviour
    {
        CharacterController controller;
        Vector3 dir;

        [SerializeField] public float speed;
        [SerializeField] public float maxSpeed;

        [SerializeField] int currentLane;
        [SerializeField] float lineDistance;

        [SerializeField] float jumpForce;
        [SerializeField] public float gravity;
        [SerializeField] bool isJumping;

        [SerializeField] float slideDuration;
        [SerializeField] float slideSpeed;
        [SerializeField]bool isSliding;

        Vector3 targetPosition;
        private Movable movementStrategy;

        [SerializeField] bool isDead;
        Animator animator;

        Vector3 resumePos;
        void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
            StartCoroutine(IncreaseSpeed());
        }

        void Update()
        {
            if (controller.isGrounded)
                isJumping = false;
            else
                isJumping = true;
            if(!isDead)
                InputController();
            targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
            if (currentLane == 0)
                targetPosition += Vector3.left * lineDistance;
            else if (currentLane == 2)
                targetPosition += Vector3.right * lineDistance;

            if (transform.position == targetPosition)
                return;
        }


        private void FixedUpdate()
        {
            if (!isDead)
            {
                // Move left or right
                if (movementStrategy != null)
                    movementStrategy.Move(controller, targetPosition);

                dir.z = speed;
            }
            // Apply gravity
            dir.y += gravity * Time.deltaTime;
            // Move the player
            controller.Move(dir * Time.deltaTime);
        }
        private void InputController()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeController.swipeLeft)
            {
                if (currentLane > 0)
                {
                    currentLane--;
                    movementStrategy = new LeftMovementStrategy(this);
                }

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeController.swipeRight)
            {
                if (currentLane < 2)
                {
                    currentLane++;
                    movementStrategy = new RightMovementStrategy(this);
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || SwipeController.swipeUp)
            {
                if (controller.isGrounded && !isSliding)
                {
                    isJumping = true;
                    dir.y = jumpForce;
                    animator.SetBool("isJumping", true);
                    StartCoroutine(DisableJumping());
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || SwipeController.swipeDown)
            {
                if (!isSliding && !isJumping)
                    StartCoroutine(Slide());
            }
        }

        IEnumerator DisableJumping()
        {
            yield return new WaitForSeconds(.1f);

            if (controller.isGrounded)
            {
                animator.SetBool("isJumping", false);
                isJumping = false;
                yield return null;
            }
            else
            {
                StartCoroutine(DisableJumping());
            }

            yield return null;
        }
        IEnumerator Slide()
        {
            isSliding = true;
            controller.height = 1;
            animator.SetBool("isSliding", true);

            yield return new WaitForSeconds(slideDuration);
            StartCoroutine(IncreasePlayerHeight());

            isSliding = false;
            animator.SetBool("isSliding", false);

            yield return null;
        }
        IEnumerator IncreasePlayerHeight()
        {
            if(controller.height < 2)
            {
                controller.height += .05f;
                yield return new WaitForSeconds(0.01f);

                StartCoroutine(IncreasePlayerHeight());
            }
            else
            {
                controller.height = 2;
            }
            yield return null;
        }
        IEnumerator IncreaseSpeed()
        {
            yield return new WaitForSeconds(4f);

            if (speed < maxSpeed)
            {
                speed += 1;
                StartCoroutine(IncreaseSpeed());
            }
        }

        public void Death()
        {
            resumePos = transform.position;
            isDead = true;
            dir = Vector3.zero;
            animator.SetBool("isDead", true);
        }
    }
}