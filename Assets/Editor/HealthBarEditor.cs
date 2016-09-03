using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor (typeof (HealthBar))]
public class HealthBarEditor : Editor
{

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("pos"));
		EditorGUILayout.PropertyField (serializedObject.FindProperty ("healthNodeImageFullPrefab"));
		EditorGUILayout.PropertyField (serializedObject.FindProperty ("healthNodeImageEmptyPrefab"));

		serializedObject.ApplyModifiedProperties();
	}

	public void OnSceneGUI ()
	{
		HealthBar healthBar = target as HealthBar;
		healthBar.pos = new Vector3 (healthBar.pos.x, healthBar.pos.y, 0);

		// everything in blinding purple for visibility
		Handles.color = Color.magenta;
		GUIStyle style = new GUIStyle ();
		style.normal.textColor = Color.magenta;
		style.alignment = TextAnchor.LowerCenter;

		// position
		float x = healthBar.pos.x;
		float y = healthBar.pos.y;

		// width / height default
		float width = 1;
		float height = 1;

		// change width height to match sprite size if possible
		Image image = healthBar.healthNodeImageEmptyPrefab.GetComponent<Image> ();
		if (image)
		{
			width = image.sprite.bounds.size.x * healthBar.transform.localScale.x;
			height = image.sprite.bounds.size.y * healthBar.transform.localScale.y;
		}

		// draw sample healthbar squares
		int numSampleHealthNodes = 3;
		for (int i = 0; i < numSampleHealthNodes; i++)
		{
			Handles.DrawSolidRectangleWithOutline(new Rect((x - width / 2) + healthBar.pos.x + (i * 1.5f), y - height / 2, width, height), Color.red, Color.gray);
		}

		// label healthbar
		Handles.Label(new Vector3(x + healthBar.pos.x, y - height / 2), "HealthBar", style);
	}
}
