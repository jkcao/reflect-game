using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
	public float jumpHeight;
	public float jumpBox;
	private Rigidbody2D rigidBody;
	private bool isGrounded;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		isGrounded = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Check if player is grounded, via reference frame of its two corners.
		Vector2 leftCheck = new Vector2(transform.position.x - jumpBox, transform.position.y);
		Vector2 rightCheck = new Vector2(transform.position.x + jumpBox, transform.position.y);
		Vector2 leftEnd = leftCheck;
		leftEnd.y -= jumpBox * 1.4f;
		Vector2 rightEnd = rightCheck;
		rightEnd.y -= jumpBox * 1.4f;

		Debug.DrawLine(leftCheck, leftEnd, Color.green);
		Debug.DrawLine(rightCheck, rightEnd, Color.green);

		if(Physics2D.Linecast(leftCheck, leftEnd, 1 << LayerMask.NameToLayer("Ground"))
			|| Physics2D.Linecast(rightCheck, rightEnd, 1 << LayerMask.NameToLayer("Ground"))) {
			isGrounded = true;
		}

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
