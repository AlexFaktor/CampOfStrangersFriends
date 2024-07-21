﻿using App.GameCore.Battles.Manager;
using App.GameCore.Tools.Formulas;
using App.GameCore.Units;
using App.GameCore.Units.Actions;

namespace App.GameCore.Content.Units.Abilitys
{
    internal class AbilityAutoAttack : RechargingAbility
    {
        public AbilityAutoAttack(Battle battle, Unit unit) : base(battle, unit)
        {
        }

        public override void Action()
        {
            if (_battle.Timeline < Time.Now)
            {
                _battle.Result.Logs.Text += $"[{_battle.Timeline}] AbilityAutoAttack on reload\n";
            }

            var enemys = _battle.AllUnits.Where(u => u.Team.Token != _unit.Team.Token).ToList();

            var target = _unit.SelectEnemy(enemys);

            target.ReceiveDamageFromUnit(GetTotalPower());

            Time.Reload();
        }

        private double GetTotalPower()
        {
            var damage = _unit.Damage.Now;
            var coefficientInitiative = GameFormulas.GetRatio(_unit.Initiative.Now, _unit.Initiative.Default);
            return damage * coefficientInitiative;
        }
    }
}
