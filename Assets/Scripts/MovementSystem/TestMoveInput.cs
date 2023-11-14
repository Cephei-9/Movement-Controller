using UnityEngine;

namespace BlackBlock.MovementSystem
{
	public class TestMoveInput : MonoBehaviour, IMoveInput
	{
		public Vector2 MoveVector { get; private set; }
		public Vector2 RotateVector { get; private set; }
		public bool IsCrouch { get; private set; }
		public bool IsJump { get; private set; }

		private void Update()
		{
			var yaw = Input.GetAxis("Mouse X");
			var pitch = Input.GetAxis("Mouse Y");

			RotateVector = new Vector2(yaw, pitch);

			float x = Input.GetAxisRaw("Horizontal");
			float y = Input.GetAxisRaw("Vertical");

			MoveVector = new Vector2(x, y);

			IsJump = Input.GetButton("Jump");

			IsCrouch = Input.GetKey(KeyCode.C);
		}
	}
}