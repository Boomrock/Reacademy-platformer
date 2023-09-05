using UnityEngine;

namespace UI.HUD
{
    public class HUDWindowController
    {
        private readonly UIService.UIService _uiService;
        
        private HUDWindow _hudWindowWindow;

        public HUDWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;
            _hudWindowWindow = uiService.Get<HUDWindow>();
            
            SetParameters();
        }
        
        public void ChangeHealthPoint(float healthPoint)
        {
            healthPoint = ChekHPPoint(healthPoint, _hudWindowWindow.СurrentHealth);
            _hudWindowWindow.ChangeHealthBar(healthPoint);
        }
        
        public void ChangeScore(int score)
        {
            _hudWindowWindow.ChangeScoreText(score);
        }
        
        public void SetParameters(int score = 0, float healthPoint = 100f)
        {
            ChangeScore(score);
            
            healthPoint = ChekHPPoint(healthPoint);
            _hudWindowWindow.ChangeHealthBar(healthPoint);
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
    }
}