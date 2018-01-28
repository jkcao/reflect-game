using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : Player {
	protected override void Movement(float horizontal, bool charjump, bool reflectjump) {
		// Moves RL and jumps if input & able.
		if (isGrounded && reflectjump) {
			rigidBody.velocity = new Vector2 (-(speed * horizontal), jumpHeight);
		} else {
			rigidBody.velocity = new Vector2 (-(speed * horizontal), rigidBody.velocity.y);
			isGrounded = false;
		}
	}
}
