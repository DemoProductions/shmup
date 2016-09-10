using UnityEngine;
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
		public int numWaves;
		public int maxTimesAWaveCanInstantiate;
		public string[] waves;
		public string boss;
	}

	public float waveSeparation = 10;

	public string player1;
	public Vector3 player1Spawn;

	public Level[] levels;

	public static Func<string, string, string> JoinPaths = (str1, str2) => str1 + "/" + str2;

	public static string backgroundsFolder = "Backgrounds";
	public static string middlegroundsFolder = "Middlegrounds";
	public static string foregroundsFolder = "Foregrounds";
	public static string playersFolder = "Players";
	public static string bossesFolder = "Bosses";
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
		GameObject foreground = Resources.Load (JoinPaths (foregroundsFolder, level.foreground)) as GameObject;
		if (foreground)
		{
			GameObject instantiatedForeground = Instantiate (foreground, Vector3.zero, Quaternion.identity) as GameObject;

			// instantiate player
			GameObject player1GameObject = Resources.Load (JoinPaths (playersFolder, player1)) as GameObject;
			GameObject instantiatedPlayer1 = Instantiate (player1GameObject, player1Spawn, Quaternion.identity) as GameObject;
			instantiatedPlayer1.transform.SetParent (instantiatedForeground.transform);

			// instantiate boss
			GameObject bossGameObject = Resources.Load (JoinPaths (bossesFolder, level.boss)) as GameObject;
			GameObject instantiatedBoss = Instantiate (bossGameObject, new Vector3 (1, 0, 0) * 2 + new Vector3 (waveSeparation * (level.numWaves + 1), 0), Quaternion.identity) as GameObject;
			instantiatedBoss.transform.SetParent (instantiatedForeground.transform);

			// instantiate waves randomly
			if (level.numWaves / level.maxTimesAWaveCanInstantiate > level.waves.Length)
			{
				Debug.Log ("Number of waves and max times a wave can instantiate do not fit with the length of wave types that can be in this level");
			}

			int[] waveCount = new int [level.numWaves]; // keep track of what r initially lands on to make sure that waves can only be instantiated the maxTimesAWaveCanInstantiate
			for (int i = 0; i < level.numWaves; i++)
			{
				int r = UnityEngine.Random.Range (0, level.waves.Length);

				// if r lands on a wave that has been instantiated the max number of times, increment r until a wave that can be instantiated is found
				while (waveCount [r] > level.maxTimesAWaveCanInstantiate)
				{
					if (r == level.numWaves - 1)
						r = 0;
					else
						r++;
				}

				GameObject wave = Resources.Load (JoinPaths (wavesFolder, level.waves [r])) as GameObject;
				if (wave)
				{
					GameObject instantiatedWave = Instantiate (wave, (Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0)) * 2) + new Vector3 (waveSeparation * i, 0), Quaternion.identity) as GameObject;
					instantiatedWave.transform.SetParent (instantiatedForeground.transform);
					waveCount [r]++;
				}
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}
}

