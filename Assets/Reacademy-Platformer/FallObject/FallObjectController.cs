using System;
using Player;
using UnityEngine;

namespace FallObject
{
    public class FallObjectController
    {
        public static event Action<float> DamageToPlayerNotify;
        public event Action<FallObjectController> PlayerCatchFallingObjectNotify;
        public event Action<FallObjectController> DeathAnimationEndedNotify;
        public event Action<FallObjectController> ObjectFellNotify;
        public int PointsPerObject => _pointsPerObject;
        public FallObjectView View => _view;
        public FallObjectModel Model => _model;
        public int Damage => _damage;

        private Vector3 _defaultScale = new Vector3(0.15f, 0.15f, 0.15f);
        private Vector3 _deltaVector = new Vector3(0, -0.001f, 0);
        private FallObjectAnimator _animator;
        private FallObjectView _view;
        private FallObjectModel _model;
        private int _pointsPerObject;
        private float _minPositionY = -7f;
        private float _fallSpeed;
        private int _damage;
        private bool _isCatched;


        public FallObjectController(
            FallObjectView view,
            FallObjectModel model)
        {
            _model = model;
            _pointsPerObject = model.PointsPerObject;
            _fallSpeed = model.FallSpeed;
            _damage = model.Damage;

            _view = view;
            _view.transform.localScale = _defaultScale;
            
            _animator = new FallObjectAnimator(this);
            _animator.Spawn();
            _animator.DeathAnimationEnded += () => DeathAnimationEndedNotify?.Invoke(this);
            PlayerCatchFallingObjectNotify += (controller) => _animator.Death();
            
            _view.OnCollisionEnter2DNotify += OnCollisionEnter2D;
        }

        void OnCollisionEnter2D(Collision2D collision2D)
        {
            var player = collision2D.gameObject.GetComponent<PlayerView>();

            if (player != null && !_isCatched)
            {
                PlayerCatchFallingObjectNotify?.Invoke(this);
                _isCatched = true;

                if (_model.Type == FallObjectType.Type2)
                {
                    DamageToPlayerNotify?.Invoke(_damage);
                }
            }
        }

        private void FixedUpdate()
        {
            if (_view.transform.position.y <= _minPositionY)
            {
                ObjectFellNotify?.Invoke(this);
                DamageToPlayerNotify?.Invoke(_damage);
            }

            _view.transform.position += _deltaVector * _fallSpeed;
        }

        public void SetActive(bool value)
        {
            if (value == true)
            {
                TickableManager.TickableManager.FixedUpdateNotify += FixedUpdate;
                
            }
            else
            {
                TickableManager.TickableManager.FixedUpdateNotify -= FixedUpdate;
            }

            _view.transform.localScale = _defaultScale;
            View.gameObject.SetActive(value);
            _isCatched = !value;
        }
        
        public void SetModel(FallObjectModel model)
        {
            _pointsPerObject = model.PointsPerObject;
            _fallSpeed = model.FallSpeed;
            _damage = model.Damage;
            _view.SpriteRenderer.sprite = model.ObjectSprite;
        }
    }
}