using System.Collections;
using System.Collections.Generic;
using GFA.TPS.Movement;
using UnityEngine;

namespace GFA.TPS.AI
{

    public class BasicAIState : AIState
    {
        public CharacterMovement CharacterMovement { get; set; }

    }

    public class BossAIState : AIState
    {
        public CharacterMovement CharacterMovement { get; set; }
        public Animator Animator { get; set; }
    }
}