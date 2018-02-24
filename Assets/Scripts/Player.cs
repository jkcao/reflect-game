using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Player : MonoBehaviour {

	public float speed;
	public float jumpHeight;
	public float jumpBox;
	protected Rigidbody2D rigidBody;
	protected bool isGrounded;
	protected float groundPosition;
	protected float halfHeight;
	public Vector2 respawn;

	public Player mirror;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		isGrounded = true;
		respawn = transform.position;
		halfHeight = this.GetComponent<SpriteRenderer>().bounds.size.y / 2;
		groundPosition = this.transform.position.y - halfHeight;
	}

	protected void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Death") {
			Application.LoadLevel(Application.loadedLevel);
		}
		if (col.transform.tag == "MovingPlatform") {
			transform.parent = col.transform;
		}
	}

	protected void OnCollisionExit2D (Collision2D col) {
		if (col.transform.tag == "MovingPlatform") {
			transform.parent = null;
		}
	}

	protected void OnCollisionExit2D (Collision2D col){
		if (col.gameObject.tag == "Ground") {
			groundPosition = this.transform.position.y - halfHeight;
		}
	}

	// Update is called once per frame
	void Update () {
		// Reset level by player.
		if(Input.GetKeyDown("space")) Application.LoadLevel(Application.loadedLevel);

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
			// Calculate to keep for allocating mirror platform dynamically.
		}

		float horizontal = Input.GetAxis("Horizontal");

		// Call Movement function.
		Movement (horizontal, Input.GetKeyDown ("k"));
	}

	// Returns the y-position of the ground the player was last standing on.
	public float getGroundPosition() {
		return groundPosition;
	}


	protected abstract void Movement (float horizontal, bool jump);
}
