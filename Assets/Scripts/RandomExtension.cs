using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class RandomExtension
{
    /// <summary>
    /// Return a random boolean with probability of being true equal to p
    /// </summary>
    /// <param name="random"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool NextBoolean(this Random random, float p = 0.5f)
    {
        int num = random.Next();
        return num < Int32.MaxValue * p;
    }

    public static float NextFloat(this Random random, float min, float max)
    {
        double num = random.NextDouble();
        return (float)(min + (max - min) * num);
    }
}
