using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{    
     public AudioClip coinPickupSFX;
     public int pointForCoinPicker = 100;
     void OnTriggerEnter2D(Collider2D other) 
     {
        if(other.tag == "Player"){
            
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
