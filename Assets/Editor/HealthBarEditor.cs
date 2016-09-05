using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor (typeof (HealthBar))]
public class HealthBarEditor : Editor
{

	const string healthBarLabel = "HealthBar";

	bool snap = true;
	float snapTo = .5f;

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();

		snap = EditorGUILayout.Toggle ("Snap", snap);
		snapTo = EditorGUILayout.FloatField ("Snap To", snapTo);

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
		//  can use either healthNodeImageEmptyPrefab or healthNodeImageFullPrefab since they are the same size
		Image image = healthBar.healthNodeImageEmptyPrefab.GetComponent<Image> ();
		if (image)
		{
			width = image.sprite.bounds.size.x * healthBar.healthNodeImageEmptyPrefab.transform.localScale.x;
			height = image.sprite.bounds.size.y * healthBar.healthNodeImageEmptyPrefab.transform.localScale.y;
		}

		// draw sample healthbar squares
		int numHealthNodes = healthBar.GetComponent<Health> ().hp;
		for (int i = 0; i < numHealthNodes; i++)
		{
			Handles.DrawSolidRectangleWithOutline (new Rect (x + (0.32f * i), y, width, height), Color.clear, Color.gray);
		}

		// label healthbar
		Handles.Label (new Vector3 (x, y - height / 2), healthBarLabel, style);

		// draw handle for healthbar
		EditorGUI.BeginChangeCheck ();

		Quaternion rotation = Quaternion.identity;

		Vector3 position = Handles.PositionHandle (new Vector3 (x, y, 0), rotation);

		// snap check
		if (snap)
		{   //round(X / N)*N
			position.x = (float)System.Math.Round (position.x / snapTo) * snapTo;
			position.y = (float)System.Math.Round (position.y / snapTo) * snapTo;
		}

		if (EditorGUI.EndChangeCheck ())
		{
			Undo.RecordObject (healthBar, "Moved healthbar");
			healthBar.pos.x = (position.x);
			healthBar.pos.y = (position.y);
		}
	}
}
