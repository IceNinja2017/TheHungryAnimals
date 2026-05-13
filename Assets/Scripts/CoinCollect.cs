using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public AudioSource coinCollextsfx;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            if (coinCollextsfx != null) 
            {
                coinCollextsfx.pitch = Random.Range(1f, 1.4f);
                coinCollextsfx.Play();
            }
            GameManager.Instance.AddScore(1);
            Destroy(other.gameObject);
        }
    }
}
