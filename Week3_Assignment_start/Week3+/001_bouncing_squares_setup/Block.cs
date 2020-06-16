using System;
using GXPEngine;


public class Block : EasyDraw
{
	/******* PUBLIC FIELDS AND PROPERTIES *********************************************************/

	// These four public static fields are changed from MyGame, based on key input (see Console):
	public static bool drawDebugLine = false;
	public static bool wordy = false;
	public static float bounciness = 0.98f;
	// For ease of testing / changing, we assume every block has the same acceleration (gravity):
	public static Vec2 acceleration = new Vec2 (0, 0);

	public readonly int radius;

	// Mass = density * volume.
	// In 2D, we assume volume = area (=all objects are assumed to have the same "depth")
	public float Mass {
		get {
			return 4 * radius * radius * _density;
		}
	}

	public Vec2 position {
		get {
			return _position;
		}
	}

	public Vec2 velocity;

	/******* PRIVATE FIELDS *******************************************************************/

	Vec2 _position;
	Vec2 _oldPosition;

	Arrow _velocityIndicator;

	float _red = 1;
	float _green = 1;
	float _blue = 1;

	float _density = 1;

	const float _colorFadeSpeed = 0.025f;

	/******* PUBLIC METHODS *******************************************************************/

	public Block (int pRadius, Vec2 pPosition, Vec2 pVelocity) : base (pRadius*2, pRadius*2)
	{
		radius = pRadius;
		_position = pPosition;
		velocity = pVelocity;

		SetOrigin (radius, radius);
		draw ();
		UpdateScreenPosition();
		_oldPosition = new Vec2(0,0);

		_velocityIndicator = new Arrow(_position,velocity, 10);
		AddChild(_velocityIndicator);
	}

	public void SetFadeColor(float pRed, float pGreen, float pBlue) {
		_red = pRed;
		_green = pGreen;
		_blue = pBlue;
	}

	public void Update() {
		// For extra testing flexibility, we call the Step method from MyGame instead:
		//Step();
	}

	public void Step() {
		_oldPosition=_position;

		// No need to make changes in this Step method (most of it is related to drawing, color and debug info). 
		// Work in Move instead.
		Move();

		UpdateColor();
		UpdateScreenPosition();
		ShowDebugInfo();
	}

	/******* PRIVATE METHODS *******************************************************************/

	/******* THIS IS WHERE YOU SHOULD WORK: ***************************************************/

	void Move() {
		// TODO: implement Assignment 3 here (and in methods called from here).
		velocity += acceleration;
		_position += velocity;

		// Example methods (replace/extend):
		CheckBoundaryCollisions();
		CheckBlockOverlaps();

		// TIP: You can use the CollisionInfo class to pass information between methods, e.g.:
		//
		//Collision firstCollision=FindEarliestCollision();
		//if (firstCollision!=null)
		//	ResolveCollision(firstCollision);
	}

	// This method is just an example of how to check boundaries, and change color.
	void CheckBoundaryCollisions() {
		MyGame myGame = (MyGame)game;
		if (_position.x - radius < myGame.LeftXBoundary) {
			// move block from left to right boundary:
			//_position.x += myGame.RightXBoundary - myGame.LeftXBoundary - 2 * radius;
			//_position.x = myGame.LeftXBoundary + 2 * radius;		//assignment 1
			pointOfImpactX(myGame.LeftXBoundary + radius);
			velocity = -bounciness * velocity;
			SetFadeColor(1, 0.2f, 0.2f);
			if (wordy) {
				Console.WriteLine ("Left boundary collision");
			}
		} else if (_position.x + radius > myGame.RightXBoundary) {
			// move block from right to left boundary:

			//_position.x -= myGame.RightXBoundary - myGame.LeftXBoundary - 2 * radius;
			//_position.x = myGame.RightXBoundary - 2*radius;		//assignment 1
			pointOfImpactX(myGame.RightXBoundary - radius);
			velocity = -bounciness * velocity;
			SetFadeColor(1, 0.2f, 0.2f);
			if (wordy) {
				Console.WriteLine ("Right boundary collision");
			}
		}

		if (_position.y - radius < myGame.TopYBoundary) {
			// move block from top to bottom boundary:
			//_position.y += myGame.BottomYBoundary - myGame.TopYBoundary - 2 * radius;
			//_position.y = myGame.TopYBoundary + 2 * radius;		//assignment 1
			pointOfImpactY(myGame.TopYBoundary + radius);
			velocity.y = -bounciness * velocity.y;
			SetFadeColor(0.2f, 1, 0.2f);
			if (wordy) {
				Console.WriteLine ("Top boundary collision");
			}
		} else if (_position.y + radius > myGame.BottomYBoundary) {
			// move block from bottom to top boundary:
			//_position.y -= myGame.BottomYBoundary - myGame.TopYBoundary - 2 * radius;
			//_position.y = myGame.BottomYBoundary - 2 * radius;		//assignment 1
			pointOfImpactY(myGame.BottomYBoundary -  radius);
			velocity.y = -bounciness * velocity.y;
			SetFadeColor(0.2f, 1, 0.2f);
			if (wordy) {
				Console.WriteLine ("Bottom boundary collision");
			}
		}
	}


	void pointOfImpactX(float newCoordonate)
	{
		float impactX = newCoordonate;
		float a = Math.Abs(impactX - _oldPosition.x);
		float b = Math.Abs(_position.x - _oldPosition.x);
		float t = a / b;

		Vec2 point = _oldPosition + t * velocity;
		_position = point;
	}

