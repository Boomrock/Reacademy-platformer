using DG.Tweening;
using UI.UIService;

namespace UI.UIWindows
{
    public class UIAnimationWindow : UIWindow
    {
        private const float Duration = 1f;
        
        private Tween _showAnimation;
        private Tween _hideAnimation;
        public override void Show()
        {
            _showAnimation?.Kill();
            
            _showAnimation = transform
                .DOMoveY(-5, Duration)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    OnShowEvent.Invoke();
                });
                
        }

        public override void Hide()
        {
            var transformPosition = transform.position;
            
            _hideAnimation?.Kill();
            _hideAnimation = transform
                .DOMoveY(-transformPosition.y*2, Duration)
                .OnComplete(() =>
                {
                    OnHideEvent.Invoke();
                });
        }
    }
}