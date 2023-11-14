using System;
using Cephei;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace BlackBlock.MovementSystem
{
	public class FootStepHandler : IUpdatable, IMoveStateObservable
	{
		[Serializable]
		public class Data
		{
			[FormerlySerializedAs("StepSpeed")]
			public float MovingStepSpeed = 1;
			public float CrouchStepSpeed = 1;
		}

		public event Action<float> StepEvent;
		
		public float StepTime { get; private set; }

		private MoveState _currentState;
		private float _stepSign;

		private Data _data;
		private Func<float> _velocityPercentGetter;

		public FootStepHandler(Data data, Func<float> velocityPercentGetter)
		{
			_data = data;
			_velocityPercentGetter = velocityPercentGetter;
			
			_stepSign = GetRandomSign();
		}

		public void UpdateWork(float delta)
		{
			if (_currentState.IsMovingOnGround() == false)
				return;

			UpdateStepTime(delta);

			CheckStepEnd();
		}

		public void ChangeMoveState(MoveState moveState)
		{
			if (moveState.IsMovingOnGround() == false)
				Reset();

			_currentState = moveState;
		}

		public void Reset()
		{
			StepTime = 0;

			_stepSign = GetRandomSign();
		}

		private void CheckStepEnd()
		{
			if (StepTime.Abs() > 1)
			{
				StepEvent?.Invoke(_stepSign);

				StepTime = 0;
				_stepSign *= -1;
			}
		}

		private void UpdateStepTime(float delta)
		{
			float stepSpeed = _currentState.IsMoving() ? _data.MovingStepSpeed : _data.CrouchStepSpeed;

			float velocityPercent = _velocityPercentGetter.Invoke();
			StepTime += stepSpeed * _stepSign * delta * velocityPercent;
		}

		private static int GetRandomSign() => Random.Range(0, 2) == 1 ? 1 : -1;
	}
}