	void pointOfImpactY(float newCoordonate)
	{
		float impactY = newCoordonate;
		float a = Math.Abs(impactY - _oldPosition.y);
		float b = Math.Abs(_position.y - _oldPosition.y);
		float t = a / b;

		Vec2 point = _oldPosition + t * velocity;
		_position = point;
	}

	float timeOfImpactX(float newCoordonate)
	{
		float impactX = newCoordonate;
		float a = impactX - _oldPosition.x;
		float b = _position.x - _oldPosition.x;
		float t = a / b;

		return t;
	}
	float timeOfImpactY(float newCoordonate)
	{
		float impactY = newCoordonate;
		float a = impactY - _oldPosition.y;
		float b = _position.y - _oldPosition.y;
		float t = a / b;

		return t;
	}
	
	// This method is just an example of how to get information about other blocks in the scene.
	void CheckBlockOverlaps()
	{
		Block currentBlock = getEarliestCollision();
		if (currentBlock != null) resetPosition(currentBlock);

	}
	float smallestTOI;
	private Block getEarliestCollision()
	{
		MyGame myGame = (MyGame)game;
		smallestTOI = 2f;

		Block currentBlock = null;
		for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
		{
			Block other = myGame.GetMover(i);
			if (other != this)
			{
				// TODO: improve hit test, move to method:
				if (isOverlapping(other))
				{
					float toi = claculateTOI(other);
					currentBlock = setSmallestTOI(currentBlock, other, toi);
				}

			}
		}

		return currentBlock;
	}

	private Block setSmallestTOI(Block currentBlock, Block other, float toi)
	{
		if (toi < smallestTOI)
		{
			smallestTOI = toi;
			currentBlock = other;
		}

		return currentBlock;
	}

	private bool isMovingHorizontallly=false;
	private float claculateTOI(Block other)
	{
		float toiX = 2f;
		float toiY = 2f;
		if (velocity.x > 0)
		{
			toiX = timeOfImpactX(other.position.x - other.width / 2 - width / 2);
		}
		else
		{
			toiX = timeOfImpactX(other.position.x + other.width / 2 + width / 2);
		}
		if (velocity.y > 0)
		{
			toiY = timeOfImpactY(other.position.y - other.height / 2 - height / 2);
		}
		else
		{
			toiY = timeOfImpactY(other.position.y + other.height / 2 + height / 2);
		}
		if (velocity.x == 0f)
		{
			toiX = toiY;
		}
		if (velocity.y == 0f)
		{
			toiY = toiX;
		}
		if (toiX > toiY)
		{
			isMovingHorizontallly = true;
			return toiX;
		}
		else
		{
			isMovingHorizontallly = false;
			return  toiY;
		}
	}

	private bool isOverlapping(Block other)
	{
		return _position.x + width / 2 >= other.position.x - other.width / 2 &&
							_position.x - width / 2 <= other.position.x + other.width / 2 &&
							_position.y - height / 2 <= other.position.y + other.height / 2 &&
							_position.y + height / 2 >= other.position.y - other.height / 2;
	}

	private void resetPosition(Block other)
	{
		if (isNotMovingInTheSameDirection(other,isMovingHorizontallly))
		{
			chengeVelocityAndSetPosition(other);
		}


		SetFadeColor(0.2f, 0.2f, 1);
		other.SetFadeColor(0.2f, 0.2f, 1);
		if (wordy)
		{
			Console.WriteLine("Block-block overlap detected.");
		}
	}

	private void chengeVelocityAndSetPosition(Block other)
	{
		Vec2 u = (Mass * velocity + other.Mass * other.velocity) * (1 / (Mass + other.Mass));
		velocity = u - bounciness * (velocity - u);
		other.velocity = u - bounciness * (other.velocity - u);
		_position = _oldPosition + smallestTOI * velocity;
	}

	private bool isNotMovingInTheSameDirection(Block other, bool direction)
	{
		//return (!((velocity.x > 0 && other.velocity.x > 0 && velocity.x < other.velocity.x) ||
		//			(velocity.x < 0 && other.velocity.x < 0 && velocity.x > other.velocity.x))&&
		//			!((velocity.y > 0 && other.velocity.y > 0 && velocity.y < other.velocity.y) ||
		//			(velocity.y < 0 && other.velocity.y < 0 && velocity.y > other.velocity.y)));
		if (direction)
		{
			return !((position.x - other.position.x > 0 && velocity.x - other.velocity.x > 0) ||
				   (position.x - other.position.x < 0 && velocity.x - other.velocity.x < 0));
		}
		else
		{
			return !(position.y - other.position.y > 0 && velocity.y - other.velocity.y > 0) ||
				   (position.y - other.position.y < 0 && velocity.y - other.velocity.y < 0);
		}
	}

	/******* NO NEED TO CHANGE ANY OF THE CODE BELOW: **********************************************/

	void UpdateColor() {
		if (_red < 1) {
			_red = Mathf.Min (1, _red + _colorFadeSpeed);
		}
		if (_green < 1) {
			_green = Mathf.Min (1, _green + _colorFadeSpeed);
		}
		if (_blue < 1) {
			_blue = Mathf.Min (1, _blue + _colorFadeSpeed);
		}
		SetColor(_red, _green, _blue);
	}

	void ShowDebugInfo() {
		if (drawDebugLine) {
			((MyGame)game).DrawLine (_oldPosition, _position);
		}
		_velocityIndicator.startPoint = _position;
		_velocityIndicator.vector = velocity;
	}

	void UpdateScreenPosition() {
		x = _position.x;
		y = _position.y;
	}

	void draw() {
		Fill (200);
		NoStroke ();
		ShapeAlign (CenterMode.Min, CenterMode.Min);
		Rect (0, 0, width, height);
	}
}
