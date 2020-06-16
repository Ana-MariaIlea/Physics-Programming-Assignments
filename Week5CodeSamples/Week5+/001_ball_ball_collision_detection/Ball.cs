using System;
using GXPEngine;

public class Ball : EasyDraw
{
	public Vec2 velocity;
	public Vec2 position {
		get {
			return _position;
		}
	}
	public float Mass {
		get {
			return radius * radius;
		}
	}

	public readonly int radius;
	Vec2 _position;

	public Ball (int pRadius, Vec2 pPosition) : base (pRadius*2 + 1, pRadius*2 + 1)
	{
		radius = pRadius;
		_position = pPosition;

		UpdateScreenPosition ();
		SetOrigin (radius, radius);

		Draw ((byte)Utils.Random (1, 255), (byte)Utils.Random (1, 255), (byte)Utils.Random (1, 255));
	}

	void Draw(byte red, byte green, byte blue) {
		Fill (red, green, blue);
		Stroke (red, green, blue);
		Ellipse (radius, radius, 2*radius, 2*radius);
	}

	void UpdateScreenPosition() {
		x = _position.x;
		y = _position.y;
	}

	public void Step () {
		_position += velocity;
		UpdateScreenPosition ();
	}
}
