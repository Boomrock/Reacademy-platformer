using Player;
using Sounds;
using UI.HUD;
using UI.UIService;
using UI.UIWindows;
using UnityEngine;
using Zenject;

public class GameController : IInitializable
{
    private FallObjectSpawner _spawner;
    private PlayerController _playerController;
    private UIService _uiService;
    private UIMainMenuController _mainMenuWindowController;
    private UIGameWindowController _gameWindowController;
    private UIEndGameWindowController _endMenuWindowController;
    private ScoreCounter _scoreCounter;
    private SoundController _soundController;

    public GameController(PlayerController playerController,
        SoundController soundController,
        FallObjectSpawner fallObjectSpawner,
        UIService uiService,
        ScoreCounter scoreCounter
    )
    {
        _soundController = soundController;
        _playerController = playerController;
        _spawner = fallObjectSpawner;
        _uiService = uiService;
        _scoreCounter = scoreCounter;
        _playerController.PlayerHpController.OnZeroHealth += StopGame;
    }

    [Inject]
    void InjectControllers(UIMainMenuController uiMainMenuController,
        UIGameWindowController uiGameWindowController,
        UIEndGameWindowController uiEndGameWindowController,
        HUDWindowController hudWindowController)
    {
        _uiService.Add<UIMainMenuWindow>(uiMainMenuController);
        _uiService.Add<UIGameWindow>(uiGameWindowController);
        _uiService.Add<UIEndGameWindow>(uiEndGameWindowController);
        _uiService.Add<HUDWindow>(hudWindowController);
    }
    private void ScoreInit()
    {
        var hudWindowController = (HUDWindowController)_uiService.GetController<HUDWindow>();
        _scoreCounter.ScoreChangeNotify += hudWindowController.ChangeScore;
    }

    public void StartGame()
    {
        _soundController.Stop();
        _soundController.Play(SoundName.BackMain, loop: true);

        _playerController.Spawn();
        _spawner.StartSpawn();
    }

    public void StopGame()
    {
        _playerController.DestroyView(() => _uiService.ShowOnly<UIEndGameWindow>());
        _spawner.StopSpawn();
    }

    public void Initialize()
    {
        _uiService.Show<UIMainMenuWindow>();
        ScoreInit();
        _soundController.Play(SoundName.BackStart, loop: true);
    }
}
