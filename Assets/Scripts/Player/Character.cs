using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Player
{
  protected override void Movement(float horizontal, bool jump)
  {
    // Moves LR and jumps if input & able.
    if (isGrounded && jump)
    {
			rigidBody.velocity = new Vector2(-speed * horizontal, jumpHeight);
    }
    else
    {
			rigidBody.velocity = new Vector2(speed * horizontal, rigidBody.velocity.y);
      isGrounded = false;
    }
  }

	protected override void anim(float direction, bool jump)
	{
		
	}

	IEnumerator placeBlock() {
		yield return new WaitForSeconds(0.15f);
		GameObject block = (GameObject)Instantiate(blockPrefab);
		block.GetComponent<Transform>().position = new Vector2(transform.position.x + (dir * 2.3f), transform.position.y + .4f);
	}

  protected override void SpecialAbility(int condition)
  {
		if (condition == 0 && restrict.getRestricted() && blockPrefab != null)
		{
			//Make sure there's nothing already in front of you
			if ((dir == -1 && !hitRight) || (dir == 1 && !hitLeft))
			{
				this.GetComponent<AudioSource> ().Play ();
				StartCoroutine (placeBlock ());
			}
		}
  }

}
