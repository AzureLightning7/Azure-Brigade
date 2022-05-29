using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarGameController : MonoBehaviour {

	//Spawn variables
	public GameObject farSpawnObject;
	public float farSpawnWait;
	public float farSpawnRate; //time inbetween spawns
	public float farSpawnPositionRange; //where the object should be spawned
	public float farSpawnPositionHeight;
	public float farSpawnPositionDepth;

	//Powerup variables
	public GameObject farPowerupObject;
	public float farPowerupWait;  // how long to wait until start spawning
	public float farPowerupRateMin;  // minimum of how often powerup spawns in seconds
	public float farPowerupRateMax; // maximum of how often powerup spawns in seconds

	// Shield variables
	public GameObject farShieldPowerupObject;
	public float farShieldWait;
	public float farShieldRateMin;
	public float farShieldRateMax;

	void Start()
	{
		InvokeRepeating("FarSpawn",farSpawnWait,farSpawnRate);
		InvokeRepeating("FarSpawnPowerup", farPowerupWait, Random.Range(farPowerupRateMin,farPowerupRateMax));
		InvokeRepeating("FarSpawnShield", farShieldWait, Random.Range(farShieldRateMin,farShieldRateMax));
	}
	
	void FarSpawn()
	{
		Instantiate(farSpawnObject,new Vector3(Random.Range(-farSpawnPositionRange,farSpawnPositionRange),farSpawnPositionHeight, farSpawnPositionDepth), Quaternion.identity);
	}

	void FarSpawnPowerup()
	{
		float randomX = Random.Range(-12f, 12f); // Random x offset
		Vector3 farPowerPos = new Vector3(randomX, -40, 30); // y 24 is top of screen?
		Instantiate(farPowerupObject, farPowerPos, Quaternion.identity);
	}

	void FarSpawnShield()
	{
		float randomX = Random.Range(-12f, 12f); // Random x offset
		Vector3 farShieldPowerupPos = new Vector3(randomX, -40, 30); // y 24 is top of screen?
		Instantiate(farShieldPowerupObject, farShieldPowerupPos, Quaternion.identity);
	}
}
