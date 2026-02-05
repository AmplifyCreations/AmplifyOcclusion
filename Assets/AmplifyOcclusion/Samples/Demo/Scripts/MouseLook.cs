using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 3F;
	public float sensitivityY = 3F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -80F;
	public float maximumY = 80F;

	public float forwardSpeedScale = 0.03f;
	public float strafeSpeedScale = 0.03f;

	float rotationX = 0F;
	float rotationY = 0F;

	bool look = false;

	Quaternion originalRotation;

	void Update ()
	{
		if ( GUIUtility.hotControl != 0 )
			return;

		if ( Input.GetMouseButtonDown( 0 ) )
			look = true;
		if ( Input.GetMouseButtonUp( 0 ) )
			look = false;

		if ( look )
		{
			if ( axes == RotationAxes.MouseXAndY )
			{
				// Read the mouse input axis
				rotationX += Input.GetAxis( "Mouse X" ) * sensitivityX;
				rotationY += Input.GetAxis( "Mouse Y" ) * sensitivityY;

				rotationX = ClampAngle( rotationX, minimumX, maximumX );
				rotationY = ClampAngle( rotationY, minimumY, maximumY );

				Quaternion xQuaternion = Quaternion.AngleAxis( rotationX, Vector3.up );
				Quaternion yQuaternion = Quaternion.AngleAxis( rotationY, Vector3.left );

				transform.localRotation = originalRotation * xQuaternion * yQuaternion;
			}
			else if ( axes == RotationAxes.MouseX )
			{
				rotationX += Input.GetAxis( "Mouse X" ) * sensitivityX;
				rotationX = ClampAngle( rotationX, minimumX, maximumX );

				Quaternion xQuaternion = Quaternion.AngleAxis( rotationX, Vector3.up );
				transform.localRotation = originalRotation * xQuaternion;
			}
			else
			{
				rotationY += Input.GetAxis( "Mouse Y" ) * sensitivityY;
				rotationY = ClampAngle( rotationY, minimumY, maximumY );

				Quaternion yQuaternion = Quaternion.AngleAxis( rotationY, Vector3.left );
				transform.localRotation = originalRotation * yQuaternion;
			}
		}

		// We are grounded, so recalculate move direction directly from axes
		Vector3 moveDir = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );
		moveDir = transform.TransformDirection( moveDir );
		moveDir *= 10.0f;

		float scale = ( Input.GetKey( KeyCode.LeftShift ) || Input.GetKey( KeyCode.RightShift ) ) ? 150.0f : 50.0f;
		float forwardSpeed = Input.GetAxis( "Vertical" ) * forwardSpeedScale * scale;
		float strafeSpeed = Input.GetAxis( "Horizontal" ) * strafeSpeedScale * scale;
		if ( forwardSpeed != 0.0f )
		{
			transform.position += transform.forward * forwardSpeed;
		}
		if ( strafeSpeed != 0.0f )
		{
			transform.position += transform.right * strafeSpeed;
		}
	}

	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
		originalRotation = transform.localRotation;
		look = false;
	}

	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}
