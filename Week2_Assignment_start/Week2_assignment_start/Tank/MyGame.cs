using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{	

	static void Main() 
	{
		new MyGame().Start();
	}

	public MyGame () : base(800, 600, false,false)
	{
		// background:
		AddChild (new Sprite ("assets/desert.png"));
		// tank:
		AddChild (new Tank (width / 2, height / 2));

		TestVectorFunctions test = new TestVectorFunctions();
	}

}