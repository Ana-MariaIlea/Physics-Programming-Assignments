using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{	
	// This code sample demonstrates the projection of the green vector onto the red vector.
	//
	// The result is _projection (yellow), which is parallel to the red vector.
	//
	// Finally, the perpendicular vector (blue) is chosen to be perpendicular to red, such that
	//  projection + perpendicular = green vector.

	// TODO: In the Vec2 struct, implement the dot product.

	Arrow _green;
	Arrow _red;
	Arrow _projection;
	Arrow _perpendicular;

	public MyGame () : base(800, 600, false, false)
	{
		Vec2 start = new Vec2 (width/2, height/2);
		Vec2 zero = new Vec2 (0, 0);

		uint green = 0xff00ff00;
		uint red = 0xffff0000;
		uint yellow = 0xffffff00;
		uint blue = 0xff0000ff;

		_green = new Arrow (start, zero, 1, green, 4);
		AddChild (_green);
		_red = new Arrow (start, zero, 1, red, 4);
		AddChild (_red);

		_projection = new Arrow (start, zero, 1, yellow, 4);
		AddChild (_projection);
		_perpendicular = new Arrow (start, zero, 1, blue, 4);
		AddChild (_perpendicular);
	}


	void Update () 
	{
		bool somethingChanged = false;

		Vec2 mousePoint = new Vec2 (Input.mouseX, Input.mouseY);

		if (Input.GetMouseButton (0)) {
			_green.vector = mousePoint - _green.startPoint;
			somethingChanged = true;
		}

		if (Input.GetMouseButton (1)) {
			_red.vector = mousePoint - _red.startPoint;
			somethingChanged = true;
		}

		if (somethingChanged) {
			Vec2 unitVectorRed = _red.vector.Normalized ();

			//dot product of the green vector onto the UNIT red vector gives us the length of the projection:
			float vectorPLength = _green.vector.Dot (unitVectorRed);

			//length of projection times the UNIT red vector gives us the projection vector:
			_projection.vector = unitVectorRed * vectorPLength;

			// set the start point (tail) of the vector _perpendicular to the end point (head) of _projection:
			_perpendicular.startPoint = _projection.startPoint + _projection.vector;

			// Choose _perpendicular such that the (vector) sum of _perpendicular and _projection yields the green vector:
			_perpendicular.vector = _green.vector - _projection.vector;
		}
	}

	static void Main() 
	{
		new MyGame().Start();
	}
}