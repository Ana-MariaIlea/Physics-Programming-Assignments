using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;


public class MyGame : Game
{	
	static void Main() {
		new MyGame().Start();
	}

	List<Ball> balls;
	int counter=0;

	bool Newton = false;
	bool wordy = true;

	public MyGame () : base(800, 600, false,false)
	{
		balls = new List<Ball> ();
	}

	void SpawnBall() {
		counter++;
		if (counter >= balls.Count * 5) {
			counter = 0;
			Ball newBall = new Ball (Utils.Random (20, 70), new Vec2 (Utils.Random (100, 700), Utils.Random (100, 500)));
			AddChild (newBall);
			balls.Add (newBall);
		}
	}

	void ResolveOverlaps() {
		for (int i = 0; i < balls.Count; i++) {
			balls [i].velocity = new Vec2 (0, 0);
		}
		for (int i = 0; i < balls.Count-1; i++) {
			for (int j = i + 1; j < balls.Count; j++) {
				
				Vec2 difference = balls [i].position - balls [j].position; 

				float distance = difference.Length ();
				float minDistance = balls [i].radius + balls [j].radius;

				if (minDistance > distance) {
					if (wordy) {
						Console.WriteLine("Current distance: {0} should be at least: {1}", distance, minDistance);
					}

					// This is the unit collision normal:
					difference.Normalize ();

					if (!Newton) {
						// both balls move an equal amount, regardless of mass:
						balls [i].velocity += difference * 0.5f;
						balls [j].velocity -= difference * 0.5f;
					} else {
						// conservation of momentum - the smaller ball moves more:
						balls [i].velocity += difference * (balls [j].Mass / (balls [i].Mass + balls [j].Mass));
						balls [j].velocity -= difference * (balls [i].Mass / (balls [i].Mass + balls [j].Mass));
					}
				}
			}
		}
	}

	void MoveBalls() {
		for (int i = 0; i < balls.Count; i++) {
			balls [i].Step ();
		}
	}

	void KeyInput() {
		if (Input.GetKeyDown(Key.N)) {
			Newton=!Newton;
			Console.WriteLine("Using Newton's laws (conservation of momentum: "+Newton);
		}
		if (Input.GetKeyDown(Key.W)) {
			wordy = !wordy;
		}
	}

	void Update () {
		KeyInput();
		SpawnBall ();
		ResolveOverlaps ();
		MoveBalls ();
	}
}