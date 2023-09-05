using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStorage
    {
        private List<PlayerView> _listObject = new List<PlayerView>();

        public void Add(PlayerView playerView)
        {
            if (playerView == null)
            {
                Debug.Log($"Component {playerView} is null");
                
                return;
            }

            if (_listObject.Contains(playerView))
            {
                Debug.Log($"Component {playerView} has already been written to the list");
                
                return;
            }
            
            _listObject.Add(playerView);
        }

        public void Delete(PlayerView playerView)
        {
            if (!_listObject.Contains(playerView))
            {
                Debug.Log($"The component {playerView} is not in the list"); 
                
                return;
            }
            
            _listObject.Remove(playerView);
            
            Object.Destroy(playerView.gameObject);
        }
    }
}