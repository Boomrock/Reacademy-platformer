using UI.UIService;

namespace UI.HUD
{
    public class HUDWindowController : UIController<HUDWindow>
    {
        public HUDWindowController(HUDWindow window, UIRoot uiRoot) : base(window, uiRoot)
        {
            SetParameters();
        }
        
        public void ChangeHealthPoint(float healthPoint)
        {
            healthPoint = ChekHPPoint(healthPoint, _window.СurrentHealth);
            _window.ChangeHealthBar(healthPoint);
        }
        
        public void ChangeScore(int score)
        {
            _window.ChangeScoreText(score);
        }
        
        public void SetParameters(int score = 0, float healthPoint = 100f)
        {
            ChangeScore(score);
            
            healthPoint = ChekHPPoint(healthPoint);
            _window.ChangeHealthBar(healthPoint);
        }

        private float ChekHPPoint(float healthPoint, float currentHP = 0)
        {
            healthPoint /= 100;
            currentHP /= 100;
            
            if (healthPoint + currentHP > 1)
            {
                currentHP = 1;
            }
            else if (healthPoint + currentHP < 0)
            {
                currentHP = 0;
            }
            else
            {
                currentHP += healthPoint;
            }
            return currentHP;
        }

        public void ShowWindow()
        {
            var transform = _window.transform;
            transform.SetParent(_uiRoot.Container, false);
            var windowPosition = transform.position;
            windowPosition.y *= 2;
            transform.position = windowPosition;
            
            _window.Show();

        }

        public void HideWindow()
        {
            var transform = _window.transform;
            transform.SetParent(_uiRoot.Container, false);
            var windowPosition = transform.position;
            windowPosition.y *= 2;
            transform.position = windowPosition;
            
            _window.Hide();
        }

       
    }
}