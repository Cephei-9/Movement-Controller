using UnityEngine;

namespace BlackBlock.MovementSystem
{
	public class BunnyHopPlatform : MonoBehaviour
	{
		[SerializeField]
		private bool _activateStatus;

		public bool ActivateStatus => _activateStatus;
	}
}