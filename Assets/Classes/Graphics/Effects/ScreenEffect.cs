using UnityEngine;

namespace Assets.Classes.Graphics.Effects
{
    public class ScreenEffect : MonoBehaviour
    {

        public static ScreenEffect GetInstanceInScene<T>() where T : ScreenEffect
        {
            return ScreenEffects.GetEffectInstance<T>();
        }
    }
}
