////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using Assets.Extensions.IOSNative.Enum;
using Assets.Extensions.IOSNative.GameCenter;
using Assets.Extensions.IOSNative.xExample.Scripts.Multiplayer.ptpGame.packages;

namespace Assets.Extensions.IOSNative.xExample.Scripts.Multiplayer.ptpGame.managers
{
    public class NetwrokManager  {

        //--------------------------------------
        // INITIALIZE
        //--------------------------------------



        //--------------------------------------
        //  PUBLIC METHODS
        //--------------------------------------

        public static void send(BasePackage pack) {
            GameCenterMultiplayer.instance.SendDataToAll (pack.getBytes(), GameCenterDataSendType.RELIABLE);
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
