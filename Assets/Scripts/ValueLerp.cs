using Cephei;
using UnityEngine;

namespace BlackBlock
{
	public class ValueLerp : IUpdatable
	{
		public float Value;

		public float TargetValue;

		public float LerpFactor;

		public ValueLerp(float lerpFactor) => LerpFactor = lerpFactor;

		public ValueLerp(float value, float targetValue, float lerpFactor)
		{
			Value = value;
			TargetValue = targetValue;
			LerpFactor = lerpFactor;
		}

		public void UpdateWork(float delta) => 
			Value = Mathf.Lerp(Value, TargetValue, LerpFactor * delta);
	}
}