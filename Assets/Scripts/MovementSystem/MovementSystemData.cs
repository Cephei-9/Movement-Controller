using UnityEngine;

namespace BlackBlock.MovementSystem
{
	[CreateAssetMenu(fileName = "MovementSystemData", menuName = "PlayerController/Move/MovementSystemData", order = 51)]
	public class MovementSystemData : ScriptableObject
	{
		public DefaultSpeedController.Data SpeedData;

		public FootStepHandler.Data FootStepData;

		public MovementSoundData SoundData;
		
		public ForceByShoot.Data HitForceData;

		public float Sensitivity = 2;

		public float JumpHeight = 1;

		public float LerpFactor = 10;
	}
}