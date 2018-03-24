using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : Player
{

  protected override void Movement(float horizontal, bool jump)
  {
    // Moves RL and jumps if input & able.
    if (isGrounded && jump)
    {
      rigidBody.velocity = new Vector2(speed * horizontal, jumpHeight);
    }
    else
    {
      rigidBody.velocity = new Vector2(-(speed * horizontal), rigidBody.velocity.y);
      isGrounded = false;
    }
  }

  protected override void SpecialAbility(int condition)
  {
    if (condition == 0 && restrict.getRestricted() && blockPrefab != null)
    {

      //Make sure there's nothing already in front of you
      if ((dir == -1 && !hitRight) || (dir == 1 && !hitLeft))
      {
        GameObject block = (GameObject)Instantiate(blockPrefab);
        block.GetComponent<Transform>().position = new Vector2(transform.position.x + (dir * -1 * 1.3f), transform.position.y);
      }
    }
  }
}
