using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{	

	static void Main() {
		new MyGame().Start();
	}

	Ball _ball;

	EasyDraw _text;
	Timer timer;

	public MyGame () : base(800, 600, false,false)
	{
		timer = new Timer();
		AddChild(timer);
		_ball = new Ball(30, new Vec2(width / 2, height / 2));
		AddChild(_ball);

		_text = new EasyDraw(500, 25);
		_text.TextAlign(CenterMode.Min, CenterMode.Min);
		AddChild(_text);

		TestVectorFunction test = new TestVectorFunction();
		
	}

	void Update()
	{
		
		_ball.Step();
		if (_ball.gameOver == true) timer.PauseTime();
		

		_text.Clear(Color.Transparent);
		_text.Text("Velocity: " + _ball.velocity, 0, 0);
	}

}

