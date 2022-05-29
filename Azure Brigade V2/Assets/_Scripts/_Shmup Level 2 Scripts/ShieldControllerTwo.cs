﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldControllerTwo : MonoBehaviour {

	public float speed = 5.0f;
	public float rotateSpeed = 50.0f;

	void Start()
	{
		// Nothing here yet
	}
	
	void Update()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime); // move
		transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime); // spin
	}

	private void OnTriggerEnter( Collider other )
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerControllerTwo player = other.GetComponent<PlayerControllerTwo>();

			if (player != null)
				player.EnableShield();

			Destroy(gameObject);
		}
	}
}