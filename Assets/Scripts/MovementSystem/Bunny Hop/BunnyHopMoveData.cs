using UnityEngine;

namespace BlackBlock.MovementSystem
{
	[CreateAssetMenu(fileName = "BunnyHopMoveData", menuName = "PlayerController/Move/BunnyHopMoveData", order = 51)]
	public class BunnyHopMoveData : ScriptableObject
	{
		/// <summary>
		/// Velocity value to start acceleration
		/// </summary>
		public float AccelerationThreshold = 20;

		public float MaxSpeed = 10;

		public float DefaultSpeed = 5;

		/// <summary>
		/// Acceleration speed
		/// </summary>
		public float AccelerationLerpFactor = 0.5f;

		/// <summary>
		/// Deceleration speed
		/// </summary>
		public float DecelerationLerpFactor = 1;
	}
}