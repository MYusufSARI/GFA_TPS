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
        private CharacterMovement _movement;

        public override void Begin(AIController controller)
        {
            _movement = controller.GetComponent<CharacterMovement>();

        }

        protected override void Execute(AIController controller)
        {
            var player = _matchInstance.Player;


            var dist = Vector3.Distance(player.transform.position, controller.transform.position);

            if (dist<_acceptanceRadius)
            {
                _movement.MovementInput = Vector3.zero;
            }

            else 
            {
                var dir = (player.transform.position - controller.transform.position).normalized;
                _movement.MovementInput = new Vector2(dir.x, dir.z);
            }
        }

        public override void End(AIController controller)
        {
        }
    }
}

