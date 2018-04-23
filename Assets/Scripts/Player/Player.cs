using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

abstract public class Player : MonoBehaviour
{

  public float speed;
  public float jumpHeight;
  public float jumpBox;
  public GameObject blockPrefab;
  public GameObject restrictedPlat;
  public ParticleSystem dust;

  protected Rigidbody2D rigidBody;
  protected bool hitLeft;
  protected bool hitRight;

  protected bool isGrounded;
  protected int dir;
  protected float groundPosition;
  protected float halfHeight;
  protected Block restrict;
  public Vector2 respawn;

  public Player mirror;

  // Use this for initialization
  void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
    if (restrictedPlat != null) restrict = restrictedPlat.GetComponent<Block>();
    isGrounded = true;
    respawn = transform.position;
    halfHeight = this.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    groundPosition = this.transform.position.y - halfHeight;
    StopSparkles();
  }

  protected void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.tag == "Death")
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    if (col.transform.tag == "MovingPlatform")
    {
      transform.parent = col.transform;
    }
  }

  protected void OnCollisionExit2D(Collision2D col)
  {
    if (col.transform.tag == "MovingPlatform")
    {
      transform.parent = null;
      groundPosition = transform.position.y - halfHeight;
    }
  }

  // Update is called once per frame
  void Update()
  {
    // Reset level by player.
	if (Input.GetKeyDown("return")) 	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    // Check if player is grounded, via three points: its center and its two corners.
    Vector2 leftCheck = new Vector2(transform.position.x - jumpBox, transform.position.y);
    Vector2 rightCheck = new Vector2(transform.position.x + jumpBox, transform.position.y);
    Vector2 midCheck = transform.position;
    Vector2 leftEnd = leftCheck;
	leftEnd.y -= (halfHeight + 0.1f);
    Vector2 rightEnd = rightCheck;
	rightEnd.y -=  (halfHeight + 0.1f);
    Vector2 midEnd = midCheck;
	midEnd.y -=  (halfHeight + 0.1f);

    //Linecast to check for potential blocks in front 
    Vector2 leftCast = leftCheck;
	leftCast.x += 5f;
    Vector2 rightCast = rightCheck;
	rightCast.x -= 5f;

    if (Physics2D.Linecast(leftCheck, leftCast, 1 << LayerMask.NameToLayer("Block")))
    {
      hitLeft = true;
    }
    else
    {
      hitLeft = false;

    }

    if (Physics2D.Linecast(rightCheck, rightCast, 1 << LayerMask.NameToLayer("Block")))
    {
      hitRight = true;

    }
    else
    {
      hitRight = false;

    }

    if ((Physics2D.Linecast(leftCheck, leftEnd, 1 << LayerMask.NameToLayer("Ground"))
      || Physics2D.Linecast(rightCheck, rightEnd, 1 << LayerMask.NameToLayer("Ground"))
      || Physics2D.Linecast(midCheck, midEnd, 1 << LayerMask.NameToLayer("Ground"))
	  || Physics2D.Linecast(leftCheck, leftEnd, 1 << LayerMask.NameToLayer("Block"))
	  || Physics2D.Linecast(rightCheck, rightEnd, 1 << LayerMask.NameToLayer("Block"))
			|| Physics2D.Linecast(midCheck, midEnd, 1 << LayerMask.NameToLayer("Block"))) && !Input.GetKeyDown("k"))
    {
      isGrounded = true;
    }
    else
    {
      isGrounded = false;
      groundPosition = transform.position.y - halfHeight;
      // Calculate to keep for allocating mirror platform dynamically.
    }

    float horizontal = Input.GetAxis("Horizontal");

    // Check player direction
    if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
    {
      dir= -1;
    }
    if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
    {
      dir = 1;
    }
    // Checking for reflection special ability.
		if (Input.GetKeyDown("j") || Input.GetMouseButtonDown(0))
    {
      SpecialAbility(0);
    }
 
    // Call Movement function.
		Movement(horizontal, Input.GetKeyDown("w") || Input.GetKeyDown("space"));
  }

  public void StartSparkles ()
  {
    if (!dust.isPlaying)
    {
      dust.Play(true);
    }
  }

  public void StopSparkles()
  {
    if (dust.isPlaying)
    {
      dust.Pause();
      dust.Clear();
    }
  }

  // Returns the y-position of the ground the player was last standing on.
  public float getGroundPosition()
  {
    return groundPosition;
  }

  protected abstract void Movement(float horizontal, bool jump);
  protected abstract void SpecialAbility(int condition);
}
