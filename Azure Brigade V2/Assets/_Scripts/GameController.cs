using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject spawnObject;  //What will be spawned
	public float spawnWait; //how long to wait until start spawning
	public float spawnRate; //time inbetween spawns
	public float spawnPositionRange; //where the object should be spawned
	public float spawnPositionHeight;

	// Powerup variables
	public GameObject powerupObject;
	public float powerupWait;  // how long to wait until start spawning
	public float powerupRateMin;  // minimum of how often powerup spawns in seconds
	public float powerupRateMax;  // maximum of how often powerup spawns in seconds

	// Shield variables
	public GameObject shieldPowerupObject;
	public float shieldWait;
	public float shieldRateMin;
	public float shieldRateMax;

	void Start()
	{
		InvokeRepeating("Spawn",spawnWait,spawnRate);
		InvokeRepeating("SpawnPowerup", powerupWait, Random.Range(powerupRateMin,powerupRateMax));
		InvokeRepeating("SpawnShield", shieldWait, Random.Range(shieldRateMin,shieldRateMax));
	}
	
	//create a new object
	void Spawn()
	{
		Instantiate(spawnObject,new Vector3(Random.Range(-spawnPositionRange,spawnPositionRange),spawnPositionHeight, 0), Quaternion.identity);
	}

	void SpawnPowerup()
	{
		float randomX = Random.Range(-12f, 12f); // Random x offset
		Vector3 powerPos = new Vector3(randomX, 24, 0); // y 24 is top of screen?
		Instantiate(powerupObject, powerPos, Quaternion.identity);
	}

	void SpawnShield()
	{
		float randomX = Random.Range(-12f, 12f); // Random x offset
		Vector3 shieldPowerupPos = new Vector3(randomX, 24, 0); // y 24 is top of screen?
		Instantiate(shieldPowerupObject, shieldPowerupPos, Quaternion.identity);
	}
}