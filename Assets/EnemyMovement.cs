using UnityEngine;
using System.Collections;
using UnityEditor;

public class EnemyMovement : MonoBehaviour
{

	public int speed = 100;

	public enum modifiers {
		none,
		sin,
		cos
	}

	public float xVelocity = -1;
	public float yVelocity = 0;

	public modifiers xModifier = modifiers.none;
	public float xModifierVelocity = 0;
	public float xModifierRate = 1;
	public float xModifierOffset = 0;

	public modifiers yModifier = modifiers.none;
	public float yModifierVelocity = 0;
	public float yModifierRate = 1;
	public float yModifierOffset = 0;

	Rigidbody2D rbody;

	// Use this for initialization
	void Start ()
	{
		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		float x = xVelocity;
		float y = yVelocity;

		switch ((int)xModifier)
		{
		case (int)modifiers.sin:
			x += Mathf.Sin ((Time.time + xModifierOffset) * xModifierRate) * xModifierVelocity;
			break;
		case (int)modifiers.cos:
			x += Mathf.Cos ((Time.time + xModifierOffset) * xModifierRate) * xModifierVelocity;
			break;
		}

		switch ((int)yModifier)
		{
		case (int)modifiers.sin:
			y += Mathf.Sin ((Time.time + yModifierOffset) * yModifierRate) * yModifierVelocity;
			break;
		case (int)modifiers.cos:
			y += Mathf.Cos ((Time.time + yModifierOffset) * yModifierRate) * yModifierVelocity;
			break;
		}

		rbody.velocity = new Vector2 (x, y) * Time.deltaTime * speed;
	}
}

[CustomEditor(typeof(EnemyMovement))]
public class EnemyMovementEditor : Editor
{
	override public void OnInspectorGUI()
	{
		var script = target as EnemyMovement;

		EditorGUILayout.LabelField ("Base Movement", EditorStyles.boldLabel);
		script.xVelocity = EditorGUILayout.FloatField ("X Velocity", script.xVelocity);
		script.yVelocity = EditorGUILayout.FloatField ("Y Velocity", script.yVelocity);
		EditorGUILayout.Space ();

		EditorGUILayout.LabelField ("X Modifier", EditorStyles.boldLabel);
		script.xModifier = (EnemyMovement.modifiers)EditorGUILayout.EnumPopup("X Modifier", script.xModifier);

		switch ((int)script.xModifier)
		{
		case (int)EnemyMovement.modifiers.sin:
			script.xModifierVelocity = EditorGUILayout.FloatField ("Sin Velocity", script.xModifierVelocity);
			script.xModifierRate = EditorGUILayout.FloatField ("Sin Rate", script.xModifierRate);
			script.xModifierOffset = EditorGUILayout.FloatField ("Sin Offset", script.xModifierOffset);
			break;
		case (int)EnemyMovement.modifiers.cos:
			script.xModifierVelocity = EditorGUILayout.FloatField ("Cos Velocity", script.xModifierVelocity);
			script.xModifierRate = EditorGUILayout.FloatField ("Cos Rate", script.xModifierRate);
			script.xModifierOffset = EditorGUILayout.FloatField ("Cos Offset", script.xModifierOffset);
			break;
		}
		EditorGUILayout.Space ();

		EditorGUILayout.LabelField ("Y Modifier", EditorStyles.boldLabel);
		script.yModifier = (EnemyMovement.modifiers)EditorGUILayout.EnumPopup("Y Modifier", script.yModifier);

		switch ((int)script.yModifier)
		{
		case (int)EnemyMovement.modifiers.sin:
			script.yModifierVelocity = EditorGUILayout.FloatField ("Sin Velocity", script.yModifierVelocity);
			script.yModifierRate = EditorGUILayout.FloatField ("Sin Rate", script.yModifierRate);
			script.yModifierOffset = EditorGUILayout.FloatField ("Sin Offset", script.yModifierOffset);
			break;
		case (int)EnemyMovement.modifiers.cos:
			script.yModifierVelocity = EditorGUILayout.FloatField ("Cos Velocity", script.yModifierVelocity);
			script.yModifierRate = EditorGUILayout.FloatField ("Cos Rate", script.yModifierRate);
			script.yModifierOffset = EditorGUILayout.FloatField ("Cos Offset", script.yModifierOffset);
			break;
		}
		EditorGUILayout.Space ();

	}
}