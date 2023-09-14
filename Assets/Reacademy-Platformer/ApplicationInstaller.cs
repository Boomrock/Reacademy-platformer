using FallObject;
using Player;
using Reacademy_Platformer.Installs;
using Sounds;
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
            BindingUIServices.Install(Container);

            Container.Bind<FallObjectConfig>()
                .FromScriptableObjectResource(ResourcesConst.FallObjectConfigPath)
                .AsSingle();

            Container.Bind<FallObjectStorage>().AsSingle();
            
            Container.BindMemoryPool<FallObjectView, FallObjectView.Pool>()
                .WithInitialSize(2)
                .FromComponentInNewPrefabResource(ResourcesConst.FallObjectView)
                .UnderTransformGroup("FallObject");
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
