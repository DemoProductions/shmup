using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
	public void OnInspectorGUI ()
	{
		serializedObject.Update();

		serializedObject.ApplyModifiedProperties();
	}

//	public void OnSceneGUI()
//	{
//	}
}