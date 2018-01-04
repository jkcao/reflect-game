using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
	public float jumpHeight;
	private Rigidbody2D rigidBody;
	private bool isGrounded;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		isGrounded = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Check if player is grounded.
		Vector2 endpoint = transform.position;
		endpoint.y -= 0.9f;
		isGrounded = Physics2D.Linecast(transform.position, endpoint, 1 << LayerMask.NameToLayer("Ground"));

		float horizontal = Input.GetAxis("Horizontal");

		// Check if player can jump.
		bool jump = false;
			if (isGrounded && (Input.GetKeyDown ("space"))) {
			jump = true;
		}

		// Call Movement function.
		Movement (horizontal, jump);
	}

	private void Movement(float horizontal, bool jump) {
		// Moves LR and jumps if input & able.
		if (!jump) {
			rigidBody.velocity = new Vector2 (speed * horizontal, rigidBody.velocity.y);
		} else {
			rigidBody.velocity = new Vector2 (speed * horizontal, jumpHeight);
			isGrounded = false;
		}
	}
}
