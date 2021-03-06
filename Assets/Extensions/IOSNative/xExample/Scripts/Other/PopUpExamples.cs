////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using Assets.Extensions.IOSNative.Core;
using Assets.Extensions.IOSNative.Enum;
using Assets.Extensions.IOSNative.PopUps;
using Assets.Extensions.IOSNative.PopUps.@base;
using Assets.Extensions.IOSNative.xExample.Scripts.Base;
using UnityEngine;

namespace Assets.Extensions.IOSNative.xExample.Scripts.Other
{
    public class PopUpExamples : BaseIOSFeaturePreview {

        //--------------------------------------
        // INITIALIZE
        //--------------------------------------

        void Awake() {
            IOSNativePopUpManager.showMessage ("Wlcome", "Hey there, welcome to pop-ups testing scene!");
        }

        //--------------------------------------
        //  PUBLIC METHODS
        //--------------------------------------
	

        void OnGUI() {

            UpdateToStartPos();
		
            GUI.Label(new Rect(StartX, StartY, Screen.width, 40), "Native Pop Ups", style);

            StartY+= YLableStep;
            if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Rate PopUp with events")) {
                IOSRateUsPopUp rate = IOSRateUsPopUp.Create("Like this game?", "Please rate to support future updates!");
                rate.OnComplete += onRatePopUpClose;
		
            }
		

            StartX += XButtonStep;
            if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Dialog PopUp")) {
                IOSDialog dialog = IOSDialog.Create("Dialog Titile", "Dialog message");
                dialog.OnComplete += onDialogClose;

            }


            StartX += XButtonStep;
            if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Message PopUp")) {
                IOSMessage msg = IOSMessage.Create("Message Titile", "Message message");
                msg.OnComplete += onMessageClose;

            }


            StartX = XStartPos;
            StartY+= YButtonStep;
            if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Dismissed PopUp")) {
                Invoke ("dismissAler", 2f);
                IOSMessage.Create("Hello", "I will die in 2 sec");
            }


            StartX += XButtonStep;
            if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Open App Store")) {
                IOSNativeUtility.RedirectToAppStoreRatingPage();
            }


            StartX = XStartPos;
            StartY+= YButtonStep;
            if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Preloader ")) {
                IOSNativeUtility.ShowPreloader();
                Invoke("HidePreloader", 3f);
            }
		
		
            StartX += XButtonStep;
            if(GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Hide Preloader")) {
                HidePreloader();
            }
		
        }
	

        //--------------------------------------
        //  GET/SET
        //--------------------------------------
	
        //--------------------------------------
        //  EVENTS
        //--------------------------------------

        private void HidePreloader() {
            IOSNativeUtility.HidePreloader();
        }

        private void dismissAler() {
            IOSNativePopUpManager.dismissCurrentAlert ();
        }
	
        private void onRatePopUpClose(IOSDialogResult result) {
            switch(result) {
                case IOSDialogResult.RATED:
                    Debug.Log ("Rate button pressed");
                    break;
                case IOSDialogResult.REMIND:
                    Debug.Log ("Remind button pressed");
                    break;
                case IOSDialogResult.DECLINED:
                    Debug.Log ("Decline button pressed");
                    break;
			
            }

            IOSNativePopUpManager.showMessage("Result", result.ToString() + " button pressed");
        }
	
        private void onDialogClose(IOSDialogResult result) {

            //parsing result
            switch(result) {
                case IOSDialogResult.YES:
                    Debug.Log ("Yes button pressed");
                    break;
                case IOSDialogResult.NO:
                    Debug.Log ("No button pressed");
                    break;

            }

            IOSNativePopUpManager.showMessage("Result", result.ToString() + " button pressed");
        }
	
        private void onMessageClose() {
            Debug.Log("Message was just closed");
            IOSNativePopUpManager.showMessage("Result", "Message Closed");
        }
	
        //--------------------------------------
        //  PRIVATE METHODS
        //--------------------------------------
	
        //--------------------------------------
        //  DESTROY
        //--------------------------------------


    }
}
