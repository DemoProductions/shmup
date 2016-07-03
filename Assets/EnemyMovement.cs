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
	SerializedProperty xVelocity;
	SerializedProperty yVelocity;

	SerializedProperty xModifier;
	SerializedProperty xModifierVelocity;
	SerializedProperty xModifierRate;
	SerializedProperty xModifierOffset;

	SerializedProperty yModifier;
	SerializedProperty yModifierVelocity;
	SerializedProperty yModifierRate;
	SerializedProperty yModifierOffset;

	void OnEnable()
	{
		xVelocity = serializedObject.FindProperty ("xVelocity");
		yVelocity = serializedObject.FindProperty ("yVelocity");

		xModifier = serializedObject.FindProperty ("xModifier");
		xModifierVelocity = serializedObject.FindProperty ("xModifierVelocity");
		xModifierRate = serializedObject.FindProperty ("xModifierRate");
		xModifierOffset = serializedObject.FindProperty ("xModifierOffset");

		yModifier = serializedObject.FindProperty ("yModifier");
		yModifierVelocity = serializedObject.FindProperty ("yModifierVelocity");
		yModifierRate = serializedObject.FindProperty ("yModifierRate");
		yModifierOffset = serializedObject.FindProperty ("yModifierOffset");
	}

	override public void OnInspectorGUI()
	{
		serializedObject.Update ();

		EditorGUILayout.LabelField ("Base Movement", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField (xVelocity, new GUIContent ("X Velocity"));
		EditorGUILayout.PropertyField (yVelocity, new GUIContent ("Y Velocity"));
		EditorGUILayout.Space ();

		EditorGUILayout.LabelField ("X Modifier", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(xModifier, new GUIContent ("X Modifier"));

		switch ((int)xModifier.enumValueIndex)
		{
		case (int)EnemyMovement.modifiers.sin:
			EditorGUILayout.PropertyField (xModifierVelocity, new GUIContent ("Sin Velocity"));
			EditorGUILayout.PropertyField (xModifierRate, new GUIContent ("Sin Rate"));
//			EditorGUILayout.PropertyField (xModifierOffset, new GUIContent ("Sin Offset")); // non-slider version
			EditorGUILayout.Slider(xModifierOffset, 0, Mathf.PI, new GUIContent("Sin Offset"));
			break;
		case (int)EnemyMovement.modifiers.cos:
			EditorGUILayout.PropertyField (xModifierVelocity, new GUIContent ("Cos Velocity"));
			EditorGUILayout.PropertyField (xModifierRate, new GUIContent ("Cos Rate"));
//			EditorGUILayout.PropertyField (xModifierOffset, new GUIContent ("Cos Offset")); // non-slider version
			EditorGUILayout.Slider(xModifierOffset, 0, Mathf.PI, new GUIContent("Cos Offset"));
			break;
		}
		EditorGUILayout.Space ();

		EditorGUILayout.LabelField ("Y Modifier", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(yModifier, new GUIContent ("Y Modifier"));

		switch ((int)yModifier.enumValueIndex)
		{
		case (int)EnemyMovement.modifiers.sin:
			EditorGUILayout.PropertyField (yModifierVelocity, new GUIContent ("Sin Velocity"));
			EditorGUILayout.PropertyField (yModifierRate, new GUIContent ("Sin Rate"));
//			EditorGUILayout.PropertyField (yModifierOffset, new GUIContent ("Sin Offset")); // non-slider version
			EditorGUILayout.Slider(yModifierOffset, 0, Mathf.PI, new GUIContent("Sin Offset"));
			break;
		case (int)EnemyMovement.modifiers.cos:
			EditorGUILayout.PropertyField (yModifierVelocity, new GUIContent ("Cos Velocity"));
			EditorGUILayout.PropertyField (yModifierRate, new GUIContent ("Cos Rate"));
//			EditorGUILayout.PropertyField (yModifierOffset, new GUIContent ("Cos Offset")); // non-slider version
			EditorGUILayout.Slider(yModifierOffset, 0, Mathf.PI, new GUIContent("Cos Offset"));
			break;
		}
		EditorGUILayout.Space ();

		serializedObject.ApplyModifiedProperties ();
	}
}