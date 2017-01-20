using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Behaviors;
using System;

public class OuterSpace : MonoBehaviour {
	private LongPressGesture lPressG;
	private bool isPressed = false;
	private int background = 1;
	private int numberOfBackgrounds = 2;
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	private void OnEnable ()
	{
		lPressG = GetComponent<LongPressGesture> ();
		lPressG.LongPressed += LongPressHandler;
	}

	private void OnDisable ()
	{
		lPressG.LongPressed -= LongPressHandler;
	}
		
	private void LongPressHandler(object sender, EventArgs e){
		ChangeBackground ();
	}
		
	public bool IsPressed(){
		return isPressed;
	}

	private void ChangeBackground(){
		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/Backgrounds/OuterSpace0"+ background++%numberOfBackgrounds);
	}
}
