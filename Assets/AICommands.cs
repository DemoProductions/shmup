using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AICommands : MonoBehaviour {

	List<List<Step>> states = new List<List<Step>>();
	State currentState;
	int currentStateNum = -1; // allow any state to start
	Weapon weapon;
	Rigidbody2D rbody;

	Timer timer;
	int rotation = 0;
	int speed = 20;

	protected virtual void DefineStates() {
		// define states here
	}

	// Use this for initialization
	protected virtual void Start () {
		weapon = this.GetComponentInChildren<Weapon> ();
		timer = this.GetComponent<Timer> ();
		rbody = this.GetComponent<Rigidbody2D> ();

		// add commands here
		DefineStates ();

		SwitchToRandomState ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		// handle AI update here
		while(currentState.execute()) return;
		// next random state
		SwitchToRandomState ();
	}

	protected void AddState(List<Step> state) {
		states.Add (state);
	}

	// switch to random state. cannot be the same as current state
	protected void SwitchToRandomState () {
		int r = UnityEngine.Random.Range (0, states.Count);

		if (r == currentStateNum) {
			r++;
			if (r > states.Count - 1) r = 0;
		}

		currentStateNum = r;
		currentState = new State (states [currentStateNum], timer);
	}

	protected StateBuilder First(Func<bool> func) {
		StateBuilder sb = new StateBuilder(func);
		sb.setTimer (timer);
		return sb;
	}

	protected bool Shoot() {
		this.weapon.Shoot ();
		return true;
	}

	protected Func<bool> SwitchProjectile(int projectile) {
		return () => {
			int proj = projectile;
			this.weapon.SwitchProjectile(proj);
			return true;
		};
	}

	protected Func<bool> Wait(float seconds) {
		return () => {
			float time = seconds;
			if (timer >= time) return true;
			else return false;
		};
	}

	protected Func<bool> MoveTo(Func<Vector2> location) {
		return () => {
			Func<Vector2> newLocation = location;
			if (rbody.position != newLocation()) {
				rbody.position = Vector2.MoveTowards (rbody.position, newLocation(), this.speed * Time.deltaTime);
				return false;
			}
			else {
				return true;
			}
		};
	}

	protected Func<bool> Rotate(int degrees, int rate) {
		int sign = degrees > 0 ? 1 : -1;
		return () => {
			int angle = degrees;
			if (rotation < Math.Abs(angle)) {
				rotation += Math.Abs(rate);
				transform.Rotate (new Vector3 (0, 0, sign * Math.Abs(rate)));
				return false;
			}
			else {
				rotation = 0;
				return true;
			}
		};
	}

	protected Func<bool> Rotate(int degrees) {
		return Rotate (degrees, 5);
	}

	protected class Step {
		List<Func<bool>> funcs;
		bool status;

		public Step(List<Func<bool>> funcs) {
			this.funcs = funcs;
		}

		public bool execute() {
			status = true;
			int count = 0;
			foreach (Func<bool> func in funcs) {
				bool funcStatus = func();
				status = status && funcStatus;
			}
			return status;
		}
	}

	private class State {
		int step = 0;
		List<Step> steps;
		Timer timer;

		public State(List<Step> steps, Timer timer) {
			this.steps = steps;
			this.timer = timer;
		}

		public bool execute() {
			if (!timer.running) timer.Reset ().Begin ();

			if (steps [step].execute ()) {
				timer.Stop ();
				step++;
			}

			if (step < steps.Count) return true;
			else return false;
		}
	}

	protected class StateBuilder {
		List<Func<bool>> step = new List<Func<bool>> ();
		List<Step> steps = new List<Step> ();
		Timer timer;

		public StateBuilder(Func<bool> first) {
			step.Add(first);
		}

		public void setTimer(Timer timer) {
			this.timer = timer;
		}

		public StateBuilder Then(Func<bool> func) {
			steps.Add (new Step (step));
			step = new List<Func<bool>> ();
			step.Add (func);
			return this;
		}

		public StateBuilder And(Func<bool> func) {
			step.Add (func);
			return this;
		}

		public StateBuilder For(float seconds) {
			int index = step.Count - 1;
			Func<bool> originalFunc = step[index];

			Func<bool> newFunc = () => {
				float time = seconds;
				Func<bool> func = originalFunc;

				if (timer < time) {
					func ();
					return false;
				}
				else {
					return true;
				}
			};

			step.RemoveAt (index);
			step.Insert (index, newFunc);
			return this;
		}

		public List<Step> End() {
			steps.Add (new Step (step));
			return steps;
		}
	}

	protected static class Location {
		public static Vector2 TopRight() {
			return Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 20, Camera.main.pixelHeight - 20));
		}

		public static Vector2 TopHalfRight() {
			return Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 60, Camera.main.pixelHeight / 2 + 60));
		}

		public static Vector2 BottomRight() {
			return Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 20, 20));
		}

		public static Vector2 BottomHalfRight() {
			return Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 60, Camera.main.pixelHeight / 2 - 60));
		}

		public static Vector2 CenterRight() {
			return Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 60, Camera.main.pixelHeight / 2));
		}

		public static Vector2 Center() {
			return Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));
		}
	}
}
