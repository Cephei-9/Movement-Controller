using System;
using System.Collections.Generic;
using Cephei;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Input = UnityEngine.Input;

namespace BlackBlock.MovementSystem
{
	public class MovementSystem : IUpdatable
	{
		public enum WorkMode
		{
			Default,
			BunnyHop
		}

		#region Fields

		public event Action<MoveState> ChangeMoveStateEvent;

		public event Action<float> StepEvent;

		public event Action<float> FallEvent;

		private List<IUpdatable> _updatables;
		private List<IMoveStateObservable> _moveStateObservables;
		
		private DefaultSpeedController _defaultSpeedController;
		private BunnyHopPlatformTrigger _bunnyHopTrigger;

		private IMoveInput _input;

		private MovementSystemData _movementSystemData;
		private GroundRaycaster _groundRaycaster;
		
		private BunnyHopSpeedController _bunnyHopSpeedController;
		private FootStepHandler _footStepHandler;
		private MovementSound _movementSound;
		
		private FPCharracterController _controller;
		private FallHandler _fallHandler;
		private readonly ForceByShoot _hitForce;

		#endregion

		#region Properties 

		public MoveState CurrentMoveState { get; private set; }

		public WorkMode CurrentWorkMode { get; private set; }
		
		public Vector3 Velocity => _controller.Velocity;

		public float CurrentSpeed => _controller.CurrentSpeed;

		public float MaxSpeed => _controller.MaxSpeed;
		
		public float SpeedPercent => Mathf.Min(CurrentSpeed / MaxSpeed, 1);

		public float StepTime => _footStepHandler.StepTime;

		#endregion
		
		#region Initialization

		public MovementSystem(FPCharracterController controller, TestMoveInput moveInput,
			MovementSystemData movementSystemData,
			BunnyHopMoveData bunnyHopData, AudioSource audioSource, Transform raycasterPoint, ICoroutineRunner runner)
		{
			_input = moveInput;
			_movementSystemData = movementSystemData;
			_controller = controller;
			
			_groundRaycaster = new GroundRaycaster(raycasterPoint, Vector3.down, () => _controller.GroundPoint);
			_defaultSpeedController = new DefaultSpeedController(movementSystemData.SpeedData);
			_hitForce = new ForceByShoot(controller, movementSystemData.HitForceData, runner);

			InitFallHandler(controller);
			InitFootStepHandler(movementSystemData);
			InitMovementSound(movementSystemData, audioSource);
			InitBunnyHop(bunnyHopData);

			CreateUpdateblesList();
			CreateMoveObservablesList();
		}

		private void InitFallHandler(FPCharracterController controller)
		{
			_fallHandler = new FallHandler(controller.transform);
			_fallHandler.FallEvent += (height) => FallEvent?.Invoke(height);
		}

		private void CreateMoveObservablesList()
		{
			_moveStateObservables = new List<IMoveStateObservable>
			{
				_defaultSpeedController,
				_footStepHandler,
				_movementSound,
				_bunnyHopSpeedController,
				_fallHandler
			};
		}

		private void CreateUpdateblesList()
		{
			_updatables = new List<IUpdatable>
			{
				_defaultSpeedController,
				_groundRaycaster,
				_bunnyHopSpeedController,
				_footStepHandler,
				_fallHandler
			};
		}

		private void InitBunnyHop(BunnyHopMoveData bunnyHopData)
		{
			_bunnyHopSpeedController = new BunnyHopSpeedController(bunnyHopData,
				() => _controller.movement.velocity.magnitude);
			_bunnyHopTrigger = new BunnyHopPlatformTrigger(() => _controller.GroundCollider);
			_controller.GroundedEvent += _bunnyHopTrigger.OnGrounded;
		}

		private void InitMovementSound(MovementSystemData movementSystemData, AudioSource audioSource)
		{
			_movementSound = new MovementSound(movementSystemData.SoundData, audioSource);
			_footStepHandler.StepEvent += f => _movementSound.PlayStep(SpeedPercent);
		}

		private void InitFootStepHandler(MovementSystemData movementSystemData)
		{
			_footStepHandler = new FootStepHandler(movementSystemData.FootStepData, () => _controller.SpeedPercent);
			_footStepHandler.StepEvent += (stepSign) => StepEvent?.Invoke(stepSign);
		}
		
		#endregion

		#region Public Methods

		public void UpdateWork(float delta)
		{
			Debug.Log("Update movements");
			
			UpdateMoveState();
			UpdateUpdatables(delta);
			
			HandleInput();
			UpdateControllerData();
		}

		public void ActivateBunnyHopMode()
		{
			CurrentWorkMode = WorkMode.BunnyHop;
			
			_bunnyHopTrigger.Activate();
			_bunnyHopSpeedController.Activate();
			
			_defaultSpeedController.Deactivate();
		}

		public void DeactivateBunnyHopMode()
		{
			CurrentWorkMode = WorkMode.Default;
			
			_bunnyHopTrigger.Deactivate();
			_bunnyHopSpeedController.Deactivate();
			
			_defaultSpeedController.Activate();
		}
		
		public void FreezeMovement() => _controller.Pause();

		public void UnFreezeMovement() => _controller.Unpause();

		public void Reset()
		{
			_controller.CustomReset();

			_defaultSpeedController.Reset();
			_hitForce.Reset();

			_bunnyHopSpeedController.Reset();
			_bunnyHopTrigger.Reset();
			
			_footStepHandler.Reset();
			_movementSound.Reset();
		}

		public void SetPosition(Vector3 newPosition, Quaternion rotation) => 
			_controller.SetPosition(newPosition, rotation);

		public void Hit(ShootForce forceType, Vector3 direction)
		{
			_defaultSpeedController.Hit();
			
			if(direction != default)
				_hitForce.Hit(forceType, direction);
		}

		public void SetGunSpeedMultiply(float speedMultiply)
		{
			_defaultSpeedController.SetGunSpeedMultiply(speedMultiply);
		}

		public void SetInput(IMoveInput input) => _input = input;
		
		#endregion

		#region Private Methods

		private void UpdateControllerData()
		{
			Debug.Log("Movement Speed: " + GetSpeed()); //
			
			_controller.SetMovementSpeed(GetSpeed());
			_controller.SetLookData(_movementSystemData.Sensitivity);
			_controller.SetJumpHeight(_movementSystemData.JumpHeight);
		}

		private float GetSpeed() => 
			CurrentWorkMode == WorkMode.BunnyHop ? _bunnyHopSpeedController.Speed : _defaultSpeedController.Speed;

		private void HandleInput()
		{
			bool isJump = _input.IsJump || _bunnyHopTrigger.ActivateModeStatus;
			_controller.UpdateInput(_input.MoveVector, _input.RotateVector, isJump, _input.IsCrouch);
		}

		private void UpdateUpdatables(float delta)
		{
			foreach (IUpdatable updatable in _updatables)
			{
				updatable.UpdateWork(delta);
			}
		}

		private void UpdateMoveState()
		{
			if (CurrentMoveState == _controller.CurrentState)
				return;

			CurrentMoveState = _controller.CurrentState;
			
			foreach (IMoveStateObservable moveStateObservable in _moveStateObservables)
			{
				moveStateObservable.ChangeMoveState(CurrentMoveState);
			}

			ChangeMoveStateEvent?.Invoke(CurrentMoveState);
		}
		
		#endregion
	}
}