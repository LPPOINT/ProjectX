using Assets.Classes.Common;
using UnityEngine;

namespace Assets.Classes.Effects
{
    public class ScreenEffects : SingletonBehaviour<ScreenEffect>
    {
        public static ScreenEffect GetEffectInstance<T>() where T : ScreenEffect
        {
            return Instance.gameObject.GetComponentInChildren<T>();
        }
    }
}
