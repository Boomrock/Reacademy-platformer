using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIButton : MonoBehaviour, IPointerClickHandler
    {
        public Action OnClickButton;
        
        [SerializeField] private Button button;

        [SerializeField] private bool useColor = true;
        [SerializeField] private Color downColor;
        
        [SerializeField] private bool useSprite;
        [SerializeField] private Sprite downImage;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickButton?.Invoke();
        }
        
        private void OnEnable()
        {
            OnClickButton += OnClick;
        }

        private void OnDisable()
        {
            OnClickButton -= OnClick;
        }
        
        private void OnClick()
        {
            if (useColor)
            {
                ChangeColor();
            }
            if (useSprite)
            {
                ChangeSprite();
            }
        }
        
        private void ChangeColor()
        {
            button.transition = Selectable.Transition.ColorTint;
            var buttonColors = button.colors;
            buttonColors.selectedColor = downColor;
            button.colors = buttonColors;
        }

        private void ChangeSprite()
        {
            button.transition = Selectable.Transition.SpriteSwap;
            var buttonSprite = button.spriteState;
            buttonSprite.selectedSprite = downImage;
            button.spriteState = buttonSprite;
        }
    }
}