using UnityEditor;
using UnityEngine;

public static class WaveList {

	public static void Show (SerializedProperty list) {
		EditorGUILayout.PropertyField(list);
		if (list.isExpanded) {
			EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
			for (int i = 0; i < list.arraySize; i++) {
				GUIContent label = new GUIContent();
				label.text = "Spawn " + i;
				EditorGUILayout.PropertyField (list.GetArrayElementAtIndex (i), label, true);
			}
		}
	}

}
