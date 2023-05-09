using System.Collections;
using UnityEngine;

namespace PlayerMovement
{
    public class PlayerController : MonoBehaviour
    {
        CharacterController controller;
        Animator animator;

        Movable movementStrategy;
        
        [SerializeField] public float speed;
        [SerializeField] public float maxSpeed;

        int currentLane;
        [SerializeField] float lineDistance;

        [SerializeField] float jumpForce;
        [SerializeField] float gravity;

        [SerializeField] float slideDuration;
        [SerializeField] float slideSpeed;


        [SerializeField] Material defaultMat;
        [SerializeField] Material transparentMat;
        Vector3 dir;
        Vector3 targetPosition;

        bool isJumping;
        bool isSliding;
        bool isDead;
        bool isInteractable;

        void Start()
        {
            currentLane = 1;
            isInteractable = true;
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
            StartCoroutine(IncreaseSpeed());
        }

        void Update()
        {
            if(!isDead)
                InputControl();

            targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
            //Change the lanes 
            if (currentLane == 0)
                targetPosition += Vector3.left * lineDistance;
            else if (currentLane == 2)
                targetPosition += Vector3.right * lineDistance;
        }


        private void FixedUpdate()
        {
            MovePlayer();
        }

        //Move the player in the set direction
        void MovePlayer()
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

        //Control the player's input
        private void InputControl()
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

        //Disable jumnping after player hit the ground
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
        
        //Slide
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

        //Increase player's height after the slide
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

        //Increase player speed per time
        IEnumerator IncreaseSpeed()
        {
            yield return new WaitForSeconds(4f);

            if (speed < maxSpeed)
            {
                speed += 1;
                StartCoroutine(IncreaseSpeed());
            }
        }

        //Player's death
        public void Death()
        {
            dir = Vector3.zero;
            isDead = true;
            animator.SetBool("isDead", true);
            isInteractable = false;

        }

        //Resume player's position after watching the add
        public void ResumePosition()
        {
            animator.SetBool("isDead", false);
            isDead = false;
            StartCoroutine(UnInteractable());
        }

        //Disable interact with the obstacles and change player's material
        IEnumerator UnInteractable()
        {
            SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var renderer in renderers)
            {
                renderer.material = transparentMat;
            }
            yield return new WaitForSeconds(3);

            foreach (var renderer in renderers)
            {
                renderer.material = defaultMat;
            }
            isInteractable = true;
            yield return null;

        }
        //Get bool if player is interactable
        public bool IsInteractable()
        {
            return isInteractable;
        }
    }
}