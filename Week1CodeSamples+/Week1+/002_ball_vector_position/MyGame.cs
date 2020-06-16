using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game
{
	Ball _ball;

	EasyDraw _text;

	public MyGame () : base(800, 600, false,false)
	{
		_ball = new Ball (30, new Vec2 (width / 2, height / 2));
		AddChild (_ball);

		_text = new EasyDraw (200,25);
		_text.TextAlign (CenterMode.Min, CenterMode.Min);
		AddChild (_text);
	}

	void Update () {
		_ball.Step ();

		_text.Clear (Color.Transparent);
		_text.Text("POS X,Y = " + _ball.x + "," + _ball.y, 0, 0);
	}

	static void Main()
	{
		new MyGame().Start();
	}
}