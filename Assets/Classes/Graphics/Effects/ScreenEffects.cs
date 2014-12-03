using Assets.Classes.Foundation.Classes;

namespace Assets.Classes.Graphics.Effects
{
    public class ScreenEffects : SingletonBehaviour<ScreenEffect>
    {
        public static T GetEffectInstance<T>() where T : ScreenEffect
        {
            return Instance.gameObject.GetComponentInChildren<T>();
        }

        public static void ShowStripes()
        {
            GetEffectInstance<StripesEffect>().Show();
        }

        public static void HideStripes()
        {
            GetEffectInstance<StripesEffect>().Hide();
        }

        public static void FadeIn()
        {
            GetEffectInstance<FadeEffect>().FadeIn();
        }

        public static void FadeOut()
        {
            GetEffectInstance<FadeEffect>().FadeOut();
        }

        public static void BlurIn()
        {
            GetEffectInstance<BlurEffect>().PlayBlurIn();
        }

        public static void BlurOut()
        {
            GetEffectInstance<BlurEffect>().PlayBlurOut();
        }

        public static void BlurInContinuous()
        {
            GetEffectInstance<BlurEffect>().PlayBlurInContinuous();
        }

    }
}
