using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;     //how fast this travels upwards
	public float lifetime; //how long this lasts (in seconds)

	void Start()
	{
		rb = GetComponent<Rigidbody>(); //reference rigidBody
		rb.velocity = transform.up * speed;   //Set movement for bullet (upward)
		Invoke("Remove",lifetime);  //Set a "timer" for if this bullet exists too long
	}
	
	void Remove()
	{
		Destroy(gameObject);
	}
}
