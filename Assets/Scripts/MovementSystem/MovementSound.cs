using Cephei;
using UnityEngine;

namespace BlackBlock.MovementSystem
{
	public class MovementSound : IMoveStateObservable
	{
		private MoveState _currentMoveState;

		private MovementSoundData _data;
		private AudioSource _source;

		public MovementSound(MovementSoundData data, AudioSource source)
		{
			_data = data;
			_source = source;
		}

		public void PlayStep(float velocityPercent)
		{
			_source.pitch = _data.StepsSoundData.GetRandomizedPitch();
			_source.PlayOneShot(_data.StepsSoundData.GetRandomClip(), GetStepVolume(velocityPercent));
		}
		
		public void Reset()
		{
			_source.Stop();
			_currentMoveState = MoveState.Idle;
		}

		private float GetStepVolume(float velocityPercent)
		{
			float randomVolume = _data.StepsSoundData.GetRandomizedVolume();
			float minimalVolume = _data.MinimalStepVolumePercent * randomVolume;
			float volumeByVelocity = (1 - _data.MinimalStepVolumePercent) * randomVolume * velocityPercent;
			float finalVelocity = minimalVolume + volumeByVelocity;
			float crouchingFactor = _currentMoveState.IsCrouch() ? _data.CrouchingFootStepMultiplayer : 1;
			
			return finalVelocity * crouchingFactor;
		}

		public void ChangeMoveState(MoveState moveState)
		{
			if (moveState.IsCrouch())
				PlaySound(_data.CrouchingSoundData);
			if(_currentMoveState.IsCrouch() && moveState.IsStay())
				PlaySound(_data.StandingSoundData);
			if(moveState.IsJump())
				PlaySound(_data.JumpSoundData);
			if(_currentMoveState.IsAir() && moveState.IsOnGround())
				PlaySound(_data.FallSoundData);

			_currentMoveState = moveState;
		}

		private void PlaySound(SoundDataMax soundData) => soundData.PlayClip(_source);
	}
}