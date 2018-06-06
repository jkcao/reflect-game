using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class OnOff : MonoBehaviour {
	public Sprite onSprite;
	public Sprite onHighlightSprite;
	public Sprite offSprite;
	public Sprite offHighlightSprite;
	public SpriteRenderer switchVisual;
	public ParticleSystem dust;
	public bool particlesOnAtStart;
	
	protected bool turnOn;
	protected bool cCol;
	protected bool rCol;
	protected AudioSource sound;

	// Use this for initialization
	void Start () {
		turnOn = false;
		cCol = false;
		rCol = false;
		switchVisual = GetComponent<SpriteRenderer>();
		sound = GetComponent<AudioSource> ();
		begin ();
		if (dust != null) {
			dust.Pause ();
			dust.Clear ();
		}
	}

	protected void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Character") {
			cCol = true;
		}
		if (col.gameObject.tag == "Reflection") {
			rCol = true;
		}
	}

	// Checks if the player is in-contact with an OnOff object or not.
	protected void OnTriggerStay2D (Collider2D col){
		if (col.gameObject.tag == "Character") {
			cCol = true;
		}
		if (col.gameObject.tag == "Reflection") {
			rCol = true;
		}
	}
		
	protected void OnTriggerExit2D (Collider2D col){
		if (col.gameObject.tag == "Character") {
			cCol = false;
		}
		if (col.gameObject.tag == "Reflection") {
			rCol = false;
		}
		if (this.GetComponent<CircleCollider2D> ()) {
			if (turnOn) {
				switchVisual.sprite = onSprite;
			} else {
				switchVisual.sprite = offSprite;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		// If the player is on the object and toggles it, turn the object On/Off.
		if((cCol && (Input.GetKeyDown("j") || Input.GetMouseButtonDown(0))) || (rCol && (Input.GetKeyDown("l") || Input.GetMouseButtonDown(1)))) {
			setOnBool(!turnOn);
		}
	}
		
	protected void changeVisual(bool turnOn) {
		if(turnOn) {
			switchVisual.sprite = offSprite;
			if (dust != null) {
				if (!particlesOnAtStart) {
					dust.Play ();
				} else {
					dust.Pause ();
					dust.Clear ();
				}
			}
		} else {
			switchVisual.sprite = onSprite;
			if (dust != null) {
				if (particlesOnAtStart) {
					dust.Play ();
				} else {
					dust.Pause ();
					dust.Clear ();
				}
			}
		}
	}

	// Function to pass the on/off bool to other scripts.
	public bool getOnBool(){
		return turnOn;
	}

	IEnumerator switchOn(bool x) {
		yield return new WaitForSeconds(0.15f);
		turnOn = x;
		changeVisual (turnOn);
		sound.Play ();
		action (turnOn);
	}

	// Function to set the on/off bool from other scripts and perform the necessary changes.
	public void setOnBool(bool x){
		if (turnOn != x) {
			StartCoroutine (switchOn(x));
		}
	}

	// These are abstract methods so each type of object can perform the action it's supposed to,
	// and initialize in the way they need to.
	protected abstract void action (bool turnOn);
	protected abstract void begin ();
}
