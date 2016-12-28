using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{

	public float time;
	public bool running;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (running)
		{
			time += Time.deltaTime;
		}
	}

	public Timer Begin ()
	{
		running = true;
		return this;
	}

	public Timer Stop ()
	{
		running = false;
		return this;
	}

	public Timer Reset ()
	{
		time = 0;
		return this;
	}

	public static bool operator >(Timer t1, float t2) {
		return t1.time > t2;
	}

	public static bool operator >=(Timer t1, float t2) {
		return t1.time >= t2;
	}

	public static bool operator <(Timer t1, float t2) {
		return t1.time < t2;
	}

	public static bool operator <=(Timer t1, float t2) {
		return t1.time <= t2;
	}
}
