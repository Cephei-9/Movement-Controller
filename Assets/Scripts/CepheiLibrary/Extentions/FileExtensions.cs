using System.IO;
using UnityEngine;

namespace Cephei.DebugPack
{
	public static class FileExtensions
	{
		public static string AssetPath
		{
			get
			{
				string assetsPath = "";
				int fullLenght = Application.streamingAssetsPath.Length;
				int removedLenght = 16;

				for (int i = 0; i < fullLenght - removedLenght; i++)
				{
					assetsPath += Application.streamingAssetsPath[i];
				}

				return assetsPath;
			}
		}

		public static string GetNameWithoutExtension(this FileInfo fileInfo)
		{
			string nameWithoutExtension = "";

			for (var i = 0; i < fileInfo.Name.Length; i++)
			{
				char currentChar = fileInfo.Name[i];
			
				if (currentChar == '.')
					break;
						
				nameWithoutExtension += currentChar;
			}

			return nameWithoutExtension;
		}
	}
}