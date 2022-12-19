using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{    
     public float BulletSpeed = 20f;
     Rigidbody2D MyRigidbody;
     PlayerMovement Player;

     float xSpeed;
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<PlayerMovement>();
        xSpeed = Player.transform.localScale.x * BulletSpeed;
    }

    
    void Update()
    {
        MyRigidbody.velocity = new Vector2 (xSpeed, 0f);
    }
     void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){

             Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
     void OnCollisionEnter2D(Collision2D other) {
          Destroy(gameObject);
    }
}
