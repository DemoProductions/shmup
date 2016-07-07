using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Wave))]
public class WaveHandleEditor : Editor
{
	bool snap = true;
	float snapTo = .5f;

	public override void OnInspectorGUI ()
	{
		serializedObject.Update();

		snap = EditorGUILayout.Toggle ("Snap", snap);
		snapTo = EditorGUILayout.FloatField ("Snap To", snapTo);
		WaveList.Show(serializedObject.FindProperty("spawnables"));

		serializedObject.ApplyModifiedProperties();
	}

	public void OnSceneGUI()
	{
		Wave wave = target as Wave;
		wave.transform.position = new Vector3 (wave.transform.position.x, 0, 0);

		// everything in blinding purple for visibility
		Handles.color = Color.magenta;
		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.magenta;
		style.alignment = TextAnchor.LowerCenter;

		// draw spawnable handles
		int count = 0;
		foreach (Wave.Spawnable spawnable in wave.spawnables)
		{
			// position
			float x = spawnable.position.x;
			float y = spawnable.position.y;

			// width / height default
			float width = 1;
			float height = 1;

			// change width height to match sprite size if possible
			if (spawnable.gameObject)
			{
				SpriteRenderer renderer = spawnable.gameObject.GetComponent<SpriteRenderer> ();
				if (renderer)
				{
					width = renderer.sprite.bounds.size.x * spawnable.gameObject.transform.localScale.x;
					height = renderer.sprite.bounds.size.y * spawnable.gameObject.transform.localScale.y;
				}
			}

			// draw spawn square
			Handles.DrawSolidRectangleWithOutline(new Rect((x - width / 2) + wave.transform.position.x, y - height / 2, width, height), Color.red, Color.gray);
//			Handles.DrawSolidRectangleWithOutline(new Rect(spawnable.x - 1, spawnable.y - 1, 2, 2), Color.red, Color.gray);

			// label spawn
			Handles.Label (new Vector3 (x + wave.transform.position.x, y - height / 2), "Spawn " + count++, style);

			// draw positional line from spawn to y = 0
			Handles.DrawLine (new Vector3 (x + wave.transform.position.x, y, 0), new Vector3 (x + wave.transform.position.x, 0, 0));

			// draw handle for spawn
			EditorGUI.BeginChangeCheck( );

			Quaternion rotation = Quaternion.identity;

			if (y < 0)
			{
				rotation *= Quaternion.Euler (0, 0, 180);
			}

			Vector3 position = Handles.PositionHandle (new Vector3 (x + wave.transform.position.x, y, 0), rotation);

			// snap check
			if (snap)
			{	//round(X / N)*N
				position.x = (float)System.Math.Round(position.x / snapTo) * snapTo;
				position.y = (float)System.Math.Round(position.y / snapTo) * snapTo;
			}

			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(wave, "Moved spawnable");
				spawnable.position.x = (position.x - wave.transform.position.x);
				spawnable.position.y = (position.y);
			}
		}

		// camera square
		var left = Camera.main.ViewportToWorldPoint(Vector3.zero).x + wave.transform.position.x;
		var right = Camera.main.ViewportToWorldPoint(Vector3.one).x + wave.transform.position.x;
		var top = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
		var bottom = Camera.main.ViewportToWorldPoint(Vector3.one).y;

		Vector3[] cameraVerts = {new Vector3(left, top, 0), new Vector3(right, top, 0), new Vector3(right, bottom, 0), new Vector3(left, bottom, 0)};
		Handles.DrawSolidRectangleWithOutline(cameraVerts, Color.clear, Color.magenta);

		// y = 0 line
		Handles.DrawLine (new Vector3 (left, 0, 0), new Vector3 (right, 0, 0));
	}
}