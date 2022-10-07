using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{


    public float arrowSpeed = 1.5f;
    private Rigidbody2D rb;
    void Awake(){
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
    }
    void Start() {
        if (this.gameObject.tag == "Arrow1"){
            arrowSpeed = 1.5f;
        }
          if (this.gameObject.tag == "Arrow2"){
            arrowSpeed = -1.5f;
        }
    }
    void Update()
    {
        rb.velocity = new Vector2(arrowSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(this.gameObject.tag == "Arrow1" && other.tag == "P2"){
            other.gameObject.GetComponent<DamageScript>().DamageDealt(3f);
            Destroy(gameObject);
        }
        if(this.gameObject.tag == "Arrow2" && other.tag == "P1"){
            other.gameObject.GetComponent<DamageScript>().DamageDealt(3f);  
            Destroy(gameObject);
        }
    }
}