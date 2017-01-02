using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Behaviors;
using System;
using System.Collections.Generic;

public class Planet: MonoBehaviour {
	//Public attributes
	public float RotationSpeed = 20f;
	public bool galaxyAppeared = false;
	public bool hitByStar = false;
	public int rounds = 0;

	//Private attributes
	private float degree = 90f;
	private float degreeDelta = 0.25f;
	private float radius = 2.5f;
	private TransformGesture gesture;
	private bool touchIsHappening = false;
	private Sprite[] planetSprites;

	private void OnEnable()
	{
		gesture = GetComponent<TransformGesture> ();
		gesture.TransformStarted += HandleTransformStarted;
		gesture.TransformCompleted += HandleTransformCompleted;
	}

	private void OnDisable()
	{
		gesture.TransformStarted -= HandleTransformStarted;
		gesture.TransformCompleted -= HandleTransformCompleted;
	}

	void HandleTransformStarted(object sender, EventArgs e)
	{
		touchIsHappening = true;
		ChangePlanet (this.GetComponent<SpriteRenderer>().sprite.name);
	}

	void HandleTransformCompleted (object sender, EventArgs e)
	{	
		touchIsHappening = false;
	}
		
	// Use this for initialization
	void Start () {
		planetSprites = new Sprite[4];
		planetSprites[0] = Resources.Load<Sprite> ("Sprites/planet_1");
		planetSprites[1] = Resources.Load<Sprite> ("Sprites/planet_2");
		planetSprites[2] = Resources.Load<Sprite> ("Sprites/planet_3");
		planetSprites[3] = Resources.Load<Sprite> ("Sprites/planet_4");
	}
	
	// Update is called once per frame
	void Update () {
		if (galaxyAppeared) {
			transform.Rotate(0, 0, Time.deltaTime*RotationSpeed);
			Orbit ();
		}	
	}

	void Fixed(){
		if (touchIsHappening) {
			Vector2 targetPosition = Camera.main.ScreenToWorldPoint (new Vector2 (gesture.ScreenPosition.x, gesture.ScreenPosition.y));
			transform.position = targetPosition;
		}
	}

	void Orbit(){
		this.transform.position = new Vector2 (Mathf.Cos (degree * Mathf.PI / 180)*radius,Mathf.Sin (degree * Mathf.PI / 180)*radius);
		if ((degree  + degreeDelta) == 90) {
			rounds++;
		}
		degree = (degree + degreeDelta) %360;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Star") {
			hitByStar = true;
		}
	}

	public bool isTouched(){
		return touchIsHappening;
	}

	private void ChangePlanet(string fileName){
		int number = (Int32.Parse(fileName.Split ('_') [1])) % 4;
		this.GetComponent<SpriteRenderer> ().sprite = planetSprites [number];
	}

}