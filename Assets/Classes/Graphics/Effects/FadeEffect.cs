using System;
using Assets.Extensions.iTween;
using UnityEngine;

namespace Assets.Classes.Graphics.Effects
{
    public class FadeEffect : ScreenEffect
    {



        public enum FadeType
        {
            FadeIn,
            FadeOut
        }
        public class FadeInfo
        {
            public FadeInfo(Texture2D fadeTexture, float fadeDuration, iTween.EaseType fadeEaseType, float amount, bool destroyFadeAfterFinish = false)
            {
                DestroyFadeAfterFinish = destroyFadeAfterFinish;
                Amount = amount;
                FadeTexture = fadeTexture;
                FadeDuration = fadeDuration;
                FadeEaseType = fadeEaseType;
            }

            public FadeInfo(Color fadeColor, float fadeDuration, iTween.EaseType fadeEaseType, float amount, bool destroyFadeAfterFinish = false)
            {
                DestroyFadeAfterFinish = destroyFadeAfterFinish;
                Amount = amount;
                FadeTexture = iTween.CameraTexture(fadeColor);
                FadeDuration = fadeDuration;
                FadeEaseType = fadeEaseType;
            }

            public Texture2D FadeTexture { get;  set; }
            public float FadeDuration { get;  set; }
            public iTween.EaseType FadeEaseType { get;  set; }
            public float Amount { get;  set; }
            public bool DestroyFadeAfterFinish { get;  set; }

        }
        public class FadeEventArgs : EventArgs
        {
            public FadeEventArgs(FadeType type, FadeInfo info)
            {
                Info = info;
                Type = type;
            }

            public FadeType Type { get; private set; }
            public FadeInfo Info { get; private set; }
        }


        public event EventHandler<FadeEventArgs> FadeStarted;
        protected virtual void OnFadeStarted(FadeEventArgs e)
        {
            var handler = FadeStarted;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<FadeEventArgs> FadeDone;
        protected virtual void OnFadeDone(FadeEventArgs e)
        {
            var handler = FadeDone;
            if (handler != null) handler(this, e);
        }


        public bool IsFading { get; private set; }
        public FadeInfo LastFadeInfo { get; private set; }

        private void OnITweenFadeToDone()
        {
            IsFading = false;
            if(LastFadeInfo.DestroyFadeAfterFinish)
                iTween.CameraFadeDestroy();
            OnFadeDone(new FadeEventArgs(FadeType.FadeIn, LastFadeInfo));
        }
        private void OnITweenFadeFromDone()
        {
            IsFading = false;
            if (LastFadeInfo.DestroyFadeAfterFinish)
                iTween.CameraFadeDestroy();
            OnFadeDone(new FadeEventArgs(FadeType.FadeOut, LastFadeInfo));
        }

        public void FadeIn(FadeInfo fadeInfo)
        {
            if(IsFading) return;
            LastFadeInfo = fadeInfo;

            iTween.CameraFadeDestroy();
            iTween.CameraFadeAdd(fadeInfo.FadeTexture);
            iTween.CameraFadeTo(iTween.Hash(
                "oncomplete", "OnITweenFadeToDone",
                "oncompletetarget", gameObject, 
                "time", fadeInfo.FadeDuration,
                "easetype", fadeInfo.FadeEaseType,
                "amount", fadeInfo.FadeDuration,
                "delay", 0));

            IsFading = true;
            OnFadeStarted(new FadeEventArgs(FadeType.FadeIn, fadeInfo));
        }
        public void FadeOut(FadeInfo fadeInfo)
        {
            if(IsFading) return;
            LastFadeInfo = fadeInfo;

            iTween.CameraFadeDestroy();
            iTween.CameraFadeAdd(fadeInfo.FadeTexture);
            iTween.CameraFadeFrom(iTween.Hash(
                "oncomplete", "OnITweenFadeFromDone",
                "oncompletetarget", gameObject,
                "time", fadeInfo.FadeDuration,
                "easetype", fadeInfo.FadeEaseType,
                "amount", fadeInfo.FadeDuration,
                "delay", 0));

            IsFading = true;
            OnFadeStarted(new FadeEventArgs(FadeType.FadeOut, fadeInfo));

        }

        public void FadeIn()
        {
            FadeIn(new FadeInfo(Color.black, 0.6f, iTween.EaseType.linear, 255, true));
        }
        public void FadeOut()
        {
            FadeOut(new FadeInfo(Color.black, 0.6f, iTween.EaseType.linear, 0, true));
        }

    }
}
