using System;

class CVec {
	public float x;
	public float y;

	public CVec(float px, float py) {
		x = px;
		y = py;
	}

	public override string ToString () {
		return "(" + x + "," + y + ")";
	}

	public void Scale(float scalar) {
		x *= scalar;
		y *= scalar;
	}

	// This method is called when evaluating x+y, where x and y are CVecs:
	public static CVec operator +(CVec left, CVec right) {
		return new CVec (left.x + right.x, left.y + right.y);
	}

	// This method is called by the garbage collector upon removing this object from memory:
	~CVec() {
		Console.WriteLine ("    Garbage collection removes a CVec from memory!");
	}
}

struct SVec {
	public float x;
	public float y;

	public SVec(float px, float py) {
		x = px;
		y = py;
	}

	public override string ToString () {
		return "(" + x + "," + y + ")";
	}

	public void Scale(float scalar) {
		x *= scalar;
		y *= scalar;
	}

	// This method is called when evaluating x+y, where x and y are SVecs:
	public static SVec operator +(SVec left, SVec right) {
		return new SVec (left.x + right.x, left.y + right.y);
	}
}


class MainClass
{
	public static void Main (string[] args)
	{
		// TODO: Change between SVec and CVec in the following two lines
		var a = new SVec (1, 2);
		var b = new SVec (3, 4);
		// ...and try to make sense of the console output:

		Console.WriteLine ("Types: "+a.GetType());

		Console.WriteLine ("VALUES:\n    Vector a: {0} vector b: {1}",a,b);

		Console.WriteLine ("INSTRUCTION: a = b");
		a = b;
		Console.WriteLine ("VALUES:\n    Vector a: {0} vector b: {1}",a,b);

		Console.WriteLine ("INSTRUCTION: a.x = 7");
		a.x = 7;
		Console.WriteLine ("VALUES:\n    Vector a: {0} vector b: {1}",a,b);

		Console.WriteLine ("INSTRUCTION: Scale(a,2)");
		Scale (a,2);
		Console.WriteLine ("VALUES:\n    Vector a: {0} vector b: {1}",a,b);

		Console.WriteLine ("INSTRUCTION: a.Scale(3)");
		a.Scale (3);
		Console.WriteLine ("VALUES:\n    Vector a: {0} vector b: {1}",a,b);

		Console.WriteLine ("INSTRUCTION: a = a + b");
		a = a + b;
		Console.WriteLine ("VALUES:\n    Vector a: {0} vector b: {1}",a,b);

		// Force the garbage collector to do its job:
		// (demonstration purposes only - you should usually avoid this method!)
		System.GC.Collect ();

		Console.WriteLine ("\nPress enter to quit");
		Console.ReadLine ();
	}

	static void Scale(CVec v, float scalar) {
		v.x *= scalar;
	}

	static void Scale(SVec v, float scalar) {
		v.x *= scalar;
	}
}