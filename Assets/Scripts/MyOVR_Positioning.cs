using UnityEngine;
using System.Collections;

/* *
 * This abstract class is used to manually position the Oculus Rift camera
 * Per default, no forward movement is supported in the Oculus Rift libraries
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

[RequireComponent(typeof(OVRCameraRig))]
public abstract class MyOVR_Positioning : MonoBehaviour
{
	private OVRCameraRig cameraRig;
	public Transform root;
	public void OnEnable()
	{
		if (cameraRig == null)
		{
			cameraRig = GetComponent<OVRCameraRig>() as OVRCameraRig;
		}
		if (cameraRig != null)
		{
			cameraRig.onPositioningUpdate += PositionOculusInScene;
		}
		
	}
	
	public void OnDisable()
	{
		if (cameraRig != null)
		{
			cameraRig.onPositioningUpdate -= PositionOculusInScene;
		}
		
	}
	
	
	// Method to inherit to be able to reposition the camera at will.
	protected abstract void PositionOculusInScene(OVRPose leftPose, OVRPose rightPose, Transform left, Transform center, Transform right);
	
	// Method to apply the custom positioning
	protected void ApplyPositioningAt(Transform leftOVREye, Transform OVRcenter, Transform rightOVREye, ref Vector3 centerPosition, ref Vector3 toGoToTheRight, ref Vector3 toGoToTheLeft, ref Quaternion orientationLeft, ref Quaternion orientationRight)
	{
		leftOVREye.localPosition = centerPosition + toGoToTheLeft;
		OVRcenter.localPosition = centerPosition;
		rightOVREye.localPosition = centerPosition + toGoToTheRight;
		
		leftOVREye.localRotation = orientationLeft;
		OVRcenter.localRotation = orientationLeft;
		rightOVREye.localRotation = orientationRight;
	}
}