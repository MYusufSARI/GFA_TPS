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
        private MatchInstance _matchInstance;

        public override void Begin(AIController controller)
        {
            if (controller.TryGetState<BasicAIState>(out var state))
            {
                state.CharacterMovement = controller.GetComponent<CharacterMovement>();
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

            if (dist < _acceptanceRadius)
            {
                movement.MovementInput = Vector3.zero;
            }

            else
            {
                var dir = (player.transform.position - controller.transform.position).normalized;
                movement.MovementInput = new Vector2(dir.x, dir.z);
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

