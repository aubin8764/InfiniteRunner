using UnityEngine;

namespace Component.StateMachine
{
    public class GameState : State
    {
        private int _life = 3;

        public GameState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnCollision += HandleCollision;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_life);


        }

        public override void Update()
        {
            // noop
        }

        public override void Exit()
        {
            GameEventService.OnCollision -= HandleCollision;
            GameEventService.OnGameState?.Invoke(false); 
        }

        private void HandleCollision()
        {
            _life--;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_life);

            if (_life <= 0)
            {
                StateMachine.ChangeState(new GameOverState(StateMachine));
            }
        }
    }
}
