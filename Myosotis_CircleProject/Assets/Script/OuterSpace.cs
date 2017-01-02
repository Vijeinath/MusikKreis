using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Behaviors;
using System;

public class OuterSpace : MonoBehaviour {
	private PressGesture gesture;
	private LongPressGesture lPressG;
	private bool isPressed = false;
	private int background = 1;
	private int numberOfBackgrounds = 5;
	//private Vector2 pressPosition;
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	private void OnEnable ()
	{
		gesture = GetComponent<PressGesture> ();
		gesture.Pressed += PressHandler;

		lPressG = GetComponent<LongPressGesture> ();
		lPressG.LongPressed += LongPressHandler;
	}


	private void OnDisable ()
	{
		gesture.Pressed -= PressHandler;

		lPressG.LongPressed -= LongPressHandler;
	}


	private void PressHandler (object sender, EventArgs e)
	{
		isPressed = true;
		print ("pressed");
		//pressPos = Camera.main.ScreenToWorldPoint (new Vector2 (pressG.ScreenPosition.x, pressG.ScreenPosition.y));
	}

	private void LongPressHandler(object sender, EventArgs e){
		print ("LongPressed");
		ChangeBackground ();
	}
		
	public bool IsPressed(){
		return isPressed;
	}

	private void ChangeBackground(){
		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Backgrounds/OuterSpace0"+ background++%numberOfBackgrounds);
		// transform.localScale += new Vector3(0.52F, 0.42F, 0F);
	}
}
