using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Player : EasyDraw
{
    private Barrel _barrel;
    private MyGame myGame;
    private Platform currentline;

    private static float bounciness = 0f;
    public Vec2 acceleration = new Vec2(0, 1);


    public Vec2 velocity;
    public Vec2 position;
    private Vec2 _oldPosition;
    public Vec2 jump;

    private Vec2 friction;
    private Vec2 impactForce;
    public float gravity = 0.75f;

    public readonly int radius;
    public readonly bool moving;

    private float _speed = 0;
    public int ending = 0;
    private float smallestToi;
    private bool colWithLine = false;
    //------------------------------------------------------------------------------------------
    //                                      constructor
    //------------------------------------------------------------------------------------------
    public Player(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(), bool moving = true) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        radius = pRadius;
        position = pPosition;
        velocity = pVelocity;
        this.moving = moving;

        position = pPosition;
        UpdateScreenPosition();
        SetOrigin(radius, radius);

        Draw(230, 200, 0);

        jump = new Vec2(0, -15);
        currentline = new Platform(new Vec2(0, 0), new Vec2(0, 0), Platform.Basic, 0);
        _barrel = new Barrel(Barrel.MovableBarrel);
        _barrel.width /= 2;
        _barrel.height /= 2;
        AddChild(_barrel);
        myGame = (MyGame)game;

    }
    //------------------------------------------------------------------------------------------
    //                                      Draw()
    //------------------------------------------------------------------------------------------
    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(radius, radius, 2 * radius, 2 * radius);
    }
    //------------------------------------------------------------------------------------------
    //                                      UpdateScreenPosition()
    //------------------------------------------------------------------------------------------
    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }
    //------------------------------------------------------------------------------------------
    //                                      KeyControls()
    //------------------------------------------------------------------------------------------
    void KeyControls()
    {

        if (Input.GetKey(Key.D))
        {
            _speed = 1f;

        }
        else if (Input.GetKey(Key.A))
        {
            _speed = -1f;

        }

        _speed *= 0.9f;

        handleJump();

    }
    //------------------------------------------------------------------------------------------
    //                                      handleJump()
    //------------------------------------------------------------------------------------------
    private void handleJump()
    {
        if (Input.GetKeyDown(Key.W))
        {
            if (colWithLine == true)
            {
                velocity += jump;
                acceleration.SetXY(0, gravity);
                colWithLine = false;
            }
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      Step()
    //------------------------------------------------------------------------------------------
    public void Step()
    {
        Vec2 dir = (currentline.end - currentline.start).Normalized();
        dir *= _speed;
        if (currentline._inverted == false)
        {
            dir *= -1;

        }
        //dir *= -1;
        velocity += dir;
        calculateFriction();
        velocity += friction;
        velocity += acceleration;
        KeyControls();
        _oldPosition = position;
        position += velocity;
        collisionDetection();

        UpdateScreenPosition();
        invertGravity();
    }
    //------------------------------------------------------------------------------------------
    //                                      invertGravity()
    //------------------------------------------------------------------------------------------
    private void invertGravity()
    {
        if (position.x > 800 && position.x < 1000)
        {
            if (gravity != -0.5f)
                gravity -= 0.035f;
        }
        else { gravity = 0.75f; }
    }
    //------------------------------------------------------------------------------------------
    //                                      calculateFriction()
    //------------------------------------------------------------------------------------------
    private void calculateFriction()
    {
        friction = velocity * -1 * currentline._bounce;
    }
    //------------------------------------------------------------------------------------------
    //                                      collisionDetection(()
    //------------------------------------------------------------------------------------------
    public void collisionDetection()
    {
        CollisionInfo firstCollision = FindEarliestCollision();
        if (firstCollision != null)
        {
            handleColWithBullets(firstCollision);
            handleColWithEndPlatform(firstCollision);
            colWithLine = true;
            ResolveCollision(firstCollision);
        }
        else acceleration.SetXY(0, gravity);
    }
    //------------------------------------------------------------------------------------------
    //                                      handleColWithBullets()
    //------------------------------------------------------------------------------------------
    private void handleColWithBullets(CollisionInfo firstCollision)
    {
        if (firstCollision.other is Bullet)
        {
            if (firstCollision.timeOfImpact == 0f) { Step(); }
            Bullet b = firstCollision.other as Bullet;
            if (b._type == Bullet.IsBullet)
            {
                calculateImpactForce();
                velocity += impactForce;
                myGame._movers.Remove(b);
                b.LateDestroy();
            }
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      handleColWithEndPlatform()
    //------------------------------------------------------------------------------------------
    private void handleColWithEndPlatform(CollisionInfo firstCollision)
    {
        if (firstCollision.other is Platform)
        {
            Platform p = firstCollision.other as Platform;
            if (p._platformType == Platform.EndSim)
            {
                this.LateDestroy();
                ending = 1;
            }
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      calculateImpactForce()
    //------------------------------------------------------------------------------------------
    private void calculateImpactForce()
    {
        impactForce = velocity.Normalized() * -1 * 30;
    }
    //------------------------------------------------------------------------------------------
    //                                      FindEarliestCollision()
    //------------------------------------------------------------------------------------------
    public CollisionInfo FindEarliestCollision()
    {
        GameObject current = null;
        Vec2 currentNormal = new Vec2();
        smallestToi = 2f;
        BallBallCollisionDetection(ref current, ref currentNormal);
        BallSegmentCollisionDetection(ref current, ref currentNormal);

        if (current != null)
        {
            return new CollisionInfo(currentNormal, current, smallestToi);
        }
        else
        {
            return null;
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      BallSegmentCollisionDetection()
    //------------------------------------------------------------------------------------------
    private void BallSegmentCollisionDetection(ref GameObject current, ref Vec2 currentNormal)
    {
        for (int i = 0; i < myGame.GetNumberOfLines(); i++)
        {
            LineSegment line = myGame.GetLine(i);
            if (Input.GetKey(Key.S) && line.parent is Stairs) { continue; }
            Vec2 differenceVector = _oldPosition - line.start;
            float ballDistance = differenceVector.Dot((line.end - line.start).Normal());

            Vec2 normal = (line.end - line.start).Normal();
            Vec2 dist = position + (-ballDistance + radius) * normal;
            float difference = (dist - position).Magnitude();
            float b = -velocity.Dot(normal);
            float a = ballDistance - radius;

            if (b > 0)
            {
                float t = 3f;
                if (a >= 0)
                {
                    t = a / b;
                }
                else if (a >= -radius)
                {
                    t = 0;
                }
                else continue;
                if (t <= 1f)
                {
                    Vec2 POI = _oldPosition + t * velocity;
                    float d = (POI - line.start).Dot((line.end - line.start).Normalized());
                    if (0 <= d && d <= (line.end - line.start).Magnitude())
                    {
                        if (t < smallestToi)
                        {
                            smallestToi = t;
                            current = line;
                            currentNormal = normal;
                            currentline = (Platform)line;
                        }
                    }
                }

            }
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      BallBallCollisionDetection()
    //------------------------------------------------------------------------------------------
    private void BallBallCollisionDetection(ref GameObject current, ref Vec2 currentNormal)
    {
        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {

            Bullet mover = (Bullet)myGame.GetMover(i);
            Vec2 relativePosition = position - mover.position;

            Vec2 u = _oldPosition - mover.position;
            float a = velocity.Magnitude() * velocity.Magnitude();
            float b = 2 * u.Dot(velocity);
            float c = u.Magnitude() * u.Magnitude() - (radius + mover.radius) * (radius + mover.radius);
            float delta = b * b - 4 * a * c;
            float t;
            t = (-b - Mathf.Sqrt(delta)) / (2 * a);

            if (c < 0)
            {
                if (b < 0)
                {
                    t = 0;
                }
                else continue;
            }
            if (a != 0)
            {
                if (delta >= 0)
                {
                    Vec2 pointOfImpact = _oldPosition + velocity * t;
                    Vec2 normal = (pointOfImpact - mover.position).Normalized();
                    if (0 <= t && t < 1)
                    {
                        if (t < smallestToi)
                        {
                            smallestToi = t;
                            current = mover;
                            currentNormal = normal;
                        }
                    }
                }
            }
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      ResolveCollision()
    //------------------------------------------------------------------------------------------
    public void ResolveCollision(CollisionInfo col)
    {

        if (col.other is Platform)
        {

            Platform line = col.other as Platform;
            if (line._platformType == Platform.Gravity)
            {
                acceleration.SetXY(0, 0);
            }
            else if (line._platformType == Platform.Basic)
            {
                acceleration.SetXY(0, 0.75f);

            }

        }
        position = _oldPosition + col.timeOfImpact * velocity;
        velocity.Reflect(bounciness, col.normal);

    }
    //------------------------------------------------------------------------------------------
    //                                      Shoot()
    //------------------------------------------------------------------------------------------
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(button: 0))
        {
            Vec2 bulletVelocity = new Vec2(1, 0);
            bulletVelocity.RotateAroundDegrees(_barrel.rotation + this.rotation, new Vec2(0, 0));
            Vec2 bulletDirection = new Vec2(this.x, this.y);
            Vec2 delta = Vec2.GetUnitVectorDeg(_barrel.rotation + this.rotation);
            bulletDirection += delta * (_barrel.width / 3 * 2);
            Bullet bul;
            if ((Input.GetKey(Key.LEFT_SHIFT)))
            {
                bul = new Bullet(10, bulletDirection, bulletVelocity);
                bul.acceleration.SetXY(0, 0.5f);
                bul.velocity *= 5;

            }
            else bul = new Bullet(5, bulletDirection, bulletVelocity);

            bul.rotation = _barrel.rotation + this.rotation;
            game.AddChild(bul);
            myGame._movers.Add(bul);
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      Update()
    //------------------------------------------------------------------------------------------
    void Update()
    {
        Shoot();
    }
}



