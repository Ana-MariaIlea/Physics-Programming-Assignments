using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Platform : LineSegment
{
    public const int Basic = 0;
    public const int Gravity = 1;
    public const int EndSim = 2;

    public const float Ground = 0.1f;
    public const float Ice = 0.01f;
    public const float Sand = 0.25f;


    public int _platformType;
    public float _bounce;
    public bool _inverted;

    private Bullet startBall;
    private Bullet endBall;

    MyGame myGame;
    //------------------------------------------------------------------------------------------
    //                                      constructor
    //------------------------------------------------------------------------------------------
    public Platform(Vec2 start, Vec2 end, int platformType, float bounce, bool inverted = false) : base(start, end, 0xff00ff00, 4)
    {
        _bounce = bounce;
        _platformType = platformType;
        startBall = new Bullet(0, start, moving: false);
        endBall = new Bullet(0, end, moving: false);
        myGame = (MyGame)game;
        myGame._lines.Add(this);
        myGame._movers.Add(startBall);
        myGame._movers.Add(endBall);
        startBall.SetType(Bullet.IsSegBall);
        endBall.SetType(Bullet.IsSegBall);
        AddChild(startBall);
        AddChild(endBall);
        _inverted = inverted;
    }
    //------------------------------------------------------------------------------------------
    //                                      removeLine()
    //------------------------------------------------------------------------------------------
    public void removeLine()
    {
        myGame._movers.Remove(startBall);
        myGame._movers.Remove(endBall);
        myGame._lines.Remove(this);
        this.LateDestroy();
    }
}

