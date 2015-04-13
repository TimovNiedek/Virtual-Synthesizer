using UnityEngine;
using System.Collections;

/* * 
 * Implementation of the MyOVR_Positioning abstract class
 * Handles forward and backwards movement of the Oculus Rift headset
 * 
 * Based on code from a question on the UnityAnswers forum:
 * http://answers.unity3d.com/questions/858277/how-can-i-fixe-a-head-limitation-on-the-oculus-rif.html
 * 
 * Modified by Daniel Roeven and Timo van Niedek
 * */

/* --------------------------BEER-WARE LICENSE----------------
 * Strée Eloi (PrIMD42@gmail.com) wrote this file.
 * ======  =======   PERSONAL USE   ====== ======
 * As long as you retain this notice you
 * can do whatever you want with this code. If you think
 * this stuff is worth it, you could buy me a beer in return, 
 * ====== ========  COMMERCIAL USE   ====== ====
 * Please refer to personal use license.
 * Plus, send me an email with :
 * Name, Project name,  grade (1-10) and your opinions
 *_______________   LINK   _____________________
 * Donate a beer: http://www.primd.be/donate/ 
 * Contact: http://www.primd.be/
 * ----------------------------------------------------------------------------
 */

public class MyOVR_MoveHead : MyOVR_Positioning {
	public float movementSpeed;

	protected override void PositionOculusInScene(OVRPose leftPose, OVRPose rightPose, Transform left, Transform center, Transform right)
	{
		if (left == null || center == null || right == null) return;
		if (root == null)
		{
			Debug.Log("No root defined", this.gameObject);
		}

		Vector3 centerPosition = (leftPose.position + rightPose.position) / 2f;
		Vector3 centerToRight = rightPose.position - centerPosition;
		Vector3 centerToLeft = leftPose.position - centerPosition;

		centerPosition *= movementSpeed;

		ApplyPositioningAt(left, center, right, ref centerPosition, ref centerToRight, ref centerToLeft, ref leftPose.orientation, ref rightPose.orientation);
		
	}
}
