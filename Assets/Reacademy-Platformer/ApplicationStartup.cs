using Bootstrap;
using Camera;

using UnityEngine;

public class ApplicationStartup : MonoBehaviour
{
    private IBootstrap _bootstrap = new Bootstrap.Bootstrap();
    private UnityEngine.Camera _camera;

    private void Start()
    {
        StartBootstrap();
    }

    private void StartBootstrap()
    {
        _bootstrap.Add(new CreateMainCameraCommand(out _camera));


        _bootstrap.OnExecuteAllComandsNotify += NotifyOfCompletion;
        _bootstrap.Execute();
    }

    private void NotifyOfCompletion()
    { }
}