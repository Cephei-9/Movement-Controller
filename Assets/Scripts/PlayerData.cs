using BlackBlock.MovementSystem;
using UnityEngine;

namespace BlackBlock
{
	/// <summary>
	/// Use this on height level. Modes and settings use this in runtime
	/// Different modes can set different data 
	/// </summary>
	[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerController/PlayerData", order = 51)]
	public class PlayerData : ScriptableObject
	{
		[Header("FPData")]
		public MovementSystemData MovementData;
	}
}