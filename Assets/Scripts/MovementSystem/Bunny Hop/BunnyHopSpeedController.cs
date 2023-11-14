using System;
using Cephei;
using UnityEngine;

namespace BlackBlock.MovementSystem
{
	public class BunnyHopSpeedController : IUpdatable, IMoveStateObservable
	{
		private BunnyHopMoveData _data;

		private Func<float> _velocityGetter;

		private MoveState _moveState;
		
		public bool IsActive { get; private set; }

		public float Speed { get; private set; }
		
		public BunnyHopSpeedController(BunnyHopMoveData data, Func<float> velocityGetter)
		{
			_data = data;
			_velocityGetter = velocityGetter;
		}

		public void UpdateWork(float delta)
		{
			if (IsActive == false)
				return;

			if (_moveState.IsAir() && _velocityGetter.Invoke() > _data.AccelerationThreshold)
				Lerp(_data.MaxSpeed, _data.AccelerationLerpFactor, delta);
			else
				Lerp(_data.DefaultSpeed, _data.DecelerationLerpFactor, delta);

			Debug.Log("Update bunny: " + Speed);
		}

		public void Activate()
		{
			IsActive = true;

			Speed = _data.DefaultSpeed;
		}

		public void Deactivate() => IsActive = false;

		public void Reset() => Speed = _data.DefaultSpeed;

		public void ChangeMoveState(MoveState moveState) => _moveState = moveState;

		private void Lerp(float targetSpeed, float lerpFactor, float delta) => 
			Speed = Mathf.MoveTowards(Speed, targetSpeed, lerpFactor * delta);
	}
}