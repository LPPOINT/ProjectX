
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
using System.Runtime.InteropServices;
#endif

namespace Assets.Extensions.IOSNative.Market
{
    public class IOSNativeMarketBridge  {

#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
	[DllImport ("__Internal")]
	private static extern void _loadStore(string ids);
	
	[DllImport ("__Internal")]
	private static extern void _restorePurchases();
	
	
	[DllImport ("__Internal")]
	private static extern void _buyProduct(string id);
	
	[DllImport ("__Internal")]
	private static extern void _verifyLastPurchase(string url);
	#endif


        public static void loadStore(string ids) {
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_loadStore(ids);
		#endif
        }
	
        public static void buyProduct(string id) {
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_buyProduct(id);
		#endif
        }
	
        public static void restorePurchases() {
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_restorePurchases();
		#endif
        }
	
        public static void verifyLastPurchase(string url) {
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_verifyLastPurchase(url);
		#endif
        }

    }
}

