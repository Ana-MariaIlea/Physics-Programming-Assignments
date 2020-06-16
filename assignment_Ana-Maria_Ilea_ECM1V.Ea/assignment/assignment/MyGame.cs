using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
    public List<EasyDraw> _movers;
    public List<LineSegment> _lines;

    Player player;
    private Sprite endScreen;
    //------------------------------------------------------------------------------------------
    //                                      GetNumberOfLines()
    //------------------------------------------------------------------------------------------
    public int GetNumberOfLines()
    {
        return _lines.Count;
    }
    //------------------------------------------------------------------------------------------
    //                                      GetLine()
    //------------------------------------------------------------------------------------------
    public LineSegment GetLine(int index)
    {
        if (index >= 0 && index < _lines.Count)
        {
            return _lines[index];
        }
        return null;
    }
    //------------------------------------------------------------------------------------------
    //                                      GetNumberOfMovers()
    //------------------------------------------------------------------------------------------
    public int GetNumberOfMovers()
    {
        return _movers.Count;
    }
    //------------------------------------------------------------------------------------------
    //                                      GetMover()
    //------------------------------------------------------------------------------------------
    public EasyDraw GetMover(int index)
    {
        if (index >= 0 && index < _movers.Count)
        {
            return _movers[index];
        }
        return null;
    }
    //------------------------------------------------------------------------------------------
    //                                      constructor
    //------------------------------------------------------------------------------------------

    public MyGame() : base(1200, 603, false, false)
    {
        targetFps = 60;

        _movers = new List<EasyDraw>();
        _lines = new List<LineSegment>();

        LoadScene();
        endScreen = new Sprite("assets/end.png");
        TestVectorFunction unitTest = new TestVectorFunction();
    }
    //------------------------------------------------------------------------------------------
    //                                      AddLine()
    //------------------------------------------------------------------------------------------
    public void AddLine(Vec2 start, Vec2 end, int type, float bounce,bool inverted=false)
    {
        Platform line = new Platform(start, end, type, bounce,inverted);
        AddChild(line);
    }
    //------------------------------------------------------------------------------------------
    //                                      LoadScene()
    //------------------------------------------------------------------------------------------

    void LoadScene()
    {
        addBounderies();
        addStairs();
        addPlatformes();

        player = new Player(15, new Vec2(50, 550), new Vec2(0, 1));
        AddChild(player);

        addTurrets();

        AddLine(new Vec2(width, 150), new Vec2(width - 150, 150), Platform.EndSim, Platform.Ground);  //Platform that ends the simulation


        foreach (EasyDraw b in _movers)
        {
            if (!(b.parent is Stairs))
                AddChild(b);
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      addTurrets()
    //------------------------------------------------------------------------------------------
    private void addTurrets()
    {
        Turret item1 = new Turret(new Vec2(775, 375), Turret.Left);
        AddChild(item1);
        Turret item2 = new Turret(new Vec2(25, 475), Turret.Right);
        AddChild(item2);
        Turret item3 = new Turret(new Vec2(25, 275), Turret.Right);
        AddChild(item3);
        Turret item4 = new Turret(new Vec2(775, 175), Turret.Left);
        AddChild(item4);
        Turret item5 = new Turret(new Vec2(25, 75), Turret.Right);
        AddChild(item5);
    }
    //------------------------------------------------------------------------------------------
    //                                      addPlatformes(()
    //------------------------------------------------------------------------------------------
    private void addPlatformes()
    {
        AddLine(new Vec2(600, 100), new Vec2(0, 100), Platform.Gravity, Platform.Sand);
        AddLine(new Vec2(0, 100), new Vec2(600, 100), Platform.Basic, Platform.Ground,true);

        AddLine(new Vec2(800, 200), new Vec2(200, 200), Platform.Gravity, Platform.Ground);
        AddLine(new Vec2(200, 200), new Vec2(800, 200), Platform.Basic, Platform.Ground, true);

        AddLine(new Vec2(600, 300), new Vec2(0, 300), Platform.Gravity, Platform.Ice);
        AddLine(new Vec2(0, 300), new Vec2(600, 300), Platform.Basic, Platform.Ground, true);

        AddLine(new Vec2(800, 400), new Vec2(200, 400), Platform.Gravity, Platform.Sand);
        AddLine(new Vec2(200, 400), new Vec2(800, 400), Platform.Basic, Platform.Ground, true);

        AddLine(new Vec2(600, 500), new Vec2(0, 500), Platform.Gravity, Platform.Ice);
        AddLine(new Vec2(0, 500), new Vec2(600, 500), Platform.Basic, Platform.Ground, true);

        AddLine(new Vec2(800, 600), new Vec2(0, 600), Platform.Gravity, Platform.Ground);

        AddLine(new Vec2(800, 200), new Vec2(800, height), Platform.Basic, Platform.Ground); // vertical line
        AddLine(new Vec2(800, height), new Vec2(800, 200), Platform.Basic, Platform.Ground);
    }
    //------------------------------------------------------------------------------------------
    //                                      addStairs()
    //------------------------------------------------------------------------------------------
    private void addStairs()
    {
        Stairs stair1 = new Stairs(new Vec2(600, 100), Stairs.Right);
        Stairs stair2 = new Stairs(new Vec2(200, 200), Stairs.Left);
        Stairs stair3 = new Stairs(new Vec2(600, 300), Stairs.Right);
        Stairs stair4 = new Stairs(new Vec2(200, 400), Stairs.Left);
        Stairs stair5 = new Stairs(new Vec2(600, 500), Stairs.Right);
        AddChild(stair1);
        AddChild(stair2);
        AddChild(stair3);
        AddChild(stair4);
        AddChild(stair5);
    }
    //------------------------------------------------------------------------------------------
    //                                      addBounderies()
    //------------------------------------------------------------------------------------------
    private void addBounderies()
    {
        AddLine(new Vec2(0, 0), new Vec2(width, 0), Platform.Basic, Platform.Ground,true);               //Top
        AddLine(new Vec2(0, height), new Vec2(0, 0), Platform.Basic, Platform.Ground);              //Left
        AddLine(new Vec2(width, 0), new Vec2(width, height), Platform.Basic, Platform.Ground);      //Right
        AddLine(new Vec2(width, height), new Vec2(0, height), Platform.Gravity, Platform.Ground);   //Bottom
    }
    //------------------------------------------------------------------------------------------
    //                                      Update()
    //------------------------------------------------------------------------------------------
    void Update()
    {
        if (player.ending == 0)
        {
            player.Step();
        }
        else
        {
            AddChild(endScreen);
        }
    }
    //------------------------------------------------------------------------------------------
    //                                      Main(()
    //------------------------------------------------------------------------------------------
    static void Main()
    {
        new MyGame().Start();
    }
}