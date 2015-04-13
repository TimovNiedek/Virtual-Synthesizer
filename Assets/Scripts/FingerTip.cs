using UnityEngine;
using System.Collections;

/* *
 * Script attached to the FingerTip spheres
 * These spheres are generated in the PianoFinger class
 * which can be found in LeapMotion/Scripts/Hands
 * 
 * Authors:
 * Daniel Roeven
 * Timo van Niedek
 * */

public class FingerTip : MonoBehaviour {
	public bool isTouchingNote = false;
	public int noteTouched = -1;

	private PianoFinger parentFinger = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setParentFinger(PianoFinger finger){
		parentFinger = finger;
	}

	public PianoFinger getParentFinger(){
		return parentFinger;
	}


}
