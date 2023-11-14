using System;
using Cephei;
using UnityEngine;

namespace BlackBlock.MovementSystem
{
	public class GroundRaycaster : PointRaycaster, IUpdatable
	{
		private Transform _point;

		private Func<Vector3> _groundPositionGetter;

		public GroundRaycaster(Transform point, Vector3 direction, Func<Vector3> groundPositionGetter, float offset = -0.01f) : base(point, direction,
			offset)
		{
			_point = point;
			_groundPositionGetter = groundPositionGetter;
		}

		public void UpdateWork(float delta) => 
			_point.position = _groundPositionGetter.Invoke();

		public void SetGroundPositionGetter(Func<Vector3> groundPositionGetter) => 
			_groundPositionGetter = groundPositionGetter;
	}
}