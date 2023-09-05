using Player;
using Sounds;
using UI.HUD;
using UI.UIService;
using UI.UIWindows;

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
    
    public GameController(UnityEngine.Camera camera)
    {
        _soundController = new SoundController();
        _camera = camera;
        
        UIInit();
        ScoreInit();
        
        _inputController = new InputController();
        _playerController = new PlayerController(_inputController, 
            _hudWindowController, 
            _camera, 
            _soundController);
        _spawner = new FallObjectSpawner(_scoreCounter);

        _playerController.PlayerHpController.OnZeroHealth += StopGame;
    }
    
    private void UIInit()
    {
        _uiService = new UIService(_camera);
            
        _mainMenuWindowController = new UIMainMenuController(_uiService, this);
        _gameWindowController = new UIGameWindowController(_uiService);
        _endMenuWindowController = new UIEndGameWindowController(_uiService, this);
        _hudWindowController = new HUDWindowController(_uiService);
    }

    private void ScoreInit()
    {
        _scoreCounter = new ScoreCounter(_soundController);
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
        _spawner.StartSpawn();
        TickableManager.TickableManager.UpdateNotify += Update;
    }

    public void StopGame()
    {
        _playerController.DestroyView(()=>_gameWindowController.ShowEndMenuWindow());
        _spawner.StopSpawn();
        TickableManager.TickableManager.UpdateNotify -= Update;
    }

    private void Update()
    { }
}
