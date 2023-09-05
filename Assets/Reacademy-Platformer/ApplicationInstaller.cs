using Player;
using UnityEngine;
using Zenject;

namespace Reacademy_Platformer
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            /*public GameController(
                PlayerController playerController,
                SoundController soundController,
                FallObjectSpawner fallObjectSpawner,
                UIService uiService,
                ScoreCounter scoreCounter
            )*/
            /*public PlayerController(
                InputController inputController,
                HUDWindowController hudWindowController,
                UnityEngine.Camera camera,
                SoundController soundController)
            {*/
            var camera = Container.InstantiatePrefabResourceForComponent<UnityEngine.Camera>(ResourcesConst.MainCamera);
            Container.BindInterfacesAndSelfTo<InputController>()
                .AsSingle();
            
            Container.Bind<PlayerController>();
            Container.Bind<UnityEngine.Camera>()
                .FromInstance(camera);
           
            Container.Bind<GameController>()
                .FromNew()
                .AsSingle();

        }
    }
}
