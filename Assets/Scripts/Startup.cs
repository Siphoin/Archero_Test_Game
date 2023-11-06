using UnityEngine;

namespace Archero
{
    public static class Startup
	{
		[RuntimeInitializeOnLoadMethod]
		private static void Run ()
		{
			Debug.Log("run");
		}
	}
}