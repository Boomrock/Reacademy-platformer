using Academy_Platformer;
using UnityEngine;

namespace Camera
{
    public class CreateMainCameraCommand : Command.Command
    {
        private readonly UnityEngine.Camera _mainCameraPrefab;
        private UnityEngine.Camera _mainCamera;

        public CreateMainCameraCommand(out UnityEngine.Camera camera)
        {
            _mainCameraPrefab = Resources.Load<UnityEngine.Camera>(ResourcesConst.MainCamera);
            _mainCamera = Object.Instantiate(_mainCameraPrefab);
            camera = _mainCamera;
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Undo()
        {
            Object.Destroy(_mainCamera);
        }
    }
}