using Cephei;
using UnityEngine;

namespace BlackBlock.MovementSystem
{
	[CreateAssetMenu(fileName = "MovementSoundData", menuName = "PlayerController/Move/MovementSound", order = 51)]
	public class MovementSoundData : ScriptableObject
	{
		[Range(0, 1)]
		public float CrouchingFootStepMultiplayer = 0.4f;

		[Range(0, 1)]
		public float MinimalStepVolumePercent = 0.3f;

		public SoundDataMax StepsSoundData;
		public SoundDataMax FallSoundData;
		public SoundDataMax JumpSoundData;
		public SoundDataMax CrouchingSoundData;
		public SoundDataMax StandingSoundData; 
	}
}