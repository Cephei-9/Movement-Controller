using System.Collections;
using BlackBlock.MovementSystem;
using Cephei;
using UnityEngine;
using UnityEngine.Serialization;

namespace BlackBlock
{
	public class LocalPlayerController : MonoBehaviour, ICoroutineRunner
	{
		public PlayerData PlayerData;
		
		[FormerlySerializedAs("_controller")]
		public FPCharracterController Controller;
		
		public MovementSystemData MovementSystemData;

		public TestMoveInput MoveInput;

		public Transform RaycasterPoint;

		[Header("Test")]

		public bool BunnyHopMode;
		
		
		[SerializeField]
		private BunnyHopMoveData BunnyHopData;

		public AudioSource Source;

		private MovementSystem.MovementSystem _movementSystem;

		private void Awake()
		{
			InitMovement();
		}

		private IEnumerator Start()
		{
			yield return new WaitForSeconds(3);
			
			
		}

		private void InitMovement()
		{
			_movementSystem = new MovementSystem.MovementSystem(Controller, MoveInput, PlayerData.MovementData, BunnyHopData,
				Source, RaycasterPoint, this);

			if (BunnyHopMode)
				_movementSystem.ActivateBunnyHopMode();
		}

		private void Update()
		{
			_movementSystem.UpdateWork(Time.deltaTime);

			TestMethod();
		}
		
		private void TestMethod()
		{
			if(Input.GetKeyDown(KeyCode.P))
			{
				if(Controller.pause == false)
					_movementSystem.FreezeMovement();
				else
					_movementSystem.UnFreezeMovement();	
			}

			if (Input.GetKeyDown(KeyCode.U))
			{
				_movementSystem.SetPosition(Vector3.forward, Quaternion.Euler(0, 90, 0));
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				_movementSystem.Reset();
				_movementSystem.SetPosition(Vector3.forward, Quaternion.Euler(0, 90, 0));
			}

			if (Input.GetKeyDown(KeyCode.H))
			{
				_movementSystem.Hit(ShootForce.Power, Vector3.forward);
			}
		}

	}
}