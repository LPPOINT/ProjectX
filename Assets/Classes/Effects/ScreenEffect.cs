using UnityEngine;

namespace Assets.Classes.Effects
{
    public class ScreenEffect : MonoBehaviour
    {
        public static ScreenEffect GetInstanceInScene<T>() where T : ScreenEffect
        {
            return ScreenEffects.GetEffectInstance<T>();
        }
    }
}
