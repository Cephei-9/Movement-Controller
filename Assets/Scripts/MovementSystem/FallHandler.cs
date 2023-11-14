using System;
using Cephei;
using UnityEngine;

namespace BlackBlock.MovementSystem
{
	public class FallHandler : IUpdatable, IMoveStateObservable
	{
		public event Action<float> FallEvent;

		private Transform _playerTransform;

		private float _maxHeight;

		private bool _isCheckHeight;

		public FallHandler(Transform playerTransform)
		{
			_playerTransform = playerTransform;
			
			Reset();
		}

		public void UpdateWork(float delta)
		{
			if (_isCheckHeight == false)
				return;

			if (_playerTransform.position.y > _maxHeight)
				_maxHeight = _playerTransform.position.y;
		}

		public void ChangeMoveState(MoveState moveState)
		{
			TryEndFall(moveState);
			TryBeginFall(moveState);
		}

		private void TryBeginFall(MoveState moveState)
		{
			if (_isCheckHeight == false && moveState.IsAir()) 
				_isCheckHeight = true;
		}

		private void TryEndFall(MoveState moveState)
		{
			if (_isCheckHeight && moveState.IsOnGround()) 
				Fall();
		}

		private void Fall()
		{
			float fallingHeight = _maxHeight - _playerTransform.position.y;
			FallEvent?.Invoke(fallingHeight);

			Reset();
		}

		private void Reset()
		{
			_maxHeight = -1 * Mathf.Infinity;
			_isCheckHeight = false;
		}
	}
}