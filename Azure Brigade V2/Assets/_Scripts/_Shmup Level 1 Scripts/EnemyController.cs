using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float speed;     //the speed this moves at
	public float tiltMax;   //the maximum angle this can lean
	public float killY; //the Y position where this is removed

	//Explosin variables
	public GameObject explosion;
	public int explodeLifetime = 2; // two second explosion

	//Sound variables
	public AudioClip explodeClip; // explosion sound effect

	//score variables
	UIManager uiManager;  // reference to UI for scoring

	void Start()
	{
		transform.Rotate(0,0,Random.Range(-tiltMax,tiltMax));   //tilt the enemy slightly
		uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	void FixedUpdate()
	{
		//move down relative to where facing(negative transform.up)
		transform.Translate(-transform.up * Time.deltaTime * speed);
		if (Mathf.Abs(transform.position.y) > (Mathf.Abs(killY)) && Mathf.Sign(transform.position.y) == Mathf.Sign(killY)) //check if offscreen
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter( Collider other )
	{
		if (other.tag == "Bullet")
		{
			Destroy(other.gameObject);  //destroy bullet
			uiManager.UpdateScore(); // add points
		}

		if (other.tag == "Player")
		{
			PlayerController player = other.GetComponent<PlayerController>();
			if (player != null)
			{
				player.TakeDamage();  // hurt the player
			}
		}

		if (other.tag == "Shield")
		{
			uiManager.UpdateScore();
		}

		if (other.tag == "Bullet" || other.tag == "Player" || other.tag == "Shield")
		{
			GameObject enemyExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
			AudioSource.PlayClipAtPoint(explodeClip, Camera.main.transform.position, 1f);
			Destroy(enemyExplosion, explodeLifetime);  // destroy explosion after two seconds
			Destroy(gameObject);
		}
	}
}