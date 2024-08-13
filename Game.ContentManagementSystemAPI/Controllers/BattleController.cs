﻿using App.GameCore.Battles.Manager;
using App.GameCore.Tools.ShellImporters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace App.ContentManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleController : ControllerBase
    {
        private IServiceProvider _serviceProvider;

        private DownloaderGameConfigService _downloader;

        public BattleController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _downloader = serviceProvider.GetRequiredService<DownloaderGameConfigService>();
        }

        [HttpPost("pvp")]
        public async Task<IActionResult> MakePvP([FromQuery] string jsonBattleConfiguration)
        {
            var config = JsonConvert.DeserializeObject<BattleConfiguration>(jsonBattleConfiguration);
            if (config == null)
            {
                Log.Error($"Cannot create config with this JSON:\n{jsonBattleConfiguration}\n");
                return NotFound();
            }

            var battle = new Battle(config!, _downloader.characterConfigs);

            return Ok(battle.CalculateBattle());
        }
        
        [HttpPost("pve")]
        public async Task<IActionResult> MakePvE([FromQuery] string jsonBattleConfiguration)
        {
            var config = JsonConvert.DeserializeObject<BattleConfiguration>(jsonBattleConfiguration);
            if (config == null)
            {
                Log.Error($"Cannot create config with this JSON:\n{jsonBattleConfiguration}\n");
                return NotFound();
            }

            var battle = new Battle(config!, _downloader.characterConfigs);

            return Ok(battle.CalculateBattle());
        }

        [HttpPost("admin/custom")]
        public async Task<IActionResult> Get([FromQuery] string jsonBattleConfiguration)
        {
            var config = JsonConvert.DeserializeObject<BattleConfiguration>(jsonBattleConfiguration);
            if (config == null)
            {
                Log.Error($"Cannot create config with this JSON:\n{jsonBattleConfiguration}\n");
                return NotFound();
            }
            Log.Information("Custom battle started.");
            var battleResult = new Battle(config!, _downloader.characterConfigs).CalculateBattle();
            Log.Information($"Custom battle successfully completed\nDuration: {battleResult.Stats.ActualDuration}\nID: {battleResult.Id}");

            return Ok(battleResult);
        }
    }
}
