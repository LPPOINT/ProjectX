using System;
using Assets.Classes.Foundation.Classes;
using Assets.Classes.Foundation.Extensions;
using Assets.Extensions.iTween;
using UnityEngine;

namespace Assets.Classes.Graphics.Effects
{
    public class BlurEffect : ScreenEffect
    {


        [Serializable]
        public class Defaults 
        {
            public float FullBlurInTime = 0.5f;
            public float FullBlurOutTime = 0.3f;
            public iTween.EaseType FullBlurInEaseType = iTween.EaseType.easeInOutCirc;
            public iTween.EaseType FullBlurOutEaseType = iTween.EaseType.easeInOutCirc;


            public float FullBlurMaxSize = 9;
            public float PartialBlurSizeOffset = 3;

            public float PartialBlurInTime = 1.5f;
            public float PartialBlurOutTime = 1.5f;
            public iTween.EaseType PartialBlurInEaseType = iTween.EaseType.linear;
            public iTween.EaseType PartialBlurOutEaseType = iTween.EaseType.linear;
        }

        public Defaults DefaultValues;

        public class BlurInfo
        {
            public BlurInfo(bool isLooped, iTween.EaseType easeType, float time, float @from, float to)
            {
                To = to;
                From = @from;
                Time = time;
                EaseType = easeType;
                IsLooped = isLooped;
            }

            public bool IsLooped { get; private set; }
            public iTween.EaseType EaseType { get; private set; }
            public float Time { get; private set; }
            public float From { get; private set; }
            public float To { get; private set; }
        }

        public class BlurInInfo : BlurInfo
        {


            public bool DestroyAfterFinish { get; private set; }

            public BlurInInfo(iTween.EaseType easeType, float time,  float to, bool destroyAfterFinish = true) 
                : base(false, easeType, time, 0, to)
            {
                DestroyAfterFinish = destroyAfterFinish;
            }

            protected BlurInInfo(bool isLooped, iTween.EaseType easeType, float time, float to, bool destroyAfterFinish = true)
                : base(isLooped, easeType, time, 0, to)
            {
                DestroyAfterFinish = destroyAfterFinish;
            }
        }

        public class BlurOutInfo : BlurInfo
        {
            public BlurOutInfo(iTween.EaseType easeType, float time, float @from, float to) : base(false, easeType, time, @from, 0)
            {
            }
        }

        public class BlurInContinuousInfo : BlurInInfo
        {

            public float PartialRange { get; private set; }
            public float PartialInTime { get; private set; }
            public float PartialOutTime { get; private set; }
            public iTween.EaseType PartialInEase { get; private set; }
            public iTween.EaseType PartialOutEase { get; private set; }

            public BlurInContinuousInfo(iTween.EaseType easeType, float time, float to, float partialRange, float partialInTime, float partialOutTime, iTween.EaseType partialInEase, iTween.EaseType partialOutEase) 
                : base(true, easeType, time, to)
            {
                PartialRange = partialRange;
                PartialInTime = partialInTime;
                PartialOutTime = partialOutTime;
                PartialInEase = partialInEase;
                PartialOutEase = partialOutEase;
            }
        }

        public static float MaxBlurSize = 10f;

        #region Internal
        private Blur blur;
        private float currentTo;
        private iTween.EaseType currentEaseType;
        private float maxBlurValue;

        private void OnITweenBlurUpdate(float value)
        {
            blur.blurSize = value;
        }
        private void OnITweenBlurDone()
        {

            if (CurrentBlurInfo is BlurInInfo && !(CurrentBlurInfo is BlurInContinuousInfo) &&
                !((CurrentBlurInfo as BlurInInfo).DestroyAfterFinish))
            {
                return;
            }

            if (!IsLooped && blur != null)
            {
                IsPlaying = false;
                Destroy(blur);
                blur = null;
            }
            else if (IsLooped && CurrentBlurInfo is BlurInContinuousInfo)
            {
                var bic = CurrentBlurInfo as BlurInContinuousInfo;
                if (maxBlurValue < currentTo) maxBlurValue = currentTo;

                if (Math.Abs(currentTo - maxBlurValue) < 0.001f)
                {
                    Blur(maxBlurValue, maxBlurValue -bic.PartialRange, bic.PartialOutTime, bic.PartialOutEase, IsLooped);
                }
                else
                {
                    Blur(maxBlurValue - bic.PartialRange, maxBlurValue,bic.PartialInTime, bic.PartialInEase, IsLooped);
                }

            }
        }

        private void Blur(float blurFrom, float blurTo, float blurTime, iTween.EaseType blurEaseType, bool looped = false)
        {
            PrepareBlur();
            IsLooped = looped;

            currentTo = blurTo;
            currentEaseType = blurEaseType;

            if (IsPlaying)
            {
                iTween.Stop(gameObject);
            }

            IsPlaying = true;
            iTween.ValueTo(gameObject, iTween.Hash("onupdate", "OnITweenBlurUpdate",
                "from", blurFrom,
                "to", blurTo,
                "time", blurTime,
                "easetype", blurEaseType,
                "oncomplete", "OnITweenBlurDone",
                "oncompletetarget", gameObject));
        }

        public bool IsPlaying { get; private set; }
        public bool IsLooped { get; private set; }

        #endregion

        public BlurInfo CurrentBlurInfo { get; private set; }

        public void PlayBlur(BlurInfo info)
        {
            CurrentBlurInfo = info;
            Blur(info.From, info.To, info.Time, info.EaseType, info.IsLooped);
            OnBlurStarted(new GenericEventArgs<BlurInfo>(CurrentBlurInfo));
        }

        public void PlayBlurIn()
        {
            PlayBlur(new BlurInInfo(DefaultValues.FullBlurInEaseType, DefaultValues.FullBlurInTime, DefaultValues.FullBlurMaxSize, false));
        }

        public void PlayBlurOut()
        {
            PlayBlur(new BlurOutInfo(DefaultValues.FullBlurOutEaseType, DefaultValues.FullBlurOutTime, DefaultValues.FullBlurMaxSize, 0));
        }

        public void PlayBlurInContinuous()
        {
            PlayBlur(new BlurInContinuousInfo(
                DefaultValues.FullBlurInEaseType,
                DefaultValues.FullBlurInTime,
                DefaultValues.FullBlurMaxSize, 
                DefaultValues.PartialBlurSizeOffset,
                DefaultValues.PartialBlurInTime,
                DefaultValues.PartialBlurOutTime,
                DefaultValues.PartialBlurInEaseType, 
                DefaultValues.PartialBlurOutEaseType));
        }


        public event EventHandler<GenericEventArgs<BlurInfo>> BlurStarted;

        protected virtual void OnBlurStarted(GenericEventArgs<BlurInfo> e)
        {
            var handler = BlurStarted;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<GenericEventArgs<BlurInfo>> BlurFinished;

        protected virtual void OnBlurFinished(GenericEventArgs<BlurInfo> e)
        {
            var handler = BlurFinished;
            if (handler != null) handler(this, e);
        }

        private void PrepareBlur()
        {

            if (blur == null)
                blur = Camera.main.GetOrAddComponent<Blur>();

            blur.blurShader = Shader.Find("Hidden/FastBlur");
            blur.downsample = 1;
            blur.blurIterations = 1;
            blur.enabled = true;
        }

        private void Awake()
        {
            PrepareBlur();
            blur.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                PlayBlurIn();
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                PlayBlurOut();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                PlayBlurInContinuous();
            }
        }

    }
}
