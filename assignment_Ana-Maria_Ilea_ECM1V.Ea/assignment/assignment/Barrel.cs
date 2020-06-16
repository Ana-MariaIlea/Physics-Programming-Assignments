using GXPEngine;
using System;

class Barrel : Sprite 
{
	public const int StaticBarrel = 0;
	public const int MovableBarrel = 1;
	private int _barrelStatus;
	//------------------------------------------------------------------------------------------
	//                                      constructor
	//------------------------------------------------------------------------------------------
	public Barrel(int barrelStatus) : base("assets/barrels/t34.png") 
	{
		_barrelStatus = barrelStatus;
		SetOrigin(width / 4, height / 2);
	}
	//------------------------------------------------------------------------------------------
	//                                      Update()
	//------------------------------------------------------------------------------------------
	public void Update()
	{
		handleBarrelRotation();
	}
	//------------------------------------------------------------------------------------------
	//                                      handleBarrelRotation()
	//------------------------------------------------------------------------------------------
	private void handleBarrelRotation()
	{
		if (_barrelStatus == MovableBarrel)
		{
			Vec2 mouseVector = new Vec2(Input.mouseX - parent.x, Input.mouseY - parent.y);
			float targetAngle = mouseVector.GetAngleDegrees() - parent.rotation;
			targetAngle = (targetAngle + 180) % 360 - 180;
			float distance = Math.Abs(targetAngle - rotation);

			if (!(Input.GetKey(Key.LEFT_SHIFT) || Input.GetKey(Key.RIGHT_SHIFT)))
			{
				rotateFast(targetAngle);
			}
			else
			{
				rotateSlow(targetAngle, distance);
			}
			if (rotation > 360f) rotation -= 360f;
		}
	}
	//------------------------------------------------------------------------------------------
	//                                      rotateFast()
	//------------------------------------------------------------------------------------------
	private void rotateFast(float targetAngle)
	{
		rotation = targetAngle;
	}
	//------------------------------------------------------------------------------------------
	//                                      rotateSlow()
	//------------------------------------------------------------------------------------------
	private void rotateSlow(float targetAngle, float distance)
	{
		if (distance > 180f && distance < 360f)
		{
			if (targetAngle > rotation + 0.5f)
			{
				rotation--;
			}
			else if (targetAngle < rotation - 0.5f)
			{
				rotation++;
			}
		}
		else
		{
			if (targetAngle > rotation + 0.5f)
			{
				rotation++;
			}
			else if (targetAngle < rotation - 0.5f)
			{
				rotation--;
			}
		}
	}
}


