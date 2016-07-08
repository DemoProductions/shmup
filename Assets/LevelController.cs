using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class LevelController : MonoBehaviour {

	[System.Serializable]
	public class Level
	{
		public string name;
		public string background;
		public string middleground;
		public string[] waves;
//		public int boss; // for later
	}

	public Level[] levels;

	// Level pathing
	static string levelsPath = "Levels"; // path to the levels folder
	static string levelFolderName = "Level"; // base name for Level folders
	static string generalFolderName = "General"; // name of general assets folder
	private static Func<int, string> FolderName = (i) => i == 0 ? generalFolderName : levelFolderName + i;
	public static Func<int, string> LevelFolder = (i) => levelsPath + "/" + FolderName(i); // define path to each LevelFolder

	// Level / Wave pathing
	static string waveFolderName = "Wave";
	static string waveFileName = "wave";
	public static Func<int, string> WaveFolder = (i) => LevelFolder(i) + "/" + waveFolderName;
	public static Func<int, int, string> WaveFile = (i, j) => WaveFolder(i) + "/" + waveFileName + j;
	public static Func<int, int, Wave> Wave = (i, j) => Resources.Load(WaveFile(i, j), typeof(Wave)) as Wave;

	// Level / Background pathing
	static string backgroundFileName = "background";
//	public static Func<int, int, GameObject> = (i, j) => (null);

	// Use this for initialization
	void Start () {
		// instantiate level

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

