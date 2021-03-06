﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCameraMovement : MonoBehaviour {

    public Transform player;
    public float smoothing = .2f;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(player.position);
        Vector3 delta = player.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothing);


    }
}
