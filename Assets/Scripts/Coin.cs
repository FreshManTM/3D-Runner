using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovement;

public class Coin : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 0, 30 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.AddCoins();
            Destroy(gameObject);
        }
    }
}
