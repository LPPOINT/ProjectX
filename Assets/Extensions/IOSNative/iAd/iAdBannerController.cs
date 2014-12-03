////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using Assets.Classes.Infrastructure.FlashLikeEvents.Core.Events;
using UnityEngine;

#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
using System.Runtime.InteropServices;
#endif

namespace Assets.Extensions.IOSNative.iAd
{
    public class iAdBannerController : EventDispatcher {

#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
	[DllImport ("__Internal")]
	private static extern void _IADDestroyBanner(int id);


	[DllImport ("__Internal")]
	private static extern void _IADStartInterstitialAd();

	[DllImport ("__Internal")]
	private static extern void _IADLoadInterstitialAd();

	[DllImport ("__Internal")]
	private static extern void _IADShowInterstitialAd();
	#endif



        private static int _nextId = 0;
        private static iAdBannerController _instance;
        private Dictionary<int, iAdBanner> _banners; 

        //Actions
        public Action InterstitialDidFailWithErrorAction 	= delegate {};
        public Action InterstitialAdWillLoadAction 			= delegate {};
        public Action InterstitialAdDidLoadAction 			= delegate {};
        public Action InterstitialAdDidFinishAction			= delegate {};
	

	
        //--------------------------------------
        // INITIALIZE
        //--------------------------------------

        void Awake() {
            _banners =  new Dictionary<int, iAdBanner>();
            DontDestroyOnLoad(gameObject);
        }


        public static iAdBannerController instance {

            get {
                if (_instance == null) {
                    _instance = GameObject.FindObjectOfType(typeof(iAdBannerController)) as iAdBannerController;
                    if (_instance == null) {
                        _instance = new GameObject ("iAdBannerController").AddComponent<iAdBannerController> ();
                    }
                }

                return _instance;

            }

        }

        //--------------------------------------
        //  PUBLIC METHODS
        //--------------------------------------

        public iAdBanner CreateAdBanner(TextAnchor anchor)  {

		
            iAdBanner bannner = new iAdBanner(anchor, nextId);
            _banners.Add(bannner.id, bannner);
		
            return bannner;
		
        }
	
	
        public iAdBanner CreateAdBanner(int x, int y)  {

            iAdBanner bannner = new iAdBanner(x, y, nextId);
            _banners.Add(bannner.id, bannner);
		
            return bannner;
        }


        public void DestroyBanner(int id) {
            if(_banners != null) {
                if(_banners.ContainsKey(id)) {
                    _banners.Remove(id);
				
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
					_IADDestroyBanner(id);
				#endif
                }
            }
        }


        public void StartInterstitialAd() {
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_IADStartInterstitialAd();
		#endif
        }
	
        public void LoadInterstitialAd() {
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_IADLoadInterstitialAd();
		#endif
        }
	
        public void ShowInterstitialAd() {
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_IADShowInterstitialAd();
		#endif
        }

	
        //--------------------------------------
        //  GET/SET
        //--------------------------------------

        public static int nextId {
            get {
                _nextId++;
                return _nextId;
            }
        }



	
        public iAdBanner GetBanner(int id) {
            if(_banners.ContainsKey(id)) {
                return _banners[id];
            } else {
                Debug.LogWarning("Banner id: " + id.ToString() + " not found");
                return null;
            }
        }
	
	
	
        public List<iAdBanner> banners {
            get {
			
                List<iAdBanner> allBanners =  new List<iAdBanner>();
                if(_banners ==  null) {
                    return allBanners;
                }
			
                foreach(KeyValuePair<int, iAdBanner> entry in _banners) {
                    allBanners.Add(entry.Value);
                }
			
                return allBanners;
			
			
            }
        }

        //--------------------------------------
        //  EVENTS
        //--------------------------------------

        private void didFailToReceiveAdWithError(string bannerID) {
            int id = System.Convert.ToInt32(bannerID);
            iAdBanner banner = GetBanner(id) as iAdBanner;
            if(banner != null) {
                banner.didFailToReceiveAdWithError();
            }

        }


        private void bannerViewDidLoadAd(string bannerID) {
            int id = System.Convert.ToInt32(bannerID);
            iAdBanner banner = GetBanner(id) as iAdBanner;
            if(banner != null) {
                banner.bannerViewDidLoadAd();
            }
        }


        private void bannerViewWillLoadAd(string bannerID){
            int id = System.Convert.ToInt32(bannerID);
            iAdBanner banner = GetBanner(id) as iAdBanner;
            if(banner != null) {
                banner.bannerViewWillLoadAd();
            }
        }


        private void bannerViewActionDidFinish(string bannerID){
            int id = System.Convert.ToInt32(bannerID);
            iAdBanner banner = GetBanner(id) as iAdBanner;
            if(banner != null) {
                banner.bannerViewActionDidFinish();
            }
        }

        private void bannerViewActionShouldBegin(string bannerID){
            int id = System.Convert.ToInt32(bannerID);
            iAdBanner banner = GetBanner(id) as iAdBanner;
            if(banner != null) {
                banner.bannerViewActionShouldBegin();
            }
        }



        private void interstitialdidFailWithError(string data) {
            dispatch(iAdEvent.INTERSTITIAL_DID_FAIL_WITH_ERROR);
            InterstitialDidFailWithErrorAction();
        }

        private void interstitialAdWillLoad(string data) {
            dispatch(iAdEvent.INTERSTITIAL_AD_WILL_LOAD);
            InterstitialAdWillLoadAction();
        }

        private void interstitialAdDidLoad(string data) {
            dispatch(iAdEvent.INTERSTITIAL_AD_DID_LOAD);
            InterstitialAdDidLoadAction();
        }

        private void interstitialAdActionDidFinish(string data) {
            dispatch(iAdEvent.INTERSTITIAL_AD_ACTION_DID_FINISH);
            InterstitialAdDidFinishAction();
        }






	
        //--------------------------------------
        //  PRIVATE METHODS
        //--------------------------------------
	
        //--------------------------------------
        //  DESTROY
        //--------------------------------------

    }
}
