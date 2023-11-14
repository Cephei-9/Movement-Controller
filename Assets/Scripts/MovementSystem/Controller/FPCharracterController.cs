using System;
using ECM.Controllers;
using UnityEngine;

namespace BlackBlock.MovementSystem
{
	public class FPCharracterController : BaseFirstPersonController
	{
		[Header("Custom Controller")]
		
		public bool IsMovingView;

		public MoveState StateView;

		public float SpeedView;

		public float VelocityView;
		
		
		private const float MovingThreshold = 0.2f;

		public event Action<Collider> GroundedEvent;

		public MoveState CurrentState
		{
			get
			{
				if (isFalling)
					return MoveState.Falling;
				if (isJumping)
					return MoveState.Jump;
				if (isCrouching && IsMoving)
					return MoveState.CrouchMoving;
				if (isCrouching)
					return MoveState.CrouchIdle;
				if (IsMoving)
					return MoveState.Moving;
				
				return MoveState.Idle;
			}
		}

		public bool IsMoving => movement.velocity.magnitude > MovingThreshold;

		public Vector3 GroundPoint => movement.groundPoint;

		public Collider GroundCollider => movement.groundHit.groundCollider;

		public Vector3 Velocity => movement.velocity;

		public float CurrentSpeed => movement.velocity.magnitude;

		public float MaxSpeed => forwardSpeed;
		
		public float SpeedPercent => Mathf.Min(CurrentSpeed / MaxSpeed, 1);

		private bool _previousGroundStatus;

		public override void Awake()
		{
			base.Awake();

			restoreVelocityOnResume = false;
		}

		public override void Update()
		{
			base.Update();

			IsMovingView = IsMoving;
			StateView = CurrentState;
			SpeedView = forwardSpeed;
			VelocityView = movement.velocity.magnitude;
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();
			
			if(_previousGroundStatus == false && isGrounded)
			{
				GroundedEvent?.Invoke(movement.groundCollider);
			}

			_previousGroundStatus = isGrounded;
		}

		public void CustomReset()
		{
			ResetCrouching();
		}

		public void UpdateInput(Vector2 moveVector, Vector2 rotateVector, bool isJump, bool isCrouch)
		{
			moveDirection = new Vector3(moveVector.x, 0, moveVector.y);

			jump = isJump && isJumping == false;
			
			crouch = isJumping == false && isCrouch;
			
			mouseLook.LookRotation(movement, cameraTransform, rotateVector, isPaused);
		}

		public void SetMovementSpeed(float speed)
		{
			forwardSpeed = speed;
			backwardSpeed = speed;
			strafeSpeed = speed;
		}

		public void SetLookData(float sensitivity)
		{
			mouseLook.lateralSensitivity = sensitivity;
			mouseLook.verticalSensitivity = sensitivity;
		}

		public void SetJumpHeight(float jumpHeight) => 
			baseJumpHeight = jumpHeight;

		public void AddForce(Vector3 force) => movement.ApplyForce(force, ForceMode.VelocityChange);

		public void SetPosition(Vector3 newPosition, Quaternion rotation)
		{
			movement.cachedRigidbody.MovePosition(newPosition);
			movement.rotation = rotation;
		}

		public void Pause() => pause = true;

		public void Unpause() => pause = false;

		private void ResetCrouching()
		{
			crouch = false;

			cameraPivotTransform.localScale = Vector3.one;
		}
	}
}