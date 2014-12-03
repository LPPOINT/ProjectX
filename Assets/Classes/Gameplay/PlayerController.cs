using System;
using Assets.Classes.Foundation.Classes;
using UnityEngine;

namespace Assets.Classes.Gameplay
{
    public class PlayerController : SingletonBehaviour<PlayerController>
    {
        public event EventHandler PlayerAction;

        protected virtual void OnPlayerAction()
        {
            var handler = PlayerAction;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                OnPlayerAction();
        }
    }
}
