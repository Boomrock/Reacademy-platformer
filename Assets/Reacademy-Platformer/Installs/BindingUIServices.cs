using UI.HUD;
using UI.UIService;
using UI.UIWindows;
using Zenject;

namespace Reacademy_Platformer.Installs
{
    public class BindingUIServices: Installer<BindingUIServices>
    {
        public override void InstallBindings()
        {
            
            Container.Bind<UIRoot>()
                .FromComponentInNewPrefabResource(ResourcesConst.UIRoot)
                .AsSingle();
            Container.Bind<UIService>().AsSingle();

            Container.Bind<UIMainMenuWindow>().FromComponentInNewPrefabResource(ResourcesConst.UIMainMenuWindow)
                .AsSingle();
            Container.Bind<UIGameWindow>().FromComponentInNewPrefabResource(ResourcesConst.UIGameWindow)
                .AsSingle();
            Container.Bind<UIEndGameWindow>().FromComponentInNewPrefabResource(ResourcesConst.UIEndGameWindow)
                .AsSingle();
            Container.Bind<HUDWindow>().FromComponentInNewPrefabResource(ResourcesConst.HUDWindow)
                .AsSingle();
            
            Container.Bind<UIMainMenuController>()
                .AsSingle();
            Container.Bind<UIGameWindowController>()
                .AsSingle()
                .NonLazy();
            Container.Bind<UIEndGameWindowController>()
                .AsSingle()
                .NonLazy();
            Container.Bind<HUDWindowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}