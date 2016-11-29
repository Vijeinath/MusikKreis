using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Behaviors;
using System;

public class TouchCode : MonoBehaviour {

	private TransformGesture gesture;
	private Transform rb2d;
	private bool isActive = false;
	public float attractionStrength = 10;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Transform> ();
	}


	void Update () {
		if (isActive && !float.IsNaN(gesture.ScreenPosition.x)) {
			// get the positions of this object and the target
			Vector2 targetPosition = Camera.main.ScreenToWorldPoint (new Vector2 (gesture.ScreenPosition.x, gesture.ScreenPosition.y));
			Vector2 myPosition = transform.position;

			// work out direction and distance
			Vector2 direction = (targetPosition - myPosition).normalized;
			float distance = Vector3.Magnitude (targetPosition - myPosition);

			// apply square root to distance if specified to do so in the inspector
			//if (useSqrtOfDistance) distance = Mathf.Sqrt(distance);

			Vector3 resultingForceAmount = attractionStrength * direction * distance;

			// then finally add the force to the rigidbody
			rb2d.position =  targetPosition;
		}
	}

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

	void HandleTransformStarted (object sender, EventArgs e)
	{
		isActive = true;		
	}

	void HandleTransformCompleted (object sender, EventArgs e)
	{
		isActive = false;	
	}
}
