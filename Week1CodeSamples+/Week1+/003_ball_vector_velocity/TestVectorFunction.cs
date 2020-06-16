using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
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
		}
    }
}
