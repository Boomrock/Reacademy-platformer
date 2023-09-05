using UnityEngine;

namespace Player
{
    public class PlayerMovementController
    {
        private InputController _inputController;
        
        private readonly PlayerView _playerView;

        private const float Speed = 5f;
   
        private readonly Vector3 _leftPointStop;
        private readonly Vector3 _rightPointStop;
        private readonly float _step;

        public PlayerMovementController(InputController inputController, PlayerView playerView, PlayerController playerController)
        {
            _playerView = playerView;

            _inputController = inputController;
            playerController.OnDisposed += Disposed;

            _step = Speed * Time.deltaTime;
            _leftPointStop =  UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));
            _rightPointStop =  UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        
            _inputController.OnLeftEvent += MoveLeft;
            _inputController.OnRightEvent += MoveRight;
        }

        private void MoveLeft()
        {
            if (_playerView.transform.position.x > _leftPointStop.x + _playerView.SpriteRenderer.bounds.size.x/2f)
            {
                var position = _playerView.transform.position;
                var target = position + Vector3.left;
                _playerView.transform.position = Vector3.MoveTowards( position, target, _step);;
            }
        }

        private void MoveRight()
        {
            if (_playerView.transform.position.x < _rightPointStop.x - _playerView.SpriteRenderer.bounds.size.x / 2f)
            {
                var position = _playerView.transform.position;
                var target = position + Vector3.right;
                _playerView.transform.position = Vector3.MoveTowards(position, target, _step);
            }
        }

        public void Disposed()
        {
            _inputController.OnLeftEvent -= MoveLeft;
            _inputController.OnRightEvent -= MoveRight;
        }
    }
}