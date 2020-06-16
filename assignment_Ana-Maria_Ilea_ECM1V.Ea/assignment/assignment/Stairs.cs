using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Stairs : NLineSegment
{
    public const int Left = 0;
    public const int Right = 1;

    public Bullet ball;

    private Platform line1;
    private Platform line2;

    private int _direction;

    public bool stairsEvent = false;

    private int degrees = 1;
    private int time = 0;
    private bool stairsDown = false;
    //------------------------------------------------------------------------------------------
    //                                      constructor
    //------------------------------------------------------------------------------------------
    public Stairs(Vec2 position, int direction) : base(position, position, 0xff00ff00, 4)
    {
        MyGame myGame = (MyGame)game;
        ball = new Bullet(10, start, moving: false);
        myGame._movers.Add(ball);
        AddChild(ball);
        ball.SetType(Bullet.IsSegBall);

        _direction = direction;
        setStairsPosition();

        myGame._lines.Add(line1);
        myGame._lines.Add(line2);
        AddChild(line1);
        AddChild(line2);
    }
    //------------------------------------------------------------------------------------------
    //                                      setStairsPosition()
    //------------------------------------------------------------------------------------------

    private void setStairsPosition()
    {
        if (_direction == Right)
        {
            line1 = new Platform(start - new Vec2(100, 100), start, Platform.Gravity, Platform.Ground);
            line2 = new Platform(start, start - new Vec2(100, 100), Platform.Gravity, Platform.Ground);
        }
        else
        {
            line1 = new Platform(start, start - new Vec2(-100, 100), Platform.Gravity, Platform.Ground);
            line2 = new Platform(start - new Vec2(-100, 100), start, Platform.Gravity, Platform.Ground);
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      moveStairs()
    //------------------------------------------------------------------------------------------
    void moveStairs()
    {
        if (time < 180)
        {
            if (_direction == Right)
            {
                line1.start.RotateAroundDegrees(degrees, line1.end);
                line2.end.RotateAroundDegrees(degrees, line2.start);
            }
            else
            {
                line1.end.RotateAroundDegrees(-degrees, line1.start);
                line2.start.RotateAroundDegrees(-degrees, line2.end);
            }
            time++;
        }
        else
        {
            stairsEvent = false;
            stairsDown = true;
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      Update()
    //------------------------------------------------------------------------------------------
    void Update()
    {

        if (ball == null && stairsDown == false)
        {
            stairsEvent = true;
        }
        if (stairsEvent == true)
        {
            moveStairs();
        }
    }
}

