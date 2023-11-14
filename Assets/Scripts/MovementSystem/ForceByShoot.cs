using System;
using System.Collections;
using System.Collections.Generic;
using Cephei;
using CepheiForNPC;
using UnityEngine;
using UnityEngine.Serialization;

namespace BlackBlock.MovementSystem
{
	public class ForceByShoot
	{
		[Serializable]
		public class Data
		{
			[FormerlySerializedAs("ForceByTime")]
			public AnimationCurve ForceByTimeCurve;

			[FormerlySerializedAs("ForceTime")]
			public float Time = 1;
			
			public float PowerShootForce = 5;
			public float MiddleShootForce = 3;
			public float WeakShootForce = 1;
		}

		private FPCharracterController _controller;
		private Data _data;
		private ICoroutineRunner _coroutineRunner;
		
		private List<Coroutine> _activeCoroutines  = new List<Coroutine>();

		public ForceByShoot(FPCharracterController controller, Data data, ICoroutineRunner coroutineRunner)
		{
			_controller = controller;
			_data = data;
			_coroutineRunner = coroutineRunner;
		}

		public void Hit(ShootForce forceType, Vector3 direction) => 
			_activeCoroutines.Add(_coroutineRunner.StartCoroutine(AddSmoothForce(forceType, direction)));

		public void Stop()
		{
			foreach (Coroutine activeCoroutine in _activeCoroutines)
			{
				_coroutineRunner.StopCoroutine(activeCoroutine);
			}
		}
		
		public void Reset()
		{
			Stop();
			_activeCoroutines = new List<Coroutine>();
		}

		private IEnumerator AddSmoothForce(ShootForce forceType, Vector3 direction)
		{
			float force = GetForceByType(forceType);
			Vector3 normalizedDirection = direction.normalized;
			
			float timer = 0;

			while (timer < _data.Time)
			{
				timer += Time.deltaTime;

				float timedForce = _data.ForceByTimeCurve.Evaluate(timer / _data.Time) * force;
				_controller.AddForce(normalizedDirection * timedForce);

				yield return new WaitForFixedUpdate();
			}
		}

		private float GetForceByType(ShootForce forceType)
		{
			if (forceType == ShootForce.Power)
				return _data.PowerShootForce;
			if (forceType == ShootForce.Middle)
				return _data.MiddleShootForce;
			if (forceType == ShootForce.Weak)
				return _data.WeakShootForce;

			Debug.Log("Not correctly shoot force");
			return 0;
		}
	}
}