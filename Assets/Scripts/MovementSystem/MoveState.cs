namespace BlackBlock.MovementSystem
{
	public enum MoveState
	{
		Idle,
		Moving,
		Jump,
		CrouchIdle,
		CrouchMoving,
		Falling
	}
	
	public static class MoveStateExtensions
	{
		public static bool IsCrouch(this MoveState moveState) =>
			moveState == MoveState.CrouchIdle || moveState == MoveState.CrouchMoving;

		public static bool IsIdle(this MoveState moveState) => moveState == MoveState.Idle;

		public static bool IsMoving(this MoveState moveState) => moveState == MoveState.Moving;

		public static bool IsCrouchIdle(this MoveState moveState) => moveState == MoveState.CrouchIdle;

		public static bool IsCrouchMoving(this MoveState moveState) => moveState == MoveState.CrouchMoving;

		public static bool IsJump(this MoveState moveState) => moveState == MoveState.Jump;
		
		public static bool IsFalling(this MoveState moveState) => moveState == MoveState.Falling;
		
		public static bool IsOnGround(this MoveState moveState) =>
			moveState is MoveState.Idle or MoveState.Moving or MoveState.CrouchIdle or MoveState.CrouchMoving;
		
		public static bool IsMovingOnGround(this MoveState moveState) =>
			moveState is MoveState.Moving or MoveState.CrouchMoving;
		
		public static bool IsStay(this MoveState moveState) =>
			moveState is MoveState.Moving or MoveState.Idle;
		
		public static bool IsAir(this MoveState moveState) =>
			moveState is MoveState.Falling or MoveState.Jump;
	}
}