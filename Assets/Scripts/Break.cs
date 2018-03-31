using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour {

  private bool touchingChar;

  private void Update()
  {
    if (Input.GetKeyDown("l"))
    {
      BreakBlock();
    }
  }

  private void OnCollisionStay2D(Collision2D other)
  {
    if (other.gameObject.tag == "Reflection")
    {
      touchingChar = true;
    }
  }

  private void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.tag == "Reflection")
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