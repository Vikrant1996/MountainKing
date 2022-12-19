using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{  
    public float MoveSpeed = 3f;
    Rigidbody2D MyRigidbody;
    
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();    
        
    }

  
    void Update()
    {
        MyRigidbody.velocity = new Vector2 (MoveSpeed, 0f);
    }

     void OnTriggerExit2D(Collider2D other) {
         MoveSpeed =- MoveSpeed;
        FlipEnemyFacing();
    }
    void FlipEnemyFacing(){

       transform.localScale = new Vector2(-(Mathf.Sign(MyRigidbody.velocity.x)), 1f);
    }
}
