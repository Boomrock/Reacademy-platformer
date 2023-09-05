using Player;

namespace Academy_Platformer.Player.FactoryPlayer
{
    public interface IFactoryCharacter
    {
        PlayerView Create(PlayerModel playerModel, PlayerView playerView);
    }
}