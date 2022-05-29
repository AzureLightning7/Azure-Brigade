using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerTwo : MonoBehaviour {

	private Rigidbody rb; //the GameObject's Rigidbody
	public float speed; //Max speed of ship
	public float tilt;  //Max Degree of rotation when strafing
	public float boundaryWidth, boundaryHeight; //width and height area boundry

	//Shooting Variables
	public GameObject projectile;   //the type of projectile being shot
	public float reloadTime = 0.25f;//How long to wait before shooting again (in seconds)
	private float canFire = 0f;  // Next time ship can fire

	//Powerup Variables
	public bool powerupActive = false;
	public float powerupTime = 5.0f; // Power up seconds

	//Shield Variables
	public bool shieldActive = false;
	public float shieldTime = 5.0f;
	public GameObject myShield;

	//Lives Variables
	public int lives = 3;
	public float restartDelay;
	UIManagerTwo uiManager;  // reference to UI for lives image

	//Sound Variables
	//public GameObject mainCamera;
	private AudioSource shootAudio; // shooting sound
	public AudioClip powerupClip;
	public AudioClip shieldClip;
	public AudioClip losePowerupClip;
	public AudioClip loseShieldClip;
	public AudioClip loseGameClip;

	//Shift Variables
	public float shiftTime;  // how long it takes to shift
	public float shiftDepth; // how far to shift on the z axis
	public int shiftDirection = 0; // which way currently shifting (0 = not shifting)

	void Start()
	{
		rb = GetComponent<Rigidbody>(); //assign rigidbody
		uiManager = GameObject.Find("CanvasTwo").GetComponent<UIManagerTwo>();
		if (uiManager != null)
			uiManager.UpdateLives(lives); // Tell UI manager to show three lives at Start
		myShield = GameObject.Find("Shield");
		if (myShield != null)
			myShield.SetActive(false);
		shootAudio = GetComponent<AudioSource>(); // shooting sound
		
	}
	
	void Update()
	{
		//find player Input values, arrow keys or wasd
		if (Input.GetButtonDown("Jump") && shiftDirection == 0) // not currently shifting
		{
			print("Shift Initiated");
			if (transform.position.z <= 0)
				shiftDirection = 1;  // moving down
			else
				shiftDirection = -1; // moving up
		}

		//check is able to shoot
		if(Input.GetButton("Fire1") && shiftDirection == 0)
		{
			if (Time.time > canFire) //Time.time is how long the game has been running in seconds
			{
				shootAudio.Play();  // play shooting sound

				float shootDirection = 0;
				if (transform.position.z >= shiftDepth)
				{
					print("Firing shifted!");
					shootDirection = 180;
				}
				if (!powerupActive) // NOT powerupActive, regular fire
				{
					Instantiate(projectile, transform.position, Quaternion.Euler(0,0,shootDirection)); //create bullet
					canFire = Time.time + reloadTime;
				}
				else // Powerup!!!
				{
					Instantiate(projectile, transform.position + new Vector3(2,0,0), Quaternion.Euler(0,0,shootDirection));
					Instantiate(projectile, transform.position + new Vector3(-2,0,0), Quaternion.Euler(0,0,shootDirection));
					canFire = Time.time + (reloadTime * 0.75f);  // Faster reload time for powerups
				}
			}
		}
	}

	void FixedUpdate()  // Notice this is FixedUpdate. Physics done here!
	{
		//find player Input values, arrow keys or wasd
		float hInput = Input.GetAxis("Horizontal");
		float vInput = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(hInput,vInput,0);
		// check if shifting and move across z axis instead
		if (shiftDirection != 0)
			movement = new Vector3(0, 0, shiftDirection * (shiftDepth / shiftTime));

		rb.velocity = movement * speed; //move the ship

		// check if done shifting
		if ((rb.position.z <= 0 && shiftDirection == -1) // has reached near plane
			|| (rb.position.z >= shiftDepth && shiftDirection == 1)) // has reached far plane
		{
			shiftDirection = 0; // stop shifting
		}
		
		//keep ship inside of boundaries
		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, -boundaryWidth/2, boundaryWidth/2),  //Horizontal
			Mathf.Clamp(rb.position.y, -boundaryHeight/2, boundaryHeight/2),  //Vertical
			Mathf.Clamp(rb.position.z, 0, shiftDepth)); //Z position (NOW USED)

		//Tilt the ship a small amount when moving
		rb.rotation = Quaternion.Euler(
			vInput * tilt, //Change pitch when moving vertically
			hInput * -tilt,
			180 * rb.position.z / shiftDepth); //Change yaw when moving horizontally
	}

	public void EnablePowerup()
	{
		AudioSource.PlayClipAtPoint(powerupClip, Camera.main.transform.position, 1f);
		powerupActive = true;
		Invoke("DisablePowerup", powerupTime);
	}

	void DisablePowerup()
	{
		AudioSource.PlayClipAtPoint(losePowerupClip, Camera.main.transform.position, 1f);
		powerupActive = false;
	}

	public void EnableShield()
	{
		AudioSource.PlayClipAtPoint(shieldClip, Camera.main.transform.position, 1f);
		myShield.SetActive(true);
		shieldActive = true;  // called by power cube	
		Invoke("DisableShield", powerupTime);
	}

	void DisableShield()
	{
		AudioSource.PlayClipAtPoint(loseShieldClip, Camera.main.transform.position, 1f);
		myShield.SetActive(false);
		shieldActive = false;
	}

	public void TakeDamage()
	{
		//Player hit something
		lives--;

		uiManager.UpdateLives(lives);

		if (lives <= 0)
		{
			Invoke("ReloadScene", restartDelay);
			this.gameObject.SetActive(false);
			AudioSource.PlayClipAtPoint(loseGameClip, Camera.main.transform.position, 1f);
			GameObject myCamera = GameObject.FindGameObjectWithTag("MainCamera");
			MusicControllerTwo music = myCamera.GetComponent<MusicControllerTwo>();
			music.EndMusic();
			uiManager.GameOver();
		}
	}

	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}