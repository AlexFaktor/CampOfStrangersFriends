﻿using App.GameCore.Battles.System;
using App.GameCore.Tools.ShellImporters.ConfigReaders;
using App.GameCore.Units;
using App.GameCore.Units.Actions.Abilitys;
using App.GameCore.Units.Actions.Abilitys.Interfaces;

namespace App.GameCore.Battles.Manager;

public class Battle
{
    // Tools
    private UnitFactory _unitFactory;

    // Main
    public BattleConfiguration Configuration { get; }
    public BattleResult BattleResult { get; protected set; }
    public Random Random { get; set; }

    // Battle 
    public ulong Timeline { get; set; }
    public List<Team> AllTeam { get; protected set; } = [];
    public List<Unit> AllUnits { get; protected set; } = [];
    public List<IBattleAction> AllBattleActions { get; protected set; } = [];
    

    public Battle(BattleConfiguration battleConfiguration, CharacterConfigReader characteConfigReader)
    {
        _unitFactory = new(characteConfigReader);

        Configuration = battleConfiguration;
        BattleResult = new(battleConfiguration);
        Random = new Random(battleConfiguration.Seed);


        InitializeConfiguration();

        void InitializeConfiguration()
        {
            Timeline = 0;
            var allTeams = new List<Team>();
            foreach (var teamConfig in battleConfiguration.TeamConfigurations) {
                allTeams.Add(new(teamConfig, this, _unitFactory));
            }
            AllTeam = allTeams;
            AllUnits = allTeams.SelectMany(t => t.Units).ToList();
            AllBattleActions = AllUnits
            .SelectMany(u => u.Actions)
            .OfType<IBattleAction>()
            .ToList();
        }
    }
    

    public async Task<BattleResult> CalculateBattle()
    {
        try
        {
            var result = BattleResult;
            InitializeTeams();

            // Ігровий цикл
            while (true)
            {
                FindAndExecuteAction();
                FindWinner(result);

                if (result.Stats.TeamWinner != Guid.Empty)
                    return result;

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }

        void InitializeTeams()
        {

        }
    }

    private void FindAndExecuteAction()
    {
        if (BattleResult.Stats.TeamWinner != Guid.Empty)
            return;
        if (AllBattleActions.Count <= 0)
            throw new Exception("No BattleAction");

        foreach (var battleAction in AllBattleActions)
        {
            if (battleAction is not IPeriodicAbility)
            {
                battleAction.Action();
                AllBattleActions.Remove(battleAction);
                return;
            }

        }

        var reloadableBattleActions = new List<RechargingAbility>();

        foreach (var battleAction in AllBattleActions)
        {
            reloadableBattleActions.Add((RechargingAbility)battleAction);
        }

        var reloadableBattleAction = reloadableBattleActions.OrderBy(a => a.Time.NextUse).First();
        reloadableBattleAction.Action();
        reloadableBattleAction.Time.Reload();
        Timeline = reloadableBattleAction.Time.NextUse;
    }

    private void FindWinner(BattleResult result)
    {
        var AliveTeams = new List<Team>();

        foreach (var team in AllTeam)
        {
            if (team.IsTeamAilve())
                AliveTeams.Add(team);
        }

        if (AliveTeams.Count == 0)
            throw new Exception("No alive teams");
        else if (AliveTeams.Count == 1)
            result.Stats.TeamWinner = AliveTeams.First().Token;
    }
}
