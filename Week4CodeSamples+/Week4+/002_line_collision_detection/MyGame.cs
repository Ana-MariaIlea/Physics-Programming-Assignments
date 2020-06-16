using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{	

	static void Main() {
		new MyGame().Start();
	}

	Ball _ball;

	EasyDraw _text;

	NLineSegment _lineSegment1;
	NLineSegment _lineSegment2;
	NLineSegment _lineSegment3;

	List<NLineSegment> lines;

	public MyGame () : base(800, 600, false,false)
	{
		_ball = new Ball (30, new Vec2 (width / 2, height / 2));
		AddChild (_ball);

		_text = new EasyDraw (250,25);
		_text.TextAlign (CenterMode.Min, CenterMode.Min);
		AddChild (_text);
		lines = new List<NLineSegment>();

		_lineSegment1 = new NLineSegment (new Vec2 (500, 500), new Vec2 (100, 200), 0xff00ff00, 3);
		_lineSegment2 = new NLineSegment(new Vec2(800, 100), new Vec2(500, 600), 0xff00ff00, 3);
		_lineSegment3 = new NLineSegment(new Vec2(50, 250), new Vec2(800, 100), 0xff00ff00, 3);

		AddChild(_lineSegment1);
		AddChild(_lineSegment2);
		AddChild(_lineSegment3);

		lines.Add(_lineSegment1);
		lines.Add(_lineSegment2);
		lines.Add(_lineSegment3);

		Vec2 testVelocity = new Vec2(2, -11);
		Vec2 testNormal = new Vec2(0.8f, 0.6f);
		testVelocity.Reflect(1, testNormal);
		Console.WriteLine(testVelocity.x == 10f && testVelocity.y == -5f);

	}
	
	float currentDist;
	float degrees = 1f;
	void Update ()
	{
		// For now: this just puts the ball at the mouse position:
		_ball.Step();
		
		NLineSegment line=getEarlyestCol();


		//TODO: calculate correct distance from ball center to line


		//compare distance with ball radius
		if (line!=null)
		{
			ballReset(line);
		}
		else
		{
			_ball.SetColor(0, 1, 0);
		}


		_text.Clear(Color.Transparent);
		_text.Text("Distance to line: " + currentDist, 0, 0);
	}
	NLineSegment currentLine;
	private NLineSegment getEarlyestCol()
	{
		currentLine = null;
		float smallestDistance = 50f;
		for (int i = 0; i < lines.Count; i++)
		{
			Vec2 a = _ball.position - lines[i].start;
			float ballDistance = a.Dot((lines[i].end-lines[i].start).Normal());
			if (ballDistance < _ball.radius)
			{
				_ball.SetColor(1, 0, 0);
				if (ballDistance < smallestDistance)
				{
					currentLine = lines[i];
					currentDist = ballDistance;
				}
			}
		}
		return currentLine;
	}

	private void ballReset(NLineSegment currentLine)
	{
			_ball.x += (-currentDist + _ball.radius) * (currentLine.end-currentLine.start).Normal().x;
			_ball.y += (-currentDist + _ball.radius) * (currentLine.end - currentLine.start).Normal().y;
			_ball.velocity.Reflect(1, (currentLine.end - currentLine.start).Normal());
	}
}

