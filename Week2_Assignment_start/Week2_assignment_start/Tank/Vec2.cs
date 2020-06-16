using System;
using GXPEngine; // Allows using Mathf functions

public struct Vec2 
{
	public float x;
	public float y;

	public Vec2(float pX = 0, float pY = 0)
	{
		x = pX;
		y = pY;
	}
	//-----------------------------------------------------------
	//						operatior +
	//-----------------------------------------------------------
	public static Vec2 operator +(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x + right.x, left.y + right.y);
	}
	//-----------------------------------------------------------
	//						operatior -
	//-----------------------------------------------------------
	public static Vec2 operator -(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x - right.x, left.y - right.y);
	}
	//-----------------------------------------------------------
	//						operatior * LEFT
	//-----------------------------------------------------------

	public static Vec2 operator *(float scalar, Vec2 vector)
	{
		return new Vec2(scalar * vector.x, scalar * vector.y);
	}
	//-----------------------------------------------------------
	//						operatior * RIGHT
	//-----------------------------------------------------------

	public static Vec2 operator *(Vec2 vector, float scalar)
	{
		return new Vec2(scalar * vector.x, scalar * vector.y);
	}
	//-----------------------------------------------------------
	//						magnitude
	//-----------------------------------------------------------

	public float Magnitude()
	{
		return (float)(Math.Sqrt(this.x * this.x + this.y * this.y));
	}

	//-----------------------------------------------------------
	//						normalize
	//-----------------------------------------------------------
	public void Normalize()
	{
		float magnitude = Magnitude();
		if (magnitude != 0)
		{
			this.x /= magnitude;
			this.y /= magnitude;
		}
		else
		{
			this.x = 0;
			this.x = 0;
		}
	}
	//-----------------------------------------------------------
	//						normalized vector
	//-----------------------------------------------------------

	public Vec2 Normalized()
	{
		float magnitude = Magnitude();
		if (magnitude != 0)
		{
			return new Vec2(this.x / Magnitude(), this.y / Magnitude());
		}
		else
		{
			return new Vec2(0, 0);
		}
	}
	//-----------------------------------------------------------
	//						toString
	//-----------------------------------------------------------

	public override string ToString()
	{
		return String.Format("({0},{1})", x, y);
	}

	//-----------------------------------------------------------
	//						SetXY
	//-----------------------------------------------------------
	public void SetXY(float newX, float newY)
	{
		this.x = newX;
		this.y = newY;
	}
	//-----------------------------------------------------------
	//						deg2rad
	//-----------------------------------------------------------
	public static float Deg2Rad(float degrees)
	{
		return (float)(degrees / 180 * Math.PI);
	}
	//-----------------------------------------------------------
	//						red2deg
	//-----------------------------------------------------------
	public static float Rad2Deg(float radians)
	{
		return (float)(radians / Math.PI * 180);
	}
	//-----------------------------------------------------------
	//						GetUnitVectorDeg 
	//-----------------------------------------------------------
	public static Vec2 GetUnitVectorDeg(float degrees)
	{
		return GetUnitVectorRad(Deg2Rad(degrees));
	}
	//-----------------------------------------------------------
	//						GetUnitVectorRad 
	//-----------------------------------------------------------
	public static Vec2 GetUnitVectorRad(float radians)
	{
		return new Vec2((float)(Math.Cos(radians)), (float)(Math.Sin(radians)));
	}
	//-----------------------------------------------------------
	//						RandomUnitVector 
	//-----------------------------------------------------------
	public static Vec2 RandomUnitVector()
	{
		float randomAngle = Utils.Random(0, 360);
		return GetUnitVectorRad(Deg2Rad(randomAngle));
	}
	//-----------------------------------------------------------
	//						SetAngleDegrees 
	//-----------------------------------------------------------
	public void SetAngleDegrees(float degrees)
	{
		SetAngleRadians(Deg2Rad(degrees));
	}
	//-----------------------------------------------------------
	//						SetAngleRadians
	//-----------------------------------------------------------
	public void SetAngleRadians(float radians)
	{
		float currentMagnitude = this.Magnitude();
		this.x = currentMagnitude * Mathf.Cos(radians);
		this.y = currentMagnitude * Mathf.Sin(radians);
	}
	//-----------------------------------------------------------
	//						GetAngleDegrees 
	//-----------------------------------------------------------
	public float GetAngleDegrees()
	{
		return Rad2Deg(Mathf.Atan2(this.y, this.x));
	}
	//-----------------------------------------------------------
	//						GetAngleRadians 
	//-----------------------------------------------------------
	public float GetAngleRadians()
	{
		return Mathf.Atan2(this.y, this.x);
	}
	//-----------------------------------------------------------
	//						RotateDegrees 
	//-----------------------------------------------------------
	public void RotateDegrees(float degrees)
	{
		RotateRadians(Deg2Rad(degrees));
	}
	//-----------------------------------------------------------
	//						RotateRadians 
	//-----------------------------------------------------------
	public void RotateRadians(float radians)
	{
		float sinAngle = Mathf.Sin(radians);
		float cosAngle = Mathf.Cos(radians);
		float oldX = this.x;
		this.x = this.x * cosAngle - this.y * sinAngle;
		this.y = oldX * sinAngle + this.y * cosAngle;
	}
	//-----------------------------------------------------------
	//						RotateAroundDegrees 
	//-----------------------------------------------------------
	public void RotateAroundDegrees(float degrees, Vec2 point)
	{
		this.x -= point.x;
		this.y -= point.y;
		RotateDegrees(degrees);
		this.x += point.x;
		this.y += point.y;
	}
	//-----------------------------------------------------------
	//						RotateAroundRadians 
	//-----------------------------------------------------------
	public void RotateAroundRadians(float radians, Vec2 point)
	{
		this.x -= point.x;
		this.y -= point.y;
		RotateRadians(radians);
		this.x += point.x;
		this.y += point.y;
	}
}