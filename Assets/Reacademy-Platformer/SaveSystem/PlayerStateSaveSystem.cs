using UnityEngine;

namespace SaveSystem
{
   public class PlayerStateSaveSystem : ISaveSystem<PlayerState>
   {
       private const string PlayerStateKey = "PlayerStateKey";
   
       public void Save(PlayerState date)
       {
           Save(key: PlayerStateKey, date: date);
       }
   
       public PlayerState Load()
       {
           return Load(key: PlayerStateKey);
       }
   
       public void Clear()
       {
           Clear(key: PlayerStateKey);
       }
   
       private void Save(string key, PlayerState date)
       {
           var json = JsonUtility.ToJson(date);
           PlayerPrefs.SetString(key: key, value: json);
       }
   
       private PlayerState Load(string key)
       {
           if (PlayerPrefs.HasKey(key))
           {
               return default;
           }
   
           var json = PlayerPrefs.GetString(key);
           var playerState = JsonUtility.FromJson<PlayerState>(json);
           return playerState;
       }
   
       private void Clear(string key)
       {
           if (PlayerPrefs.HasKey(key))
           {
               PlayerPrefs.DeleteKey(key);
           }
       }
   }
   
   public struct PlayerState
   {
       public int Score;
       public int LifeState;
       public PlayerState(int score, int lifeState)
       {
           Score = score;
           LifeState = lifeState;
       }
   } 
}