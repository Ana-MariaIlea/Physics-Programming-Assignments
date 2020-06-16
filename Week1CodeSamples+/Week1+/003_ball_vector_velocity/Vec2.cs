using System;

public struct Vec2 
{
	public float x;
	public float y;

	public Vec2 (float pX = 0, float pY = 0) 
	{
		x = pX;
		y = pY;
	}

	public static Vec2 operator+ (Vec2 left, Vec2 right) {
		return new Vec2(left.x+right.x, left.y+right.y);
	}
	public static Vec2 operator- (Vec2 left, Vec2 right)
	{
		return new Vec2(left.x - right.x, left.y - right.y);
	}

	public static Vec2 operator* (float scalar, Vec2 vector)
	{
		return new Vec2(scalar * vector.x, scalar * vector.y);
	}

	public static Vec2 operator *(Vec2 vector, float scalar)
	{
		return new Vec2(scalar * vector.x, scalar * vector.y);
	}

	public float Magnitude()
	{
		return (float)(Math.Sqrt(this.x * this.x + this.y * this.y));
	}


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


	public override string ToString () 
	{
		return String.Format ("({0},{1})", x, y);
	}
	public void SetXY(float newX, float newY)
	{
		this.x = newX;
		this.y = newY;
	}

	public static float Deg2Rad(float degrees)
	{
		return (float)(degrees / 180 * Math.PI);
	}
	public static float Rad2Deg(float radians)
	{
		return (float)(radians / Math.PI * 180);
	}

	public static Vec2 GetUnitVectorDeg(float degrees)
	{
		return new Vec2((float)(Math.Cos(degrees)), (float)(Math.Sin(degrees)));
	}
	public static Vec2 GetUnitVectorRad(float radians)
	{
		return new Vec2((float)(Math.Cos(radians)), (float)(Math.Sin(radians)));
	}

}

