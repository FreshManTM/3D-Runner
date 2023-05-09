using UnityEngine;
using PlayerMovement;
public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && other.GetComponent<PlayerController>().IsInteractable())
        {
            other.GetComponent<PlayerController>().Death();
            GameManager.Instance.Death();
        }
    }
}
