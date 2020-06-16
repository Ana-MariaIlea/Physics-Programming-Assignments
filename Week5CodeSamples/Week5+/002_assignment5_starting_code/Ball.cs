using System;
using GXPEngine;

public class Ball : EasyDraw
{
    // These four public static fields are changed from MyGame, based on key input (see Console):
    public static bool drawDebugLine = false;
    public static bool wordy = false;
    public static float bounciness = 0.98f;
    // For ease of testing / changing, we assume every ball has the same acceleration (gravity):
    public static Vec2 acceleration = new Vec2(0, 0);


    public Vec2 velocity;
    public Vec2 position;

    public readonly int radius;
    public readonly bool moving;

    // Mass = density * volume.
    // In 2D, we assume volume = area (=all objects are assumed to have the same "depth")
    public float Mass
    {
        get
        {
            return radius * radius * _density;
        }
    }

    Vec2 _oldPosition;
    Arrow _velocityIndicator;

    float _density = 1;

    public Ball(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(), bool moving = true) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        radius = pRadius;
        position = pPosition;
        velocity = pVelocity;
        this.moving = moving;

        position = pPosition;
        UpdateScreenPosition();
        SetOrigin(radius, radius);

        Draw(230, 200, 0);

        _velocityIndicator = new Arrow(position, new Vec2(0, 0), 10);
        AddChild(_velocityIndicator);
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(radius, radius, 2 * radius, 2 * radius);
    }

    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }

    public void Step()
    {
        velocity += acceleration;
        collisionDetection();

        UpdateScreenPosition();

        ShowDebugInfo();
    }

    void collisionDetection()
    {
        _oldPosition = position;
        position += velocity;
        // This can be removed after adding line segment collision detection:
        //BoundaryWrapAround();

        CollisionInfo firstCollision = FindEarliestCollision();
        if (firstCollision != null)
        {
            ResolveCollision(firstCollision);
            if (firstCollision.timeOfImpact == 0f) { collisionDetection(); }

        }
    }

    float smallestToi;

    CollisionInfo FindEarliestCollision()
    {
        GameObject current = null;
        Vec2 currentNormal = new Vec2();
        smallestToi = 2f;
        MyGame myGame = (MyGame)game;
        // Check other movers:			
        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {
            Ball mover = myGame.GetMover(i);
            if (mover != this)
            {
                Vec2 relativePosition = position - mover.position;
                //if (relativePosition.Magnitude() < radius + mover.radius)
                //{
                // TODO: compute correct normal and time of impact, and 
                // return *earliest* collision instead of *first detected collision*:
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
                        //Vec2 normal = (_oldPosition - mover.position).Normalized();
                        //return new CollisionInfo(normal, mover, t);
                    }
                    else continue;
                }

                if (a != 0)
                //{
                //    return null;
                //}
                //else
                {

                    if (delta >= 0)
                    //{
                    //    return null;
                    //}
                    //else
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
                        //else return null;
                    }
                }
                //Vec2 normal = relativePosition.Normalized();
                //float overlap = radius + mover.radius - relativePosition.Magnitude();
                //return new CollisionInfo(normal, mover, overlap);


                //}
            }
        }

        for (int i = 0; i < myGame.GetNumberOfLines(); i++)
        {
            NLineSegment line = myGame.GetLine(i);

            Vec2 differenceVector = _oldPosition - line.start;
            float ballDistance = differenceVector.Dot((line.end - line.start).Normal());
            //if (ballDistance < radius)
            //{
            Vec2 normal = (line.end - line.start).Normal();
            Vec2 dist = position + (-ballDistance + radius) * normal;
            float difference = (dist - position).Magnitude();
            float b = -velocity.Dot(normal);
            //float a = b - difference;
            float a = ballDistance - radius;

            if (b > 0)
            //{

            //    return null;
            //}
            //else
            {
                float t = 3f;
                if (a >= 0)
                //{
                //    return null;
                //}
                //else
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
                    // else return null;
                }

            }
            //}

        }


        if (current != null)
        {
            return new CollisionInfo(currentNormal, current, smallestToi);
        }
        else
        {
            return null;
        }
        // TODO: Check Line segments using myGame.GetLine();
        // return null;
    }

    void ResolveCollision(CollisionInfo col)
    {

        // TODO: resolve the collision correctly: position reset & velocity reflection.
        // ...this is not an ideal collision resolve:
        if (col.other is NLineSegment)
        {

            position = _oldPosition + col.timeOfImpact * velocity;
        }
        if (col.other is Ball)
        {
            Vec2 pointOfImpact = _oldPosition + velocity * col.timeOfImpact;
            position = pointOfImpact;
        }


        //position += col.normal * col.timeOfImpact;
        velocity.Reflect(1, col.normal);
        //if (col.other is Ball)
        //{
        //    Ball otherBall = (Ball)col.other;
        //    otherBall.velocity *= -1;
        //}

    }

    void BoundaryWrapAround()
    {
        if (position.x < 0)
        {
            position.x += game.width;
        }
        if (position.x > game.width)
        {
            position.x -= game.width;
        }
        if (position.y < 0)
        {
            position.y += game.height;
        }
        if (position.y > game.height)
        {
            position.y -= game.height;
        }
    }

    void ShowDebugInfo()
    {
        if (drawDebugLine)
        {
            ((MyGame)game).DrawLine(_oldPosition, position);
        }
        _velocityIndicator.startPoint = position;
        _velocityIndicator.vector = velocity;
    }
}

