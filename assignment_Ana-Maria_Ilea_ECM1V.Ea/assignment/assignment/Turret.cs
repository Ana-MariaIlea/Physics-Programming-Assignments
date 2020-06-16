using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Turret : NLineSegment
{
    public const int Left = 0;
    public const int Right = 1;
    public const int Up = 2;

    private Platform line1;
    private Platform line2;
    private Platform line3;
    private Platform line4;

    private int distance = 25;

    private Barrel _barrel;

    private int _direction;
    private Vec2 bulletSpeed = new Vec2(1, 0);

    private float time = 0;
    //------------------------------------------------------------------------------------------
    //                                      constructor
    //------------------------------------------------------------------------------------------
    public Turret(Vec2 position, int direction) : base(position, position, 0xff00ff00, 4)
    {

        line1 = new Platform(position - new Vec2(distance, distance), position - new Vec2(distance, -distance), Platform.Basic, Platform.Ground);
        line2 = new Platform(position - new Vec2(distance, -distance), position + new Vec2(distance, distance), Platform.Basic, Platform.Ground);
        line3 = new Platform(position + new Vec2(distance, distance), position + new Vec2(distance, -distance), Platform.Basic, Platform.Ground);
        line4 = new Platform(position + new Vec2(distance, -distance), position - new Vec2(distance, distance), Platform.Gravity, Platform.Ground);

        _direction = direction;

        AddChild(line1);
        AddChild(line2);
        AddChild(line3);
        AddChild(line4);

        _barrel = new Barrel(Barrel.StaticBarrel);
        AddChild(_barrel);

        setBarrelAngleAndBulletDirection();

        _barrel.width /= 2;
        _barrel.height /= 2;
        _barrel.SetXY(position.x, position.y);
    }
    //------------------------------------------------------------------------------------------
    //                          setBarrelAngleAndBulletDirection()
    //------------------------------------------------------------------------------------------
    private void setBarrelAngleAndBulletDirection()
    {
        switch (_direction)
        {
            case Left:
                _barrel.rotation = 180;
                bulletSpeed.SetXY(-1, 0);
                break;
            case Up:
                _barrel.rotation = -90;
                bulletSpeed.SetXY(0, -1);
                break;
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      RemoveLinesFromList()
    //------------------------------------------------------------------------------------------
    public void RemoveLinesFromList()
    {
        line1.removeLine();
        line2.removeLine();
        line3.removeLine();
        line4.removeLine();
    }
    //------------------------------------------------------------------------------------------
    //                                      Shoot()
    //------------------------------------------------------------------------------------------
    private void Shoot()
    {
        if (time <= 0)
        {
            Vec2 bulletVelocity = bulletSpeed;
            Vec2 bulletDirection = new Vec2(_barrel.x, _barrel.y);
            Vec2 delta = Vec2.GetUnitVectorDeg(_barrel.rotation + this.rotation);
            bulletDirection += delta * (_barrel.width / 3 * 2);
            Bullet bul = new Bullet(5, bulletDirection, bulletVelocity);
            bul.rotation = _barrel.rotation + this.rotation;
            game.AddChild(bul);
            MyGame myGame = (MyGame)game;
            myGame._movers.Add(bul);
            time = 3500;
        }
        else time -= Time.deltaTime;
    }
    //------------------------------------------------------------------------------------------
    //                                      Update()
    //------------------------------------------------------------------------------------------
    void Update()
    {
        Shoot();
    }
}

