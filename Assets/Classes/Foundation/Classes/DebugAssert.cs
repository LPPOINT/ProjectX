using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{

    public static class DebugAssert 
    {

        public static void DebugLog(string message)
        {
            Debug.Log(message);
        }
        public static void DebugLog(string message, Object context)
        {
            Debug.Log(message, context);
        }

        public static bool IsTrue(bool condition, string ifFail)
        {
            if(!condition) DebugLog(ifFail);
            return condition;
        }

        public static bool IsTrue(bool condition, string ifFail, string ifSuccess)
        {
            DebugLog(condition ? ifSuccess : ifFail);
            return condition;
        }

        public static bool IsTrue(bool condition, string ifFail, Object context)
        {
            if (!condition) DebugLog(ifFail, context);
            return condition;
        }

        public static bool IsTrue(bool condition, string ifFail, string ifSuccess, Object context)
        {
            DebugLog(condition ? ifSuccess : ifFail, context);
            return condition;
        }
    }
}
