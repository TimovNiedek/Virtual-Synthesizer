using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* *
 * Note script attached to the key objects
 * 
 * Authors:
 * Timo van Niedek
 * Daniel Roeven
 * */

public class Note : MonoBehaviour {
	// Time between one note can be played in seconds
	public double noteCooldown = 0;
	public bool isWhite;

	private double lastTimePressed = 0;
	private int noteNumber;
	private string name;
	private GameObject camera;
	private bool isPlaying = false;
	private bool turn = false;

	// Use this for initialization
	void Start () {
		// Set the max angular velocity to remove unintentional rapid movement of a key
		rigidbody.maxAngularVelocity = 1.0f;

		camera = GameObject.Find("Main Camera");

		// Set the center of mass
		Vector3 com = new Vector3(0,0,0);
		this.rigidbody.centerOfMass = com;

		// Retrieve the name of the note object
		// This is used to set the noteNumber
		name = this.gameObject.name;
		char[] nameArray = name.ToCharArray ();
		int octave = (int)char.GetNumericValue(nameArray [nameArray.Length - 1]);
		
		string noteName = name.Remove(name.Length - 1);

		switch (noteName) {
		case "C":
			noteNumber = 12 * octave + 0;
			break;
		case "Cis":
			noteNumber = 12 * octave + 1;
			break;
		case "D":
			noteNumber = 12 * octave + 2;
			break;
		case "Dis":
			noteNumber = 12 * octave + 3;
			break;
		case "E":
			noteNumber = 12 * octave + 4;
			break;
		case "F":
			noteNumber = 12 * octave + 5;
			break;
		case "Fis":
			noteNumber = 12 * octave + 6;
			break;
		case "G":
			noteNumber = 12 * octave + 7;
			break;
		case "Gis":
			noteNumber = 12 * octave + 8;
			break;
		case "A":
			noteNumber = 12 * octave + 9;
			break;
		case "Ais":
			noteNumber = 12 * octave + 10;
			break;
		case "B":
			noteNumber = 12 * octave + 11;
			break;
		default:
			noteNumber = -48;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!turn) {
			Vector3 rotation = gameObject.transform.localEulerAngles;
			float angle = rotation.z;

			// Clamp the notes angle to 0 and 5 degrees
			if (angle > 180.0f) {
				angle = 0;
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
			} else if (angle > 5.01f) {
				angle = 5;
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
			}

			// If the not is not touched, rotate it back to 0 degrees
			if (!isPlaying && angle >= 0.00001f) {
				angle -= 0.2f;
			}
			gameObject.transform.localEulerAngles = new Vector3 (rotation.x, rotation.y, angle);
		}
	}

	// Resets the angle of the note
	// Called when hitting the reset button (T)
	public void resetAngle(){
		Vector3 angles = gameObject.transform.localEulerAngles;
		angles.z = 0.0f;
		gameObject.transform.localEulerAngles = angles;
	}

	// This function is called when a fingertip enters collision with this note
	private bool SetFingerDown(FingerTip tip)
	{
		if (!tip.isTouchingNote && !isPlaying) {
			tip.isTouchingNote = true;
			tip.noteTouched = noteNumber;
			isPlaying = true;
			camera.SendMessage ("OnFingerDown", tip.noteTouched);
			return true;
		} else
			return false;
	}

	// This function is called when a fingertip exits collision with this note
	private bool SetFingerUp(FingerTip tip)
	{
		if (tip.noteTouched == noteNumber) {
			camera.SendMessage ("OnFingerUp", tip.noteTouched);
			tip.noteTouched = -1;
			tip.isTouchingNote = false;
			isPlaying = false;
			return true;
		} else
			return false;
	}

	// Returns the FingerTip object (the sphere) for a collider
	FingerTip GetFingerTip (Collider col){
		if (col && col.gameObject.name == "FingerTip")
			return col.gameObject.GetComponent<FingerTip> ();
		else 
			return null;
	}

	void OnCollisionEnter(Collision col){
		FingerTip fingerTip = GetFingerTip (col.collider);

		if (fingerTip != null && lastTimePressed + noteCooldown <= Time.time)
		{
			lastTimePressed = Time.time;
			SetFingerDown (fingerTip);
			PianoFinger pianoFinger = fingerTip.getParentFinger();
			pianoFinger.setTouchingNote(true, isWhite, this);
		}
	}

	void OnCollisionExit(Collision col){
		FingerTip fingerTip = GetFingerTip (col.collider);

		if (fingerTip != null)
		{
			SetFingerUp (fingerTip);
			PianoFinger pianoFinger = fingerTip.getParentFinger();
			pianoFinger.setTouchingNote(false, isWhite, null);
		}
	}

	// Removes the limits of the notes angle for anarchy mode
	public void doTurn(){
		if (turn)
			turn = false;
		else
			turn = true;
	}
}
