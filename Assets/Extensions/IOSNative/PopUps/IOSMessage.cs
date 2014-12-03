////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using System;
using Assets.Classes.Infrastructure.FlashLikeEvents.Data;
using Assets.Extensions.IOSNative.PopUps.@base;
using UnityEngine;

namespace Assets.Extensions.IOSNative.PopUps
{
    public class IOSMessage : BaseIOSPopup {
	
	
        public string ok;
        public Action OnComplete = delegate {};
	
        //--------------------------------------
        // INITIALIZE
        //--------------------------------------
	
        public static IOSMessage Create(string title, string message) {
            return Create(title, message, "Ok");
        }
		
        public static IOSMessage Create(string title, string message, string ok) {
            IOSMessage dialog;
            dialog  = new GameObject("IOSPopUp").AddComponent<IOSMessage>();
            dialog.title = title;
            dialog.message = message;
            dialog.ok = ok;
		
            dialog.init();
		
            return dialog;
        }
	
	
        //--------------------------------------
        //  PUBLIC METHODS
        //--------------------------------------
	
        public void init() {
            IOSNativePopUpManager.showMessage(title, message, ok);
        }
	
        //--------------------------------------
        //  GET/SET
        //--------------------------------------
	
        //--------------------------------------
        //  EVENTS
        //--------------------------------------
	
        public void onPopUpCallBack(string buttonIndex) {
		
            dispatch(BaseEvent.COMPLETE);
            OnComplete();
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
