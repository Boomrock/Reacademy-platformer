using Player;
using Sounds;
using UI.HUD;
using UI.UIService;
using UI.UIWindows;
using UnityEngine;

public class GameController
{
    private readonly UnityEngine.Camera _camera;
    
    private FallObjectSpawner _spawner;
    private InputController _inputController;
    private PlayerController _playerController;
    private UIService _uiService;
    private UIMainMenuController _mainMenuWindowController;
    private UIGameWindowController _gameWindowController;
    private UIEndGameWindowController _endMenuWindowController;
    private HUDWindowController _hudWindowController;
    private ScoreCounter _scoreCounter;
    private SoundController _soundController;
    
    public GameController(UnityEngine.Camera camera, 
        InputController inputController, 
        PlayerController playerController, 
        SoundController soundController,
        FallObjectSpawner fallObjectSpawner, 
        UIService uiService,
        ScoreCounter scoreCounter
        )
    {
        _soundController = soundController;
        _camera = camera;
        _inputController = inputController;
        _playerController = playerController;
        _spawner = fallObjectSpawner;
        _uiService = uiService;
        _scoreCounter = scoreCounter;
        _hudWindowController = _uiService.Get<HUDWindowController>();
        ScoreInit();
        _playerController.PlayerHpController.OnZeroHealth += StopGame;
    }
    


    private void ScoreInit()
    {
        _scoreCounter.ScoreChangeNotify += _hudWindowController.ChangeScore;
    }

    public void InitGame()
    {
        _uiService.Show<UIMainMenuWindow>();
        
        _soundController.Play(SoundName.BackStart, loop:true);
    }

    public void StartGame()
    {
        _soundController.Stop();
        _soundController.Play(SoundName.BackMain, loop:true);
        
        _playerController.Spawn();
        Debug.Log("as");
        _spawner.StartSpawn();
    }

    public void StopGame()
    {
        _playerController.DestroyView(()=>_gameWindowController.ShowEndMenuWindow());
        _spawner.StopSpawn();
    }
}
