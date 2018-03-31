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
      rigidBody.velocity = new Vector2(speed * horizontal, jumpHeight);
    }
    else
    {
      rigidBody.velocity = new Vector2(speed * horizontal, rigidBody.velocity.y);
      isGrounded = false;
    }
  }

  protected override void SpecialAbility(int cond)
  {
    //I'm not sure what to do with this
  }

}
