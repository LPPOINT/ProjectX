﻿using System;
using System.Collections;
using UnityEngine;

namespace Assets.Extensions.StansAssetsCommon
{
    public class SA_ScreenShotMaker : SAC_Singleton<SA_ScreenShotMaker> {

        //Actions
        public Action<Texture2D> OnScreenshotReady;



        public void GetScreenshot() {
            StartCoroutine(SaveScreenshot());
        }

        private IEnumerator SaveScreenshot() {
		
            yield return new WaitForEndOfFrame();
            // Create a texture the size of the screen, RGB24 format
            int width = Screen.width;
            int height = Screen.height;
            Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
            // Read screen contents into the texture
            tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
            tex.Apply();

            if(OnScreenshotReady != null) {
                OnScreenshotReady(tex);
            }

        }
	
    }
}
