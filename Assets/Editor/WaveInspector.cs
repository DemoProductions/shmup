using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Wave), true)]
public class WaveInspector : Editor {

	public override void OnInspectorGUI () {
		serializedObject.Update();
		WaveList.Show(serializedObject.FindProperty("spawnables"));
		serializedObject.ApplyModifiedProperties();
	}

}
