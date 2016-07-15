using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
	static Func<string, string, string> JoinPaths = LevelController.JoinPaths;

	static string dataPath = Application.dataPath; // Assets
	static string resourceFolder = JoinPaths(dataPath, "Resources"); // Assets/Resources
	static string backgroundsFolder = JoinPaths(resourceFolder, LevelController.backgroundsFolder);
	static string middlegroundsFolder = JoinPaths(resourceFolder, LevelController.middlegroundsFolder);
    static string foregroundsFolder = JoinPaths(resourceFolder, LevelController.foregroundsFolder);
    static string wavesFolder = JoinPaths(resourceFolder, LevelController.wavesFolder);

	static List<bool> levelFoldBools = new List<bool> ();

	private string[] GetPrefabs(string folder)
	{
		DirectoryInfo dir = new DirectoryInfo (folder);
		return dir.GetFiles ("*.prefab").Select((file) => file.Name.Replace(".prefab", "")).ToArray();
	}

	private void ListOptions(SerializedProperty property, string[] options, string label)
	{
		int backgroundIndex = Array.IndexOf (options, property.stringValue);
		property.stringValue = options [EditorGUILayout.Popup (label, backgroundIndex == -1 ? 0 : backgroundIndex, options)];
	}

	public override void OnInspectorGUI ()
	{
		string[] backgrounds = GetPrefabs (backgroundsFolder);
		string[] middlegrounds = GetPrefabs (middlegroundsFolder);
        string[] foregrounds = GetPrefabs (foregroundsFolder);
        string[] waves = GetPrefabs (wavesFolder);

		serializedObject.Update ();

		// wave separation
		EditorGUILayout.PropertyField(serializedObject.FindProperty("waveSeparation"));

		// levels
		SerializedProperty levels = serializedObject.FindProperty ("levels");
		EditorGUILayout.PropertyField(levels);
		if (levels.isExpanded)
		{
			EditorGUILayout.PropertyField(levels.FindPropertyRelative("Array.size"));

			// for each level...
			EditorGUI.indentLevel += 1;
			for (int i = 0; i < levels.arraySize; i++)
			{
				GUIContent label = new GUIContent();
				string name = levels.GetArrayElementAtIndex(i).FindPropertyRelative ("name").stringValue;
				label.text = name.Length != 0 ? name : "Level " + i;

				// make sure there is enough bools in the list for the foldout
				while (levelFoldBools.Count () <= i)
				{
					levelFoldBools.Add (false);
				}

				// foldout for levels
				EditorGUILayout.GetControlRect (true, 16f, EditorStyles.foldout);
				Rect foldRect = GUILayoutUtility.GetLastRect ();
				levelFoldBools[i] = EditorGUI.Foldout (foldRect, levelFoldBools[i], label, true);

				if (levelFoldBools [i])
				{
					// define level fields
					EditorGUILayout.PropertyField (levels.GetArrayElementAtIndex (i).FindPropertyRelative ("name"));
					// backgrounds
					ListOptions (levels.GetArrayElementAtIndex (i).FindPropertyRelative ("background"), backgrounds, "Background");
					// middlegrounds
					ListOptions (levels.GetArrayElementAtIndex (i).FindPropertyRelative ("middleground"), middlegrounds, "Middleground");
                    // foregrounds
                    ListOptions(levels.GetArrayElementAtIndex (i).FindPropertyRelative("foreground"), foregrounds, "Foreground");
                    // waves
                    EditorGUI.indentLevel += 1;
					SerializedProperty wavelist = levels.GetArrayElementAtIndex (i).FindPropertyRelative ("waves");
					EditorGUILayout.PropertyField (wavelist);
					if (wavelist.isExpanded)
					{
						EditorGUILayout.PropertyField (wavelist.FindPropertyRelative ("Array.size"));

						// for each wave...
						for (int j = 0; j < wavelist.arraySize; j++)
						{
							GUIContent wavelabel = new GUIContent ();
							wavelabel.text = "Wave " + j;

							// define wave field
							ListOptions (wavelist.GetArrayElementAtIndex (j), waves, "Wave " + j);
						}
					}
					EditorGUI.indentLevel -= 1;
				}
			}
			EditorGUI.indentLevel -= 1;
		}

		serializedObject.ApplyModifiedProperties ();
	}

	public void OnSceneGUI()
	{
		LevelController levelController = target as LevelController;
		for (int i = 0; i < 3; i++)
		{
			// camera square
			var left = Camera.main.ViewportToWorldPoint(Vector3.zero).x + (levelController.waveSeparation * i);
			var right = Camera.main.ViewportToWorldPoint(Vector3.one).x + (levelController.waveSeparation * i);
			var top = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
			var bottom = Camera.main.ViewportToWorldPoint(Vector3.one).y;

			Vector3[] cameraVerts = {new Vector3(left, top, 0), new Vector3(right, top, 0), new Vector3(right, bottom, 0), new Vector3(left, bottom, 0)};
			Handles.DrawSolidRectangleWithOutline(cameraVerts, Color.clear, Color.magenta);
		}
	}
}