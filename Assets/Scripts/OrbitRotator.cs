using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitRotator : MonoBehaviour {
    public Vector3 rotCenter;
    public int speed;
	// Use this for initialization
	void Start () {
		
	}
	
	void FixedUpdate () {
        transform.RotateAround(rotCenter, Vector3.forward, speed/10 * Time.deltaTime);
    }
}
