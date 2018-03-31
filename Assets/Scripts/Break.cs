using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour {

  private bool touchingChar;

  private void Update()
  {
    if (Input.GetKeyDown("e"))
    {
      BreakBlock();
    }
  }

  private void OnCollisionStay2D(Collision2D other)
  {
    if (other.gameObject.tag == "Character")
    {
      touchingChar = true;
    }
  }

  private void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.tag == "Character")
    {
      touchingChar = false;
    }
  }

  public void BreakBlock()
  {
    if (touchingChar)
    {
      Destroy(gameObject);
    }
  }

}