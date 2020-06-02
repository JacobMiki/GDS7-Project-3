using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class CombatEncounter : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator _stateMachine;

        [SerializeField] private string _monsterDamagePropName = "MonsterDamage";
        [SerializeField] private string _playerDamagePropName = "PlayerDamage";


        public void DamageMonster(int dmg)
        {
            _stateMachine.SetInteger(_monsterDamagePropName, _stateMachine.GetInteger(_monsterDamagePropName) + dmg);
        }

        public void DamagePlayer(int dmg)
        {
            _stateMachine.SetInteger(_playerDamagePropName, _stateMachine.GetInteger(_playerDamagePropName) + dmg);
        }

        public void HitMonster()
        {
            _stateMachine.SetTrigger("MonsterHit");
            DamageMonster(1);
        }

        public void ResetMonsterDamage()
        {
            _stateMachine.SetInteger(_monsterDamagePropName, 0);
        }

        public void Interact()
        {
            HitMonster();
        }

        public void Win()
        {
            Destroy(gameObject);
        }
    }
}