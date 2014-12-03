////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using Assets.Extensions.IOSNative.GameCenter;
using UnityEngine;

namespace Assets.Extensions.IOSNative.xExample.Scripts.Multiplayer.ptpGame.GUI
{
    public class ConectionButton : MonoBehaviour {

        //--------------------------------------
        // INITIALIZE
        //--------------------------------------

        private float w;
        private float h;

        private Rect r;

        void Start() {
            w = Screen.width * 0.2f;
            h = Screen.height * 0.1f;

            r.x = (Screen.width - w) / 2f;
            r.y = (Screen.height - h) / 2f;

            r.width = w;
            r.height = h;
        }

        //--------------------------------------
        //  PUBLIC METHODS
        //--------------------------------------

        void OnGUI() {
            if(UnityEngine.GUI.Button(r, "Find Match")) {
                GameCenterMultiplayer.instance.FindMatch (2, 2);
            }
        }
	
        //--------------------------------------
        //  GET/SET
        //--------------------------------------
	
        //--------------------------------------
        //  EVENTS
        //--------------------------------------
	
        //--------------------------------------
        //  PRIVATE METHODS
        //--------------------------------------
	
        //--------------------------------------
        //  DESTROY
        //--------------------------------------

    }
}