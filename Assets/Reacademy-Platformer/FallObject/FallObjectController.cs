using System;
using Player;
using UnityEngine;
using Zenject;

namespace FallObject
{
    public class FallObjectController : IFixedTickable
    {
        public static event Action<float> DamageToPlayerNotify;
        public event Action<FallObjectController> PlayerCatchFallingObjectNotify;
        public event Action<FallObjectController> DeathAnimationEndedNotify;
        public event Action<FallObjectController> ObjectFellNotify;
        public int PointsPerObject => _pointsPerObject;
        public FallObjectView View => _view;
        public FallObjectModel Model => _model;
        public int Damage => _damage;

        private readonly TickableManager _tickableManager;
        private readonly Vector3 _defaultScale = new Vector3(0.15f, 0.15f, 0.15f);
        private readonly Vector3 _deltaVector = new Vector3(0, -0.001f, 0);
        private readonly FallObjectAnimator _animator;
        private readonly FallObjectView _view;
        private readonly FallObjectModel _model;
        private int _pointsPerObject;
        private readonly float _minPositionY = -7f;
        private float _fallSpeed;
        private int _damage;
        private bool _isCatched;

        public FallObjectController(
            FallObjectView view,
            FallObjectModel model, 
            TickableManager tickableManager)
        {
            _model = model;
            _tickableManager = tickableManager;
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

                /*if (_model.Type == FallObjectType.Type2)
                {
                    DamageToPlayerNotify?.Invoke(_damage);
                }*/
            }
        }

        public void FixedTick()
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
            _view.transform.localScale = _defaultScale;
            View.gameObject.SetActive(value);
            _isCatched = !value;
            if (value)
            {
                _tickableManager.AddFixed(this);
            }
            else
            {
                Debug.Log("RemoveFixed");
                _tickableManager.RemoveFixed(this);   
            }
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