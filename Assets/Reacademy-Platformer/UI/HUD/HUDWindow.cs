using UI.UIWindows;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class HUDWindow : UIAnimationWindow
    {
        [SerializeField] private Text text;
        [SerializeField] private Transform healthsBar;
        
        public float СurrentHealth
        {
            get
            {
                var healthsBarLocalScale = healthsBar.localScale;
                return healthsBarLocalScale.x;
            }
        }

        public void ChangeHealthBar(float healthPoint)
        {
            var healthsBarLocalScale = healthsBar.localScale;
            healthsBarLocalScale.x = healthPoint;
            healthsBar.localScale = healthsBarLocalScale;
        }

        public void ChangeScoreText(int score)
        {
            text.text = score.ToString();
        }
    }
}