using System;
using GXPEngine;

public class Bullet : EasyDraw
{
    public const int IsBullet = 0;
    public const int IsSegBall = 1;
    public int _type = 0;
    private MyGame myGame;

    public float bounciness = 1f;
    public Vec2 acceleration = new Vec2(0, 0);

    private int bounce = 0;

    public Vec2 velocity;
    public Vec2 position;
    private Vec2 _oldPosition;
    public float gravity = 1;

    public readonly int radius;
    public readonly bool moving;

    public float _speed = 0;
    float smallestToi;

    //------------------------------------------------------------------------------------------
    //                                      constructor
    //------------------------------------------------------------------------------------------
    public Bullet(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(), bool moving = true) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        radius = pRadius;
        position = pPosition;
        velocity = pVelocity;
        this.moving = moving;

        position = pPosition;
        UpdateScreenPosition();
        SetOrigin(radius, radius);

        Draw(230, 200, 0);

        myGame = (MyGame)game;
    }
    //------------------------------------------------------------------------------------------
    //                                      SetType()
    //------------------------------------------------------------------------------------------
    public void SetType(int Type)
    {
        _type = Type;
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
    //                                      Step()
    //------------------------------------------------------------------------------------------
    public void Step()
    {

        velocity += acceleration;
        _oldPosition = position;
        position += velocity;
        collisionDetection();

        UpdateScreenPosition();

    }
    //------------------------------------------------------------------------------------------
    //                                      collisionDetection()
    //------------------------------------------------------------------------------------------
    public void collisionDetection()
    {
        CollisionInfo firstCollision = FindEarliestCollision();

        if (firstCollision != null)
        {
            handleCollisionWithTurrets(firstCollision);
            handleCollisionWithStairsBall(firstCollision);
            handleCollisionWithBullets(firstCollision);

            if (acceleration.y == 0)
            {
                if (this != null)
                {
                    destroyBall();
                }

            }
            else
            {
                if (bounce < 2)
                {
                    ResolveCollision(firstCollision);
                    bounce++;
                }
                else
                {
                    destroyBall();
                }

            }
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      destroyBall()
    //------------------------------------------------------------------------------------------
    private void destroyBall()
    {
        this.LateDestroy();
        myGame._movers.Remove(this);
    }
    //------------------------------------------------------------------------------------------
    //                                      handleCollisionWithStairsBall()
    //------------------------------------------------------------------------------------------
    private void handleCollisionWithStairsBall(CollisionInfo firstCollision)
    {
        if (firstCollision.other.parent is Stairs && firstCollision.other is Bullet)
        {
            Stairs s = firstCollision.other.parent as Stairs;
            myGame._movers.Remove(s.ball);
            s.ball.LateDestroy();
            s.ball = null;
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      handleCollisionWithTurrets()
    //------------------------------------------------------------------------------------------
    private void handleCollisionWithTurrets(CollisionInfo firstCollision)
    {
        if (firstCollision.other.parent is Turret)
        {
            if (radius > 5)
            {
                Turret item = firstCollision.other.parent as Turret;
                item.RemoveLinesFromList();
                item.LateDestroy();
                destroyBall();

            }
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      handleCollisionWithBullets()
    //------------------------------------------------------------------------------------------
    private void handleCollisionWithBullets(CollisionInfo firstCollision)
    {
        if (firstCollision.other is Bullet)
        {
            Bullet b = firstCollision.other as Bullet;
            if (b._type == Bullet.IsBullet)
            {
                myGame._movers.Remove(b);
                b.LateDestroy();
            }
        }
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
            if (mover != this)
            {
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
    }
    //------------------------------------------------------------------------------------------
    //                                      ResolveCollision()
    //------------------------------------------------------------------------------------------
    public void ResolveCollision(CollisionInfo col)
    {
        position = _oldPosition + col.timeOfImpact * velocity;
        velocity.Reflect(bounciness, col.normal);
    }
    //------------------------------------------------------------------------------------------
    //                                      Update()
    //------------------------------------------------------------------------------------------
    void Update()
    {
        if (moving == true) { Step(); }
    }
}

