////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using System;
using Assets.Classes.Infrastructure.FlashLikeEvents.Data;
using Assets.Extensions.IOSNative.Core;
using Assets.Extensions.IOSNative.Enum;
using Assets.Extensions.IOSNative.PopUps.@base;
using UnityEngine;

namespace Assets.Extensions.IOSNative.PopUps
{
    public class IOSRateUsPopUp : BaseIOSPopup {
	
        public string rate;
        public string remind;
        public string declined;


        public Action<IOSDialogResult> OnComplete = delegate {};


        //--------------------------------------
        // INITIALIZE
        //--------------------------------------

        public static IOSRateUsPopUp Create() {
            return Create("Like the Game?", "Rate US");
        }
	
        public static IOSRateUsPopUp Create(string title, string message) {
            return Create(title, message, "Rate Now", "Ask me later", "No, thanks");
        }
	
        public static IOSRateUsPopUp Create(string title, string message, string rate, string remind, string declined) {
            IOSRateUsPopUp popup = new GameObject("IOSRateUsPopUp").AddComponent<IOSRateUsPopUp>();
            popup.title = title;
            popup.message = message;
            popup.rate = rate;
            popup.remind = remind;
            popup.declined = declined;
		
            popup.init();
		
	
            return popup;
        }
	
	
        //--------------------------------------
        //  PUBLIC METHODS
        //--------------------------------------
	
	
        public void init() {
            IOSNativePopUpManager.showRateUsPopUP(title, message, rate, remind, declined);
        }
	
	
        //--------------------------------------
        //  GET/SET
        //--------------------------------------
	
        //--------------------------------------
        //  EVENTS
        //--------------------------------------
	
        public void onPopUpCallBack(string buttonIndex) {
            int index = System.Convert.ToInt16(buttonIndex);
            switch(index) {
                case 0: 
                    IOSNativeUtility.RedirectToAppStoreRatingPage();
                    OnComplete(IOSDialogResult.RATED);
                    dispatch(BaseEvent.COMPLETE, IOSDialogResult.RATED);
                    break;
                case 1:
                    OnComplete(IOSDialogResult.REMIND);
                    dispatch(BaseEvent.COMPLETE, IOSDialogResult.REMIND);
                    break;
                case 2:
                    OnComplete(IOSDialogResult.DECLINED);
                    dispatch(BaseEvent.COMPLETE, IOSDialogResult.DECLINED);
                    break;
            }
		
		
		
            Destroy(gameObject);
        } 
	
        //--------------------------------------
        //  PRIVATE METHODS
        //--------------------------------------
	
        //--------------------------------------
        //  DESTROY
        //--------------------------------------


    }
}
