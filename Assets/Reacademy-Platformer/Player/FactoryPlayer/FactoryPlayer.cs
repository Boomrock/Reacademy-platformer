using Player;
using UnityEngine;

namespace Academy_Platformer.Player.FactoryPlayer
{
    public class FactoryPlayer : IFactoryCharacter
    {
        private PlayerView _playerPrefab;

        public FactoryPlayer()
        {
            _playerPrefab = Resources.Load<PlayerView>(ResourcesConst.PlayerPrefab);
        }
        
        public PlayerView Create(PlayerModel playerModel, PlayerView playerView)
        {
            playerView = GameObject.Instantiate(_playerPrefab);
            
            return playerView;
        }
    }
}