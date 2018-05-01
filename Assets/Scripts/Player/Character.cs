using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Player
{
	public AudioClip makeSound;

  protected override void Movement(bool canMove, float horizontal, bool jump)
  {
		if (canMove) {
			// Moves LR and jumps if input & able.
			if (isGrounded && jump) {
				rigidBody.velocity = new Vector2 (-speed * horizontal, jumpHeight);
			} else {
				rigidBody.velocity = new Vector2 (speed * horizontal, rigidBody.velocity.y);
				isGrounded = false;
			}
		} else {
			rigidBody.velocity = new Vector2 (0, 0);
		}
  }

	IEnumerator placeBlock() {
		Vector2 placeLoc = new Vector2(transform.position.x + (dir * 2.4f), transform.position.y + .4f);
		yield return new WaitForSeconds(0.5f);
		GameObject block = (GameObject)Instantiate(blockPrefab);
		block.GetComponent<Transform>().position = placeLoc;
		specAbil = false;
		canMove = true;
		mirror.setCanMove (true);
	}

  protected override void SpecialAbility(int condition)
  {
		if (condition == 0 && restrict.getRestricted() && blockPrefab != null)
		{
			//Make sure there's nothing already in front of you
			if ((dir == -1 && !hitRight) || (dir == 1 && !hitLeft))
			{
				audioPlay.Stop ();
				audioPlay.clip = makeSound;
				audioPlay.volume = 0.3f;
				StartCoroutine (playSound ());
				soundPlaying = true;
				canMove = false;
				mirror.setCanMove (false);
				specAbil = true;
				StartCoroutine (placeBlock ());
			}
		}
  }

}
