using UnityEngine;
using System.Collections;

public class TestBossAI : AICommands
{

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}

	protected override void DefineStates () {
		// move down and shoot, shoot missiles, move up and shoot
		AddState(
			First(MoveTo(Location.TopRight))
			.Then(Wait(2))
			.Then(Shoot).And(MoveTo(Location.BottomRight))
			.Then(Wait(2))
			.Then(SwitchProjectile(1))
			.Then(Shoot).For(2)
			.Then(SwitchProjectile(0))
			.Then(Wait(2))
			.Then(Shoot).And(MoveTo(Location.TopRight))
			.Then(Wait(2))
			.End()
		);

		// slight move (uppper right), spin, slight move (center), spin, slight move (lower right), spin
		AddState(
			First(MoveTo(Location.TopRight))
			.Then(Wait(2))
			.Then(Shoot).And(Rotate(360 * 2))
			.Then(Wait(2))
			.Then(MoveTo(Location.Center))
			.Then(Wait(2))
			.Then(Shoot).And(Rotate(360 * 2))
			.Then(Wait(2))
			.Then(MoveTo(Location.BottomRight))
			.Then(Wait(2))
			.Then(Shoot).And(Rotate(360 * 2))
			.Then(Wait(2))
			.End()
		);

		// slight rotations while shooting
		AddState(
			First(MoveTo(Location.CenterRight))
			.Then(Wait(2))
			.Then(Shoot).And(Rotate(15, 3))
			.Then(Shoot).And(Rotate(-30, 3))
			.Then(Shoot).And(Rotate(15, 3))
			.Then(MoveTo(Location.TopHalfRight))
			.Then(Shoot).And(Rotate(15, 3))
			.Then(Shoot).And(Rotate(-30, 3))
			.Then(Shoot).And(Rotate(15, 3))
			.Then(MoveTo(Location.BottomHalfRight))
			.Then(Shoot).And(Rotate(15, 3))
			.Then(Shoot).And(Rotate(-30, 3))
			.Then(Shoot).And(Rotate(15, 3))
			.Then(MoveTo(Location.CenterRight))
			.Then(Wait(2))
			.End()
		);
	}

}
