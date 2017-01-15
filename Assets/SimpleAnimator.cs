﻿using UnityEngine;

using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

[RequireComponent (typeof(SpriteRenderer))]
public class SimpleAnimator : MonoBehaviour {

	[System.Serializable]
	public class Anim {
		public string name;
		public Sprite[] frames;
		public float framesPerSec = 5;
		public bool loop = true;

		public float duration {
			get {
				return frames.Length * framesPerSec;
			}
			set {
				framesPerSec = value / frames.Length;
			}
		}
	}

	public List<Anim> animations = new List<Anim>();

	[HideInInspector]
	public int currentFrame;

	[HideInInspector]
	public bool done {
		get {
			return currentFrame >= current.frames.Length;
		}
	}

	[HideInInspector]
	public bool playing {
		get {
			return _playing;
		}
	}

	SpriteRenderer spriteRenderer;
	Anim current;
	bool _playing;
	float secsPerFrame;
	float nextFrameTime;

	[ContextMenu ("Sort All Frames by Name")]
	void DoSort() {
		foreach (Anim anim in animations) {
			System.Array.Sort(anim.frames, (a,b) => a.name.CompareTo(b.name));
		}
		Debug.Log(gameObject.name + "animation frames have been sorted alphabetically.");
	}

	void Start () {
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		if (spriteRenderer == null) Debug.Log (gameObject.name + ": Couldn't find SpriteRenderer");

		if (animations.Count > 0) PlayByIndex (0);
	}
	
	void Update () {
		if (!_playing || Time.time < nextFrameTime || spriteRenderer == null) return;

		currentFrame++;

		if (currentFrame >= current.frames.Length) {
			if (!current.loop) {
				_playing = false;
				return;
			}
			else {
				currentFrame = 0;
			}
		}

		spriteRenderer.sprite = current.frames [currentFrame];
		nextFrameTime += secsPerFrame;
	}

	public void Play(string name) {
		int index = animations.FindIndex (a => a.name == name);

		if (index < 0) {
			Debug.LogError (gameObject + "No such animation: " + name);
		}
		else {
			PlayByIndex (index);
		}
	}

	public void PlayByIndex(int index) {
		if (index < 0) return;

		Anim anim = animations[index];

		current = anim;

		secsPerFrame = 1f / anim.framesPerSec;
		currentFrame = -1;
		_playing = true;
		nextFrameTime = Time.time;
	}

	public void Stop() {
		_playing = false;
	}

	public void Resume() {
		_playing = true;
		nextFrameTime = Time.time + secsPerFrame;
	}
}
