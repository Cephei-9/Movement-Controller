using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTargetRotator : MonoBehaviour
{
	//Топовая штука!
	
	
	public Transform Target;
	[SerializeField] private int speed;

	private Rigidbody _rb => GetComponent<Rigidbody>();

	private void FixedUpdate()
	{
		var i = speed * Time.fixedDeltaTime;
		AddXYRotation(i, Time.fixedDeltaTime);
		AddRotationToRight(i, Time.fixedDeltaTime);
	}

	private void AddRotationToRight(float speed, float fixedDeltaTime)
	{
		Vector3 projectTargetRight = Vector3.ProjectOnPlane(Target.right, _rb.transform.forward);
		float angle = Vector3.SignedAngle(projectTargetRight, _rb.transform.right, _rb.transform.forward);
			
		float radDelta = -1 * angle * speed * Mathf.Deg2Rad;

		_rb.angularVelocity += _rb.transform.forward * radDelta / fixedDeltaTime;
	}

	private void AddXYRotation(float speed, float fixedDelta)
	{
		Vector3 cross = Vector3.Cross(_rb.transform.forward, Target.forward);
		float angle = Vector3.Angle(Target.forward, _rb.transform.forward);
			
		float radDelta = angle * speed * Mathf.Deg2Rad;

		_rb.angularVelocity = cross.normalized * radDelta / fixedDelta;
	}
}
