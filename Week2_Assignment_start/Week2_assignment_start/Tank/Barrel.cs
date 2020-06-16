using GXPEngine;
using System;

class Barrel : Sprite 
{
	public Barrel() : base("assets/barrels/t34.png") 
	{
		SetOrigin(width / 4, height / 2);
	}

	public void Update()
	{
		Vec2 mouseVector = new Vec2(Input.mouseX - parent.x, Input.mouseY - parent.y);
		float targetAngle = mouseVector.GetAngleDegrees() - parent.rotation;
		targetAngle = (targetAngle + 180) % 360 - 180;
		float distance = Math.Abs(targetAngle- rotation);
		//if (distance > 180f && distance < 360f) distance -= 180f;

		if (!(Input.GetKey(Key.LEFT_SHIFT) || Input.GetKey(Key.RIGHT_SHIFT)))
		{ // Shift not pressed: Directly aim at mouse
			rotation = targetAngle;
		}
		else
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
		if (rotation > 360f) rotation -= 360f;
		
	}
}
