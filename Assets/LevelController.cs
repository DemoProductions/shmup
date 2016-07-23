using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class LevelController : MonoBehaviour
{

	[System.Serializable]
	public class Level
	{
		public string name;
		public string background;
		public string middleground;
		public string foreground;
		public string player1;
		public Vector3 player1Spawn;
		//public string player2;
		//public Vector3 player2Spawn;
		public int numWaves;
		public string[] waves;
		//public int boss; // for later
	}

	public float waveSeparation = 10;

	public Level[] levels;

	public static Func<string, string, string> JoinPaths = (str1, str2) => str1 + "/" + str2;

	public static string backgroundsFolder = "Backgrounds";
	public static string middlegroundsFolder = "Middlegrounds";
	public static string foregroundsFolder = "Foregrounds";
	public static string playersFolder = "Players";
	public static string wavesFolder = "Waves";

	// Use this for initialization
	void Start ()
	{
		// debug for vars
//		foreach (Level level in levels) {
//			Debug.Log (level.name);
//			Debug.Log (level.background);
//			Debug.Log (level.middleground);
//			foreach (string wave in level.waves) {
//				Debug.Log (wave);
//			}
//		}

		// get proper level
		Level level = levels [0]; // for now default to first level

		// instantiate background
		GameObject background = Resources.Load (JoinPaths (backgroundsFolder, level.background)) as GameObject;
		if (background)
		{
			Instantiate (background, Vector3.zero, Quaternion.identity);
		}

		// instantiate middleground
		GameObject middleground = Resources.Load (JoinPaths (middlegroundsFolder, level.middleground)) as GameObject;
		if (middleground)
		{
			Instantiate (middleground, Vector3.zero, Quaternion.identity);
		}

		// instantiate foreground
		// instantiate player ? (will need to add this probably, though it will grab this from player choice...)
		GameObject foreground = Resources.Load (JoinPaths (foregroundsFolder, level.foreground)) as GameObject;
		if (foreground)
		{
			GameObject instantiatedForeground = Instantiate (foreground, Vector3.zero, Quaternion.identity) as GameObject;

			GameObject player1 = Resources.Load (JoinPaths (playersFolder, level.player1)) as GameObject;
			GameObject instantiatedPlayer1 = Instantiate (player1, level.player1Spawn, Quaternion.identity) as GameObject;
			instantiatedPlayer1.transform.SetParent (instantiatedForeground.transform);
		}

		// instantiate waves
		for (int i = 0; i < level.numWaves; i++)
		{
			int r = UnityEngine.Random.Range (0, level.waves.Length);
			GameObject wave = Resources.Load (JoinPaths (wavesFolder, level.waves [r])) as GameObject;
			if (wave)
			{
				Instantiate (wave, new Vector3 (waveSeparation * (i + 1), 0), Quaternion.identity);
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}
}

