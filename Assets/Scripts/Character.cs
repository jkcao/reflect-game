using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour {

	public float speed;
	public float jumpHeight;
	public float jumpBox;
	protected Rigidbody2D rigidBody;
	protected bool isGrounded;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		isGrounded = true;
	}

	// Update is called once per frame
	void FixedUpdate () {
		// Check if player is grounded, via three points: its center and its two corners.
		float end = jumpBox * 1.4f;
		Vector2 leftCheck = new Vector2(transform.position.x - jumpBox, transform.position.y);
		Vector2 rightCheck = new Vector2(transform.position.x + jumpBox, transform.position.y);
		Vector2 midCheck = transform.position;
		Vector2 leftEnd = leftCheck;
		leftEnd.y -= end;
		Vector2 rightEnd = rightCheck;
		rightEnd.y -= end;
		Vector2 midEnd = midCheck;
		midEnd.y -= end;

		if (Physics2D.Linecast (leftCheck, leftEnd, 1 << LayerMask.NameToLayer ("Ground"))
			|| Physics2D.Linecast (rightCheck, rightEnd, 1 << LayerMask.NameToLayer ("Ground"))
			|| Physics2D.Linecast (midCheck, midEnd, 1 << LayerMask.NameToLayer ("Ground"))) {
			isGrounded = true;
		} else {
			isGrounded = false;
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

	protected abstract void Movement (float horizontal, bool jump);
}
