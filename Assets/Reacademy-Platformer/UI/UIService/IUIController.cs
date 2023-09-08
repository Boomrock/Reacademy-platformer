namespace UI.UIService
{
    public interface IUIController
    {
        public IUIWindow UIWindow { get; }
        void ShowWindow();
        void HideWindow();
    }
}