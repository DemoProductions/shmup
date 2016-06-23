/**
  * Restricts the movement of the object to the screen.
  * @author Jorjon
  */

using UnityEngine;

public class Boundings2D : MonoBehaviour {

	void LateUpdate () {
		var left = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
		var right = Camera.main.ViewportToWorldPoint(Vector3.one).x;
		var top = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
		var bottom = Camera.main.ViewportToWorldPoint(Vector3.one).y;

		float x = transform.position.x;
		float y = transform.position.y;

		// check x
		if (transform.position.x <= left + GetComponent<Renderer>().bounds.extents.x) {
			x = left + GetComponent<Renderer>().bounds.extents.x;
		} else if (transform.position.x >= right - GetComponent<Renderer>().bounds.extents.x) {
			x = right - GetComponent<Renderer>().bounds.extents.x;
		}

		// check y
		if (transform.position.y <= top + GetComponent<Renderer>().bounds.extents.y) {
			y = top + GetComponent<Renderer>().bounds.extents.y;
		} else if (transform.position.y >= bottom - GetComponent<Renderer>().bounds.extents.y) {
			y = bottom - GetComponent<Renderer>().bounds.extents.y;
		}

		transform.position = new Vector3(x, y, transform.position.z);
	}
}