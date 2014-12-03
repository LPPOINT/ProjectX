////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using Assets.Extensions.IOSNative.Enum;

namespace Assets.Extensions.IOSNative.Market.Templates
{
    public class IOSStoreKitResponse  {

        public string productIdentifier;
        public InAppPurchaseState state;
        public string receipt;

        public string error = string.Empty;


    }
}
