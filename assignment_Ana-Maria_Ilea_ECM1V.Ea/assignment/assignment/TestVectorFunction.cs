using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class TestVectorFunction
{
    public TestVectorFunction()
    {

        //-------------------------------------------------------------------------
        //						Addition
        //-------------------------------------------------------------------------
        Vec2 testVectorAddition1 = new Vec2(2, 3);
        Vec2 testVectorAddition2 = new Vec2(3, 4);
        Console.WriteLine("Addition ok ?:" + (testVectorAddition1.x + testVectorAddition2.x == 5 && testVectorAddition1.y + testVectorAddition2.y == 7));

        //-------------------------------------------------------------------------
        //						Subtraction
        //-------------------------------------------------------------------------
        Vec2 testVectorSubtraction1 = new Vec2(2, 3);
        Vec2 testVectorSubtraction2 = new Vec2(3, 4);
        Console.WriteLine("Subtraction ok ?:" + (testVectorSubtraction1.x - testVectorSubtraction2.x == -1 && testVectorSubtraction1.y - testVectorSubtraction2.y == -1));

        //-------------------------------------------------------------------------
        //						Multiplication Right
        //-------------------------------------------------------------------------
        Vec2 testMultiplicationRight = new Vec2(2, 3);
        Vec2 resultMultiplicationRight = testMultiplicationRight * 3;
        Console.WriteLine("Scalar multiplication right ok ?:" + (resultMultiplicationRight.x == 6 && resultMultiplicationRight.y == 9 && testMultiplicationRight.x == 2 && testMultiplicationRight.y == 3));

        //-------------------------------------------------------------------------
        //						Multiplication Left
        //-------------------------------------------------------------------------

        Vec2 testMultiplicationLeft = new Vec2(2, 3);
        Vec2 resultMultiplicationLeft = testMultiplicationRight * 3;
        Console.WriteLine("Scalar multiplication right ok ?:" + (resultMultiplicationLeft.x == 6 && resultMultiplicationLeft.y == 9 && testMultiplicationLeft.x == 2 && testMultiplicationLeft.y == 3));

        //-------------------------------------------------------------------------
        //							Magnitude
        //-------------------------------------------------------------------------
        Vec2 testVectorMagnitude = new Vec2(3, 4);
        Console.WriteLine("Magnitude ok ?:" + (testVectorMagnitude.Magnitude() == 5f));

        //-------------------------------------------------------------------------
        //							Normalize Current Vector
        //-------------------------------------------------------------------------
        Vec2 testVectorNormalizeCurrent = new Vec2(3, 4);
        testVectorNormalizeCurrent.Normalize();
        Console.WriteLine("Normalized Curent Vector ok ?:" + (testVectorNormalizeCurrent.x == 0.6f && testVectorNormalizeCurrent.y == 0.8f));

        //-------------------------------------------------------------------------
        //							Normalize New Vector
        //-------------------------------------------------------------------------
        Vec2 testVectorNormalizeOther = new Vec2(3, 4);
        Vec2 normalizedVector = testVectorNormalizeOther.Normalized();
        Console.WriteLine("Normalized New Vector ok ?:" + (normalizedVector.x == 0.6f && normalizedVector.y == 0.8f));

        //-------------------------------------------------------------------------
        //							SetScale Vector
        //-------------------------------------------------------------------------
        Vec2 testVectorSetScale = new Vec2(3, 4);
        testVectorSetScale.SetXY(5, 6);
        Console.WriteLine("SetScale Vector ok ?:" + (testVectorSetScale.x == 5 && testVectorSetScale.y == 6));

        //-------------------------------------------------------------------------
        //							Deg2Rad
        //-------------------------------------------------------------------------
        float angleDegrees = 45f;
        Console.WriteLine("Deg2Rad ok ?:" + (Vec2.Deg2Rad(angleDegrees) == 0.25f * Mathf.PI));

        //-------------------------------------------------------------------------
        //							Rad2Deg
        //-------------------------------------------------------------------------
        float angleRadiand = 0.25f * Mathf.PI;
        Console.WriteLine("Rad2Deg ok ?:" + (Vec2.Rad2Deg(angleRadiand) == 45f));

        //-------------------------------------------------------------------------
        //							SetAngleDegrees
        //-------------------------------------------------------------------------
        Vec2 setNewAngleDegrees = new Vec2(10, 0);
        Vec2 expected = new Vec2(7.071068f, 7.071068f);
        setNewAngleDegrees.SetAngleDegrees(45);
        Console.WriteLine("SetAngleDegrees ok ?:" + (setNewAngleDegrees.x == expected.x && setNewAngleDegrees.y == expected.y));

        //-------------------------------------------------------------------------
        //							SetAngleRadians
        //-------------------------------------------------------------------------
        Vec2 setNewAngleRadians = new Vec2(10, 0);
        Vec2 expectedRad = new Vec2(7.071068f, 7.071068f);
        setNewAngleRadians.SetAngleRadians(Mathf.PI * 0.25f);
        Console.WriteLine("SetAngleRadians ok ?:" + (setNewAngleRadians.x == expectedRad.x && setNewAngleRadians.y == expectedRad.y));

        //-------------------------------------------------------------------------
        //							getAndleDegrees
        //-------------------------------------------------------------------------
        Vec2 getAndleDegrees = new Vec2(10, 10);
        Console.WriteLine("getAndleDegrees ok ?:" + (getAndleDegrees.GetAngleDegrees() == 45f));

        //-------------------------------------------------------------------------
        //							getAndleRadians
        //-------------------------------------------------------------------------
        Vec2 getAndleRadians = new Vec2(10, 10);
        Console.WriteLine("getAndleRadians ok ?:" + (getAndleRadians.GetAngleRadians() == Vec2.Deg2Rad(45f)));

        //-------------------------------------------------------------------------
        //							RotateDegrees
        //-------------------------------------------------------------------------
        Vec2 originalRoatateDeg = new Vec2(10, 0);
        Vec2 endRoatateDeg = new Vec2(7.071068f, 7.071068f);
        originalRoatateDeg.RotateDegrees(45f);
        Console.WriteLine("RotateDegrees ok ?:" + (originalRoatateDeg.x == endRoatateDeg.x && originalRoatateDeg.y == endRoatateDeg.y));

        //-------------------------------------------------------------------------
        //							RotateRadians
        //-------------------------------------------------------------------------
        Vec2 originalRoatateRad = new Vec2(10, 0);
        Vec2 endRoatateRad = new Vec2(7.071068f, 7.071068f);
        originalRoatateRad.RotateRadians(Mathf.PI * 0.25f);
        Console.WriteLine("RotateRadians ok ?:" + (originalRoatateRad.x == endRoatateRad.x && originalRoatateRad.y == endRoatateRad.y));

        //-------------------------------------------------------------------------
        //							RotateOverDegrees
        //-------------------------------------------------------------------------
        Vec2 originalRoatateOverDeg = new Vec2(10, 0);
        Vec2 pointDeg = new Vec2(5, 0);
        Vec2 endRoatateOverDeg = new Vec2(8.535534f, 3.535534f);
        originalRoatateOverDeg.RotateAroundDegrees(45f, pointDeg);
        Console.WriteLine("RotateOverDegrees ok ?:" + (originalRoatateOverDeg.x == endRoatateOverDeg.x && originalRoatateOverDeg.y == endRoatateOverDeg.y));

        //-------------------------------------------------------------------------
        //							RotateOverRadians
        //-------------------------------------------------------------------------
        Vec2 originalRoatateOverRad = new Vec2(10, 0);
        Vec2 pointRad = new Vec2(5, 0);
        Vec2 endRoatateOverRad = new Vec2(8.535534f, 3.535534f);
        originalRoatateOverRad.RotateAroundRadians(Mathf.PI * 0.25f, pointDeg);
        Console.WriteLine("RotateOverRadians ok ?:" + (originalRoatateOverRad.x == endRoatateOverRad.x && originalRoatateOverRad.y == endRoatateOverRad.y));

        //-------------------------------------------------------------------------
        //							Dot product
        //-------------------------------------------------------------------------
        Vec2 testDotProductVec1 = new Vec2(2, 3);
        Vec2 testDotProductVec2 = new Vec2(4, 5);
        Console.WriteLine("Dot product ok ?:" + (testDotProductVec1.Dot(testDotProductVec2) == 23));

        //-------------------------------------------------------------------------
        //							Normal
        //-------------------------------------------------------------------------
        Vec2 testVectorForNormal = new Vec2(3, -4);
        Vec2 normal = testVectorForNormal.Normal();
        Console.WriteLine("Normal ok ?:" + (normal.x == 0.8f && normal.y == 0.6f));

        //-------------------------------------------------------------------------
        //							Reflect velocity
        //-------------------------------------------------------------------------
        Vec2 testVelocity = new Vec2(2, -11);
        Vec2 testNormal = new Vec2(0.8f, 0.6f);
        testVelocity.Reflect(1, testNormal);
        Console.WriteLine("Reflect velocity ok ?:" + (testVelocity.x == 10f && testVelocity.y == -5f));
    }
}

