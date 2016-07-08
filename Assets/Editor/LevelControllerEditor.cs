using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
	static string dataPath = Application.dataPath + "/"; // Assets
	static string resourceFolder = dataPath + "Resources/"; // Assets/Resources
	static string backgroundsFolder = resourceFolder + "Backgrounds/";
	static string middlegroundsFolder = resourceFolder + "Middlegrounds/";
	static string wavesFolder = resourceFolder + "Waves/";

	List<bool> levelFoldBools = new List<bool> ();

	private string[] GetPrefabs(string folder)
	{
		DirectoryInfo dir = new DirectoryInfo (folder);
		return dir.GetFiles ("*.prefab").Select((file) => file.Name.Replace(".prefab", "")).ToArray();
	}

	private void ListOptions(SerializedProperty property, string[] options, string label) {
		int backgroundIndex = Array.IndexOf (options, property.stringValue);
		property.stringValue = options [EditorGUILayout.Popup (label, backgroundIndex == -1 ? 0 : backgroundIndex, options)];
	}

	public override void OnInspectorGUI ()
	{
		string[] backgrounds = GetPrefabs (backgroundsFolder);
		string[] middlegrounds = GetPrefabs (middlegroundsFolder);
		string[] waves = GetPrefabs (wavesFolder);

		serializedObject.Update ();

		// levels
		SerializedProperty levels = serializedObject.FindProperty ("levels");
		EditorGUILayout.PropertyField(levels);
		if (levels.isExpanded) {
			EditorGUILayout.PropertyField(levels.FindPropertyRelative("Array.size"));

			// for each level...
			EditorGUI.indentLevel += 1;
			for (int i = 0; i < levels.arraySize; i++) {
				GUIContent label = new GUIContent();
				string name = levels.GetArrayElementAtIndex(i).FindPropertyRelative ("name").stringValue;
				label.text = name.Length != 0 ? name : "Level " + i;

				// make sure there is enough bools in the list for the foldout
				if (levelFoldBools.Count () <= i) {
					levelFoldBools.Add (false);
				}

				// foldout for levels
				EditorGUILayout.GetControlRect (true, 16f, EditorStyles.foldout);
				Rect foldRect = GUILayoutUtility.GetLastRect ();
				levelFoldBools[i] = EditorGUI.Foldout (foldRect, levelFoldBools[i], label, true);

				if (levelFoldBools [i]) {
					// define level fields
					EditorGUILayout.PropertyField (levels.GetArrayElementAtIndex (i).FindPropertyRelative ("name"));
					// backgrounds
					ListOptions (levels.GetArrayElementAtIndex (i).FindPropertyRelative ("background"), backgrounds, "Background");
					// middlegrounds
					ListOptions (levels.GetArrayElementAtIndex (i).FindPropertyRelative ("middleground"), middlegrounds, "Middleground");
					// waves
					EditorGUI.indentLevel += 1;
					SerializedProperty wavelist = levels.GetArrayElementAtIndex (i).FindPropertyRelative ("waves");
					EditorGUILayout.PropertyField (wavelist);
					if (wavelist.isExpanded) {
						EditorGUILayout.PropertyField (wavelist.FindPropertyRelative ("Array.size"));

						// for each wave...
						for (int j = 0; j < wavelist.arraySize; j++) {
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

//	public void OnSceneGUI()
//	{
//	}
}