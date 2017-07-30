using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper {

	public static int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
