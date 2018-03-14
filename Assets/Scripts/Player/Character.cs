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
      rigidBody.velocity = new Vector2(0, jumpHeight);
    }
    else
    {
      rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
      isGrounded = false;
    }
  }

  protected override void SpecialAbility(int condition)
  {

  }

}
