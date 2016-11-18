using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BadStar : Star {

	public override void LoadAudioClips(List<AudioClip> listOfSounds){
		listOfSounds.Add (Resources.Load("Sound/fail01 (cut)") as AudioClip);
		listOfSounds.Add (Resources.Load("Sound/fail02 (cut)") as AudioClip);
	}
}
