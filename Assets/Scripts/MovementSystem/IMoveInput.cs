using UnityEngine;

namespace BlackBlock.MovementSystem
{
	public interface IMoveInput
	{
		public Vector2 MoveVector { get; }
		
		public Vector2 RotateVector { get; }

		public bool IsCrouch { get; }
		
		public bool IsJump { get; }
	}
}