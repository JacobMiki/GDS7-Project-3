using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class CombatEncounter : MonoBehaviour
    {
        [SerializeField] private Animator _stateMachine;

        [SerializeField] private string _monsterDamagePropName = "MonsterDamage";
        [SerializeField] private string _playerDamagePropName = "PlayerDamage";
        [SerializeField] private string _monsterHittablePropName = "MonsterHittable";
        [SerializeField] private string _monsterHitPropName = "MonsterHit";
        [SerializeField] private string _combatRepeatsPropName = "CombatRepeats";


        public void DamageMonster(int dmg)
        {
            _stateMachine.SetInteger(_monsterDamagePropName, _stateMachine.GetInteger(_monsterDamagePropName) + dmg);
        }

        public void DamagePlayer(int dmg)
        {
            _stateMachine.SetInteger(_playerDamagePropName, _stateMachine.GetInteger(_playerDamagePropName) + dmg);
        }

        public void ToggleMonsterHittable()
        {
            _stateMachine.SetBool(_monsterHittablePropName, !_stateMachine.GetBool(_monsterHittablePropName));
        }
        public void HitMonster()
        {
            if (_stateMachine.GetBool(_monsterHittablePropName))
            {
                _stateMachine.SetTrigger(_monsterHitPropName);
            }
        }

        public void AdvanceCombat()
        {
            _stateMachine.SetInteger(_combatRepeatsPropName, _stateMachine.GetInteger(_combatRepeatsPropName) + 1);
        }
        public void ResetCombat()
        {
            _stateMachine.SetInteger(_combatRepeatsPropName, 0);
        }

        public void ResetMonsterDamage()
        {
            _stateMachine.SetInteger(_monsterDamagePropName, 0);
        }

    }
}