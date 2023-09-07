using UnityEngine;

namespace GFA.TPS.AI
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private AIBehaviour _aiBehaviour;
        public AIBehaviour AIBehaviour
        {
            get => _aiBehaviour;
            set
            {
                if (_aiBehaviour)
                {
                    _aiBehaviour.End(this);
                }

                _aiBehaviour = Instantiate(value);

                if (_aiBehaviour)
                {
                    _aiState = _aiBehaviour.CreateState();
                    _aiBehaviour.Begin(this);
                }
            }
        }

        private AIState _aiState;

        private void Awake()
        {
            if (_aiBehaviour)
            {
                _aiBehaviour = Instantiate(_aiBehaviour);
                _aiBehaviour.Begin(this);
            }
        }

        private void Update()
        {
            if (AIBehaviour)
            {
                AIBehaviour.OnUpdate(this);
            }
        }

        public bool TryGetState<T>(out T state) where T : AIState
        {

            if (_aiState is T casted)
            {
                state = casted;
                return true;
            }
            else
            {
                state = null;
                return false;
            }
        }
    }
}