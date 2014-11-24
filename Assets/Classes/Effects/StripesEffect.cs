using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Classes.Effects
{
    public class StripesEffect : ScreenEffect
    {
        public enum StripesState
        {
            Showing,
            Showed,
            Hiding,
            Hided
        }


        [Serializable]
        public class StripesMotion
        {


            public static StripesMotion DefaultShowing = new StripesMotion(0.9f, iTween.EaseType.easeOutCirc, UnityEngine.Color.black);
            public static StripesMotion DefaultHiding = new StripesMotion(0.4f, iTween.EaseType.easeOutCirc, UnityEngine.Color.black);

            public StripesMotion(float duration, iTween.EaseType easeType, Color color)
            {
                Duration = duration;
                EaseType = easeType;
                Color = color;
            }

            public float Duration;
            public iTween.EaseType EaseType;
            public Color Color;
        }

        public StripesMotion ShowingMotion = StripesMotion.DefaultShowing;
        public StripesMotion HidingMotion = StripesMotion.DefaultHiding;

        public StripesState CurrentState { get; private set; }

        public bool IsVisible
        {
            get { return CurrentState == StripesState.Showed || CurrentState == StripesState.Showing; }
        }

        public Image Top;
        public Image Bottom;

        private void OnITweenShowingAnimationDone()
        {
            CurrentState = StripesState.Showed;
        }
        private void OnITweenHidingAnimationDone()
        {
            CurrentState = StripesState.Hided;
        }

        private void Awake()
        {
            CurrentState = StripesState.Hided;
        }


        private void EstablishColor(StripesMotion source)
        {
            Top.color = source.Color;
            Bottom.color = source.Color;
        }

        private float CalculateImageWorldHeight(Image image)
        {
            var corners = new Vector3[4];
            image.rectTransform.GetWorldCorners(corners);



            var h =  Math.Abs(corners[0].y - corners[1].y);
            return h;
        }

        public void Show(StripesMotion motion)
        {
            EstablishColor(motion);

            iTween.MoveBy(Top.gameObject, iTween.Hash("amount", new Vector3(0, -CalculateImageWorldHeight(Top)), "time", motion.Duration, "easetype", motion.EaseType, "oncomplete", "OnITweenShowingAnimationDone", "oncompletetarget", gameObject));
            iTween.MoveBy(Bottom.gameObject, iTween.Hash("amount", new Vector3(0, CalculateImageWorldHeight(Bottom)), "time", motion.Duration, "easetype", motion.EaseType));

            CurrentState = StripesState.Showing;

        }
        public void Hide(StripesMotion motion)
        {
            EstablishColor(motion);

            iTween.MoveBy(Top.gameObject, iTween.Hash("amount", new Vector3(0, CalculateImageWorldHeight(Top)), "time", motion.Duration, "easetype", motion.EaseType, "oncomplete", "OnITweenHidingAnimationDone", "oncompletetarget", gameObject));
            iTween.MoveBy(Bottom.gameObject, iTween.Hash("amount", new Vector3(0, -CalculateImageWorldHeight(Bottom)), "time", motion.Duration, "easetype", motion.EaseType));

            CurrentState = StripesState.Hiding;
        }

        public void Switch()
        {
            if (CurrentState == StripesState.Showed)
            {
                Hide();
            }
            else if (CurrentState == StripesState.Hided)
            {
                Show();
            }
        }

        public void Show()
        {
            Show(StripesMotion.DefaultShowing);
        }
        public void Hide()
        {
            Hide(StripesMotion.DefaultHiding);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Switch();
            }
        }
    }
}
