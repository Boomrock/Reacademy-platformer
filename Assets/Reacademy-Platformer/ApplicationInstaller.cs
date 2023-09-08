using Player;
using Sounds;
using UI.HUD;
using UI.UIService;
using UI.UIWindows;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Reacademy_Platformer
{
    public class ApplicationInstaller : MonoInstaller
    {
        private GameController _gameController;
        public override void InstallBindings()
        {
            /*public GameController(
                FallObjectSpawner fallObjectSpawner,
                */
            
            var camera = Container.InstantiatePrefabResourceForComponent<UnityEngine.Camera>(ResourcesConst.MainCamera);
            Container.Bind<UnityEngine.Camera>()
                .FromInstance(camera);
            
            Container.Bind<SoundPool>().AsSingle();
            Container.Bind<SoundController>().AsSingle();
            
       
            
            
            Container.BindInterfacesAndSelfTo<InputController>()
                .AsSingle();
            Container.Bind<ScoreCounter>().AsSingle();
            Container.Bind<FallObjectSpawner>().AsSingle();
            Container.Bind<PlayerController>()
                .AsSingle()
                .NonLazy();

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

            Container.BindInterfacesAndSelfTo<GameController>()
                .AsSingle();
        }
        
    }
}
