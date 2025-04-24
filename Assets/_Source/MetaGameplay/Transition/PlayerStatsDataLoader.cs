using Include;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.MetaGameplay.Transition
{
    public class PlayerStatsDataLoader
    {
        private PlayerStatsConfig _config;
        private ISaveLoadService _saveLoadService;

        public PlayerStatsDataLoader(PlayerStatsConfig config, ISaveLoadService saveLoadService)
        {
            _config = config;
            _saveLoadService = saveLoadService;
        }
        
        public void SavePlayerStats(PlayerStats playerStats)
        {
            _saveLoadService.Save("PlayerData", playerStats);
        }
        
        public PlayerStats LoadPlayerStats()
        {
            var saveData = _saveLoadService.Load<PlayerStats>("PlayerData");
            
            if (saveData == null)
                return _config.GetPlayerStats();
            
            return saveData;
        }
    }
}