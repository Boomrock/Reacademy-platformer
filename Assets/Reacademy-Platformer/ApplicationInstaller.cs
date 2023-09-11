using Player;
using Sounds;
using UI.HUD;
using UI.UIService;
using UI.UIWindows;
using UnityEngine;
using Zenject;

namespace Reacademy_Platformer
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
        
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

 
            
            Container.Bind<UIService>().AsSingle();
            Container.Bind<UIGameWindowController>().AsSingle();
            Container.Bind<UIEndGameWindowController>().AsSingle();
            Container.Bind<UIMainMenuController>().AsSingle();
            Container.Bind<HUDWindowController>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameController>()
                .AsSingle();

        }
    }
}
