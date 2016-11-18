using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Behaviors;
using System;

public class Cube : MonoBehaviour {
	private TransformGesture gesture;
	private bool touchIsHappening = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		print ("ScreenPosition x: "+ gesture.ScreenPosition.x);
		if (touchIsHappening) {
			print ("Cube touching");
			Vector2 targetPosition = Camera.main.ScreenToWorldPoint (new Vector2 (gesture.ScreenPosition.x, gesture.ScreenPosition.y));
			transform.position = targetPosition;
		}
	}

	void Update(){
		//print ("ScreenPosition x: "+ gesture.ScreenPosition.x);
	}

	private void OnEnable()
	{
		gesture = GetComponent<TransformGesture> ();
		gesture.TransformStarted += HandleTransformStarted;
		gesture.TransformCompleted += HandleTransformCompleted;
	}

	private void OnDisable()
	{
		gesture = GetComponent<TransformGesture> ();
		gesture.TransformStarted -= HandleTransformStarted;
		gesture.TransformCompleted -= HandleTransformCompleted;
	}

	void HandleTransformStarted (object sender, EventArgs e)
	{
		touchIsHappening = true;		
	}

	void HandleTransformCompleted (object sender, EventArgs e)
	{
		touchIsHappening = false;	
	}
}
