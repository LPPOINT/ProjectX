////////////////////////////////////////////////////////////////////////////////
//  
// @module Common Android Native Lib
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using Assets.Classes.Infrastructure.FlashLikeEvents.Core.Events;
using UnityEngine;

namespace Assets.Extensions.IOSNative.Core
{
    public abstract class ISN_Singleton<T> : EventDispatcher where T : MonoBehaviour {

        private static T _instance = null;
        private static bool applicationIsQuitting = false;
	
	
        public static T instance {
		
            get {
                if(applicationIsQuitting) {
                    Debug.Log(typeof(T) + " [Mog.Singleton] is already destroyed. Returning null. Please check HasInstance first before accessing instance in destructor.");
                    return null;
                }
			
                if (_instance == null) {
                    _instance = GameObject.FindObjectOfType(typeof(T)) as T;
                    if (_instance == null) {
                        _instance = new GameObject ().AddComponent<T> ();
                        _instance.gameObject.name = _instance.GetType ().Name;
                    }
                }
			
                return _instance;
			
            }
		
        }
	
        public static bool HasInstance {
            get {
                return !IsDestroyed;
            }
        }
	
        public static bool IsDestroyed {
            get {
                if(_instance == null) {
                    return true;
                } else {
                    return false;
                }
            }
        }
	
	
	
        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        protected override void OnDestroy () {
            base.OnDestroy();
            _instance = null;
            applicationIsQuitting = true;
        }
	
        protected virtual void OnApplicationQuit () {
            _instance = null;
            applicationIsQuitting = true;
        }

    }
}
