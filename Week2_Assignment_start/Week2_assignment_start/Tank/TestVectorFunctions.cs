using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class TestVectorFunctions
    {
        public TestVectorFunctions()
        {
            float angleDegrees = 45f;
            Console.WriteLine("Deg2Rad ok ?:" + (Vec2.Deg2Rad(angleDegrees)==0.25f*Mathf.PI));

            float angleRadiand = 0.25f*Mathf.PI;
            Console.WriteLine("Rad2Deg ok ?:" + (Vec2.Rad2Deg(angleRadiand) == 45f));

            Vec2 setNewAngleDegrees = new Vec2(10,0);
            Vec2 expected = new Vec2(7.071068f, 7.071068f);
            setNewAngleDegrees.SetAngleDegrees(45);
            Console.WriteLine("SetAngleDegrees ok ?:" + (setNewAngleDegrees.x == expected.x && setNewAngleDegrees.y == expected.y));

            Vec2 setNewAngleRadians = new Vec2(10, 0);
            Vec2 expectedRad = new Vec2(7.071068f, 7.071068f);
            setNewAngleRadians.SetAngleRadians(Mathf.PI*0.25f);
            Console.WriteLine("SetAngleRadians ok ?:" + (setNewAngleRadians.x == expectedRad.x && setNewAngleRadians.y == expectedRad.y));

            Vec2 getAndleDegrees = new Vec2(10, 10);
            Console.WriteLine("getAndleDegrees ok ?:" + (getAndleDegrees.GetAngleDegrees() == 45f));


            Vec2 getAndleRadians = new Vec2(10, 10);
            Console.WriteLine("getAndleRadians ok ?:" + (getAndleRadians.GetAngleRadians() == Vec2.Deg2Rad(45f)));
            

            Vec2 originalRoatateDeg = new Vec2(10, 0);
            Vec2 endRoatateDeg = new Vec2(7.071068f, 7.071068f);
            originalRoatateDeg.RotateDegrees(45f);
            Console.WriteLine("RotateDegrees ok ?:" + (originalRoatateDeg.x == endRoatateDeg.x && originalRoatateDeg.y == endRoatateDeg.y));

            Vec2 originalRoatateRad = new Vec2(10, 0);
            Vec2 endRoatateRad = new Vec2(7.071068f, 7.071068f);
            originalRoatateRad.RotateRadians(Mathf.PI*0.25f);
            Console.WriteLine("RotateRadians ok ?:" + (originalRoatateRad.x == endRoatateRad.x && originalRoatateRad.y == endRoatateRad.y));

            Vec2 originalRoatateOverDeg = new Vec2(10, 0);
            Vec2 pointDeg = new Vec2(5, 0);
            Vec2 endRoatateOverDeg = new Vec2(8.535534f, 3.535534f);
            originalRoatateOverDeg.RotateAroundDegrees(45f,pointDeg);
            Console.WriteLine("RotateOverDegrees ok ?:" + (originalRoatateOverDeg.x == endRoatateOverDeg.x && originalRoatateOverDeg.y == endRoatateOverDeg.y));

            Vec2 originalRoatateOverRad = new Vec2(10, 0);
            Vec2 pointRad = new Vec2(5, 0);
            Vec2 endRoatateOverRad = new Vec2(8.535534f, 3.535534f);
            originalRoatateOverRad.RotateAroundRadians(Mathf.PI*0.25f, pointDeg);
            Console.WriteLine("RotateOverRadians ok ?:" + (originalRoatateOverRad.x == endRoatateOverRad.x && originalRoatateOverRad.y == endRoatateOverRad.y));
        }
    }
}
