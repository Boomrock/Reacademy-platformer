using FallObject;
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
            Container.BindMemoryPool<FallObjectView, FallObjectView.Pool>().WithInitialSize(2)
                .FromComponentInNewPrefabResource("EnemyView")
                .UnderTransformGroup("Foos");
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


            Container.BindInterfacesAndSelfTo<GameController>()
                .AsSingle();
        }
        
    }
}
