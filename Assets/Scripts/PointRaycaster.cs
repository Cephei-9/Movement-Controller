using UnityEngine;

namespace BlackBlock
{
	public class PointRaycaster
	{
		private Transform _point;
		private Vector3 _direction;
		private float _offset;

		private float _hitTime = -1;
		private bool _cachedHitStatus;
		private RaycastHit _cachedHit;

		public float Distance => GetHit().distance;

		public Vector3 HitPoint => GetHit().point;
		
		public bool IsHit => Collider != null;

		public Collider Collider => GetHit().collider;

		public RaycastHit Hit => GetHit();

		public Vector3 RaycastPosition => _point.position + RaycastDirection * _offset;
		
		public Vector3 RaycastDirection => _point.rotation * _direction.normalized;

		public PointRaycaster(Transform point, Vector3 direction, float offset = -0.01f)
		{
			_point = point;
			_direction = direction;
			_offset = offset;
		}

		public bool TryGetComponentFromCollider<TComponent>(out TComponent bunnyHopPlatform)
		{
			bunnyHopPlatform = default;
			return IsHit && Collider.TryGetComponent<TComponent>(out bunnyHopPlatform);
		}

		private RaycastHit GetHit()
		{
			bool hasHitByCurrentTime = Mathf.Approximately(Time.time, _hitTime);

			if (hasHitByCurrentTime)
				return _cachedHit;

			return RaycastHit();
		}

		private RaycastHit RaycastHit()
		{
			Physics.Raycast(RaycastPosition, RaycastDirection, out _cachedHit);
			_hitTime = Time.time;

			return _cachedHit;
		}
	}
}