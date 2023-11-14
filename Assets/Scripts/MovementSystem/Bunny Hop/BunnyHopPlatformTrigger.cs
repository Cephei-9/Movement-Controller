using System;
using UnityEngine;

namespace BlackBlock.MovementSystem
{
	public class BunnyHopPlatformTrigger
	{
		public event Action<bool> ChangeModeStatusEvent;
		
		private PointRaycaster _raycaster;
		
		private bool _isWork;
		private bool _activateModeStatus;
		
		private readonly Func<Collider> _groundColliderGetter;

		public bool ActivateModeStatus
		{
			get => _activateModeStatus && _isWork;
			private set => _activateModeStatus = value;
		}

		public BunnyHopPlatformTrigger(Func<Collider> groundColliderGetter) =>
			_groundColliderGetter = groundColliderGetter;

		public void OnGrounded(Collider groundCollider)
		{
			if (_isWork == false)
				return;

			bool tryGetComponent = groundCollider.TryGetComponent(out BunnyHopPlatform platform);

			if (groundCollider != null && tryGetComponent &&
			    ActivateModeStatus != platform.ActivateStatus)
			{
				ActivateModeStatus = platform.ActivateStatus;
				ChangeModeStatusEvent?.Invoke(ActivateModeStatus);
			}
		}

		public void Reset() => _activateModeStatus = false;

		public void Activate() => _isWork = true;
		
		public void Deactivate() => _isWork = false;
	}
}