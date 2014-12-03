////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


namespace Assets.Extensions.IOSNative.xExample.Scripts.Multiplayer.ptpGame.packages
{
    public class ObjectCreatePackage : BasePackage {

        //--------------------------------------
        // INITIALIZE
        //--------------------------------------

        public ObjectCreatePackage(float x, float y) {

            initWriter ();

            writeFloat (x);
            writeFloat (y); 


        }

        //--------------------------------------
        //  PUBLIC METHODS
        //--------------------------------------

        public override int getId() {
            return 1;
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
