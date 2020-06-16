using System;
using GXPEngine;

// TODO: Fix this mess! - see Assignment 2.2
class Tank : Sprite 
{
	// public fields & properties:
	public Vec2 position 
	{
		get 
		{
			return _position;
		}
	}
	public Vec2 velocity;

	// private fields:
	Vec2 _position;
	Barrel _barrel;
	float _speed=0f;

	public Tank(float px, float py) : base("assets/bodies/t34.png") 
	{
		_position.x = px;
		_position.y = py;
		_barrel = new Barrel ();
		AddChild (_barrel);
		SetOrigin(width / 2, height / 2);
	}

	void Controls() 
	{
		if (Input.GetKey(Key.LEFT))
		{
			rotation-=0.5f*_speed;

		}
		if (Input.GetKey(Key.RIGHT))
		{
			rotation+= 0.5f*_speed;
		}
		//velocity.RotateDegrees(rotation);
		//Console.WriteLine();
		//if (Input.GetKey (Key.UP)) 
		//{
		//	velocity += new Vec2 (0.1f, 0);
		//}
		//if (Input.GetKey (Key.DOWN)) 
		//{
		//	velocity += new Vec2 (-0.1f, 0);
		//}


		if (Input.GetKey(Key.UP))
		{
			//velocity.x += _speed;
			_speed += 1f;

		}
		if (Input.GetKey(Key.DOWN))
		{
			//velocity.x -= _speed;
			_speed -= 1f;
		}

		velocity = Vec2.GetUnitVectorDeg(rotation) * _speed;
		_speed *= 0.9f;
	}


	void Shoot() {
		if (Input.GetMouseButtonDown(button:0)) 
		{
			Vec2 bulletVelocity = new Vec2(1, 0);
			bulletVelocity.RotateAroundDegrees(_barrel.rotation+this.rotation, new Vec2(0, 0));
			Vec2 bulletDirection = new Vec2(this.x , this.y);
			Vec2 delta = Vec2.GetUnitVectorDeg(_barrel.rotation + this.rotation);
			bulletDirection += delta*(_barrel.width/3*2);
			Bullet bul = new Bullet(bulletDirection, bulletVelocity);
			bul.rotation = _barrel.rotation + this.rotation;
			game.AddChild (bul);
		}
	}

	void UpdateScreenPosition() 
	{
		x = _position.x;
		y = _position.y;
	}

	public void Update() 
	{
		Controls ();
		// Basic Euler integration:
		_position += velocity;
		Shoot ();
		UpdateScreenPosition ();
	}
}
