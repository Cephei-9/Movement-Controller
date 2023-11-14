using System;
using Cephei;
using UnityEngine;

namespace BlackBlock.MovementSystem
{
	public class DefaultSpeedController : IUpdatable, IMoveStateObservable
	{
		[Serializable]
		public class Data
		{
			public float LerpFactor = 1;
			
			[Header("Move Speed")]
		
			public float MoveSpeed = 5;

			public float CrouchSpeed = 3;

			[Header("Hittable")]
		
			public float MovingHittableSpeed = 3;
		
			public float CrouchHittableSpeed = 2;

			public float HittableDuration = 1;
		}

		public float Speed { get; private set; }
		
		private Data _data;

		private MoveState _currentState;
		private float _gunSpeedMultiply = 1;

		private float _hitTimer = Mathf.Infinity;
		private bool _isActive = true;

		public bool IsHit => _hitTimer < _data.HittableDuration;

		public DefaultSpeedController(Data data)
		{
			_data = data;

			_currentState = MoveState.Idle;
		}

		public void UpdateWork(float delta)
		{
			if (_isActive == false)
				return;

			Speed = Mathf.Lerp(Speed, GetTargetSpeed(), _data.LerpFactor);

			_hitTimer += delta;
		}

		public void Reset()
		{
			_hitTimer = Mathf.Infinity;
			Speed = _data.MoveSpeed;
			_currentState = MoveState.Idle;
		}

		public void ChangeMoveState(MoveState moveState) => 
			_currentState = moveState;

		public void Activate() => _isActive = true;

		public void Deactivate() => _isActive = false;

		public void Hit() => _hitTimer = 0;

		private float GetTargetSpeed() => 
			GetSpeedByState() * _gunSpeedMultiply;

		private float GetSpeedByState()
		{
			if (IsHit && _currentState == MoveState.Moving)
				return _data.MovingHittableSpeed;

			if (IsHit && _currentState.IsCrouch())
				return _data.CrouchHittableSpeed;

			if (_currentState.IsCrouch() == false)
				return _data.MoveSpeed;

			if (_currentState.IsCrouch())
				return _data.CrouchSpeed;

			Debug.Log("Not correctly situation");

			return 0;
		}

		public void SetGunSpeedMultiply(float speedMultiply) => 
			_gunSpeedMultiply = speedMultiply;
	}
}