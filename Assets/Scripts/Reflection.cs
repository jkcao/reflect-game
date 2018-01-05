using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : Character {
	protected override void Movement(float horizontal, bool jump) {
		// Moves LR and jumps if input & able.
		if (!jump) {
			rigidBody.velocity = new Vector2 (-(speed * horizontal), rigidBody.velocity.y);
		} else {
			rigidBody.velocity = new Vector2 (-(speed * horizontal), jumpHeight);
			isGrounded = false;
		}
	}
}
