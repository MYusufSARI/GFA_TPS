using DG.Tweening;
using GFA.TPS.MatchSystem;
using GFA.TPS.Movement;
using UnityEngine;

namespace GFA.TPS.AI.Behaviours
{
    [CreateAssetMenu(menuName = "AI/Basic AI Behaviour")]
    public class BasicAIBehaviour : AIBehaviour
    {
        [SerializeField]
        private float _acceptanceRadius;

        [SerializeField]
        private MatchInstance? _matchInstance;

        public override void Begin(AIController controller)
        {
            if (controller.TryGetState<BasicAIState>(out var state))
            {
                state.CharacterMovement = controller.GetComponent<CharacterMovement>();
                state.Attacker = controller.GetComponent<EnemyAttacker>();
                state.PlayerDamagable = _matchInstance.Player.GetComponent<IDamagable>();
            }
        }

        protected override void Execute(AIController controller)
        {

            if (!controller.TryGetState<BasicAIState>(out var state))
            {
                return;
            }
            var player = _matchInstance.Player;

            var movement = state.CharacterMovement;

            var dist = Vector3.Distance(player.transform.position, controller.transform.position);

            var dir = (player.transform.position - controller.transform.position).normalized;
            dir.y = 0;

            if (dist < _acceptanceRadius || !state.Attacker.IsCurrentlyAttacking)
            {
                

                movement.MovementInput = new Vector2(dir.x, dir.z);
                
            }
            var rotation =  Quaternion.LookRotation(dir).normalized;
            movement.Rotation = rotation.eulerAngles.y; 

            if (dist < state.Attacker.Range)
            {
                state.Attacker.Attack(state.PlayerDamagable);
            }
        }

        public override void End(AIController controller)
        {

        }

        public override AIState CreateState()
        {
            return new BasicAIState();
        }
    }
}

