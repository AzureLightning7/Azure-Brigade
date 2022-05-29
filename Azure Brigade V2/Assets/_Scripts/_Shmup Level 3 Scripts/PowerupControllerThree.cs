using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupControllerThree : MonoBehaviour {

    public float speed = 5.0f;
    public float rotateSpeed = 50.0f;

    void Start()
    {
        // Nothing her yet
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); // move
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime); // spin
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   // Get a reference to the Player's script component
            PlayerControllerThree player = other.GetComponent<PlayerControllerThree>();

            if (player != null)         // Make sure we found the player object
                player.EnablePowerup(); // Tell the script to turn on powerup

            Destroy(gameObject);
        }
    }
}
