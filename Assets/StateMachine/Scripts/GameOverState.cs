using Component.Data;
using UnityEngine;

namespace Component.StateMachine
{
    public class GameOverState : State
    {
        public GameOverState(StateMachine stateMachine, SOLevelParameters levelParameters) : base(stateMachine, levelParameters)
        {
        }

        public override void Enter()
        {
            GameEventService.OnGameOverState?.Invoke(true);
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            GameEventService.OnGameOverState?.Invoke(false);

        }
    }
}