using System;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator
    {
        private readonly PlayerView _playerView;

        private Sequence _sequenceSpawn;
        private Sequence _sequenceGetDamage;
        private Sequence _sequenceDeath;

        private readonly Vector3 _startPositionPlayer;
        private readonly Vector3 _endPositionPlayer;

        private const float FactorOffsetX = 10f;
        private const float FactorPixelHeight = 5f;
        private const float FactorPixelWidth = 2f;
        private const float DurationSpawn = 5f;
        private const float DurationGetDamage = 0.1f;
        private const float DurationDeath = 1f;
        private const float DelaySpawn = 0.5f;
        private const float VisibilityAlphaGetDamage = 0f;
        private const float InvisibilityAlphaGetDamage = 1f;
        private const float IncreasePlayerDeath = 0.7f;
        private const float DecreasePlayerDeath = 0f;
        private const int NumberRepetitionsGetDamage = 5;
        
        public PlayerAnimator(PlayerView playerView, UnityEngine.Camera cameraView)
        {
            _playerView = playerView;

            if (cameraView != null)
            {
                _startPositionPlayer = cameraView.ScreenToWorldPoint(new Vector3(
                    -FactorOffsetX*playerView.SpriteRenderer.size.x, 
                    cameraView.pixelHeight / FactorPixelHeight, 
                    -cameraView.transform.position.z));
                _endPositionPlayer =  cameraView.ScreenToWorldPoint(new Vector3(
                    cameraView.pixelWidth / FactorPixelWidth, 
                    cameraView.pixelHeight / FactorPixelHeight, 
                    -cameraView.transform.position.z));
            }
            else
            {
                Debug.LogError("Component cameraView is null");
            }
        }
        
        public void Spawn()
        {
            _playerView.transform.position = _startPositionPlayer;

            _sequenceSpawn?.Kill();
            
            _sequenceSpawn = DOTween.Sequence();
            
            _sequenceSpawn.Append(_playerView.transform
                .DOMove(_endPositionPlayer, DurationSpawn)
                .SetEase(Ease.OutBack)
                .SetDelay(DelaySpawn));
        }
        
        public void GetDamage()
        {
            _sequenceGetDamage?.Kill();
            
            _sequenceGetDamage = DOTween.Sequence();
            
            _sequenceGetDamage
                .Append(_playerView.SpriteRenderer.DOFade(VisibilityAlphaGetDamage, DurationGetDamage))
                .Append(_playerView.SpriteRenderer.DOFade(InvisibilityAlphaGetDamage, DurationGetDamage))
                .SetLoops(NumberRepetitionsGetDamage);
        }

        public void Death(TweenCallback setEndWindow = null)
        {
            _sequenceDeath?.Kill();
                
            _sequenceDeath = DOTween.Sequence();

            _sequenceDeath
                .Append(_playerView.transform.DOScale(IncreasePlayerDeath, DurationDeath).SetEase(Ease.InOutBounce))
                .Append(_playerView.transform.DOScale(DecreasePlayerDeath, DurationDeath).SetEase(Ease.InBounce)
                    .OnComplete(setEndWindow));
        }
    }
}