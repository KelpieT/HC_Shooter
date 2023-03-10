using System.Collections.Generic;

public static class MyExtentions
{
	private const int Milliseconds = 1000;

	public static T RandomFromLis<T>(this IList<T> list)
	{
		int randomNum = UnityEngine.Random.Range(0, list.Count);
		return list[randomNum];
	}
	public static int ToMilliseconds(this float f)
	{
		return (int)(f * Milliseconds);
	}
}
