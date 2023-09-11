using Player;
using Sounds;
using UI.HUD;
using UI.UIService;
using UI.UIWindows;
using Zenject;

public class GameController: IInitializable
{
    private readonly UnityEngine.Camera _camera;
    
    private FallObjectSpawner _spawner;
    private InputController _inputController;
    private PlayerController _playerController;
    private UIService _uiService;
    private UIMainMenuController _uiMainMenuController;
    private UIGameWindowController _uiGameWindowController;
    private UIEndGameWindowController _uiEndGameWindowController;
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
    }
    [Inject]
    private void UIInit(UIMainMenuController uiMainMenuController,
        UIGameWindowController uiGameWindowController, 
        UIEndGameWindowController uiEndGameWindowController,
        HUDWindowController hudWindowController)
    {
         _uiMainMenuController = uiMainMenuController;
         _uiGameWindowController = uiGameWindowController;
         _uiEndGameWindowController = uiEndGameWindowController;
         _hudWindowController = hudWindowController;

    }

    private void ScoreInit()
    {
        _scoreCounter.ScoreChangeNotify += _hudWindowController.ChangeScore;
    }
    
    public void StartGame()
    {
        _soundController.Stop();
        _soundController.Play(SoundName.BackMain, loop:true);
        
        _playerController.Spawn();
        _spawner.StartSpawn();
    }

    public void StopGame()
    {
        _playerController.DestroyView(()=>_uiGameWindowController.ShowEndMenuWindow());
        _spawner.StopSpawn();
    }

    public void Initialize()
    {
        ScoreInit();
        _playerController.PlayerHpController.OnZeroHealth += StopGame;
        _uiService.Show<UIMainMenuWindow>();
        _soundController.Play(SoundName.BackStart, loop:true);
    }
}
