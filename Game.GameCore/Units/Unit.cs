﻿using App.GameCore.Battles.System;
using App.GameCore.Tools.Formulas;
using App.GameCore.Units.Actions;
using App.GameCore.Units.Enums;
using App.GameCore.Units.Types;
using System.Drawing;

namespace App.GameCore.Units;

public abstract class Unit
{
    // Metadata
    public Guid Token { get; } = Guid.NewGuid();
    public Team Team { get; set; }

    // Info
    public short Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;

    // Tactics
    public EUnitClass Class { get; protected set; }
    public EUnitClass SubClass { get; protected set; }
    public TUnitResource<float> Initiative { get; protected set; } = new(100); // Event when changed
    public TUnitValue<float> Speed { get; protected set; } = new(5);
    public TUnitValue<float> AttackRange { get; protected set; } = new(30);
    public ETacticalType TacticalType { get; protected set; }
    public TUnitValue<int> TacticalLevel { get; protected set; } = new(0);

    // General
    public TUnitPercentage AbilityHaste { get; protected set; } = new(0);
    public TUnitPercentage Vaparism { get; protected set; } = new(0);
    public TUnitResource<float> Mana { get; protected set; } = new(100);

    // Attack
    public TUnitValue<double> Damage { get; protected set; } = new(0);
    public BattleTimer AttackSpeed { get; protected set; } = new(1000);
    public TUnitChance Accuracy { get; protected set; } = new(0.66f);
    public TUnitChance CriticalChance { get; protected set; } = new(0.05f);
    public TUnitPercentage CriticalDamage { get; protected set; } = new(0.20f);
    public TUnitPercentage ArmorPenetration { get; protected set; } = new(0);
    public TUnitValue<float> IgnoringArmor { get; protected set; } = new(0);

    // Defensive
    public TUnitResource<double> HealthPoints { get; protected set; } = new(1); // Event when changed 
    public TUnitResource<double> Shield { get; protected set; } = new(0); // Event when changed
    public TUnitPercentage ShieldEfficiency { get; protected set; } = new(0f);
    public TUnitValue<float> HealthPassive { get; protected set; } = new(0);
    public TUnitPercentage HealthEfficiency { get; protected set; } = new(0f);
    public TUnitChance Dexterity { get; protected set; } = new(0);
    public TUnitChance CriticalDefeat { get; protected set; } = new(0);
    public TUnitValue<float> Armor { get; protected set; } = new(0);

    // Other
    public float Position { get; protected set; }

    public List<UnitAction> Actions { get; protected set; } = [];

    // Events
    public event EventHandler<AttackEventArgs>? OnAttack;
    public event EventHandler<DamageReceivedEventArgs>? OnDamageReceived;

    public Unit(Team team)
    {
        Team = team;
    }

    public void ApplyEffect(Effect effect)
    {

    }

    public void ReloadEffects()
    {
    }
    // MAKE EVENT, WHEN EFFECT DOWN REFRESH EFFECTS

    public void ApplyItems()
    {

    }
    public void ReloadItems()
    {

    }

    public virtual Unit SelectEnemy(List<Unit> enemys, float attackRange, int seed)
    {
        var targetsPrioritetsClass = UnitClassFormulas.WeightToSelect(Class, SubClass); // Get priorities based on class
        var targetsPrioritetsDistance = GetTargets(GetAttackRadius(attackRange), enemys); // Get enemies in the attack radius and their priority based on distance
        var targets = AddСlassСonsideration(targetsPrioritetsClass, targetsPrioritetsDistance); // Combine class and distance priorities
        return GameFormulas.SelectRandomlyWithPriorities(targets, seed);
    }

    public virtual void ReceiveDamageFromUnit(double damage)
    {
        if (IsShield())
        {
            Shield.Now -= damage;
        }
        else if (IsAlive())
        {
            HealthPoints.Now -= damage;
        }
        else // Dead
            return;
    }

    public bool IsAlive() => HealthPoints.Now > 0;
    public bool IsShield() => Shield.Now > 0;

    protected UnitRadius GetAttackRadius(float attackRange)
    {
        return new UnitRadius()
        {
            Back = Position - attackRange,
            Front = Position + attackRange,
        };
    }
    protected Dictionary<Unit, float> GetTargets(UnitRadius radius, List<Unit> enemys)
    {
        var dictionary = new Dictionary<Unit, float>();

        foreach (var enemy in enemys)
        {
            if (enemy.Position < radius.Front && enemy.Position > radius.Back)
            {
                var distance = Math.Abs(Math.Abs(enemy.Position) - Math.Abs(Position)); 
                var difference = 1 - (distance / radius.Radius);
                dictionary.Add(enemy, difference);
            }
        }

        return dictionary;
    }
    protected Dictionary<Unit, float> AddСlassСonsideration(Dictionary<EUnitClass, float> targetsPrioritetsClass, Dictionary<Unit, float> targets)
    {
        foreach (var target in targets.Keys.ToList())
        {
            EUnitClass unitClass = target.Class; // Припускаємо, що у класу Unit є властивість UnitClass
            if (targetsPrioritetsClass.TryGetValue(unitClass, out float priority))
            {
                targets[target] *= priority;
            }
        }

        return targets;
    }

}

public struct UnitRadius
{
    public float Back {  get; set; }
    public float Front {  get; set; }
    public readonly float Radius => (Front - Back) / 2;
}

public class AttackEventArgs : EventArgs
{
    public double DamageDealt { get; }
    public bool IsCritical { get; }
    public bool IsMiss { get; }

    public AttackEventArgs(double damageDealt, bool isCritical, bool isMiss)
    {
        DamageDealt = damageDealt;
        IsCritical = isCritical;
        IsMiss = isMiss;
    }
}

public class DamageReceivedEventArgs : EventArgs
{
    public double DamageReceived { get; }

    public DamageReceivedEventArgs(double damageReceived)
    {
        DamageReceived = damageReceived;
    }
}
