using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour {

    Rigidbody2D rb;
    public float speedFactor;
    float speed;
    float rot;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	

	void FixedUpdate () {
        speed = Input.GetAxis("Vertical");
        rot = Input.GetAxis("Horizontal");
        Vector2 speedVector = new Vector2(0, speed * speedFactor);
        rb.AddRelativeForce(speedVector);
        Vector3 rotVector = new Vector3(0, 0, -rot*2);
        transform.Rotate(rotVector);
	}
}
