using UnityEngine;

//Attach the script to the empty gameobject on your sceneS
namespace Assets.Extensions.IOSNative.iAd
{
    public class iAdIOSInterstitial : MonoBehaviour {


        // --------------------------------------
        // Unity Events
        // --------------------------------------


        void Start() {
            ShowBanner();
        }




        // --------------------------------------
        // PUBLIC METHODS
        // --------------------------------------

        public void ShowBanner() {
            iAdBannerController.instance.StartInterstitialAd();
        }



        // --------------------------------------
        // GET / SET
        // --------------------------------------



        public string sceneBannerId {
            get {
                return Application.loadedLevelName + "_" + this.gameObject.name;
            }
        }

	
    }
}
