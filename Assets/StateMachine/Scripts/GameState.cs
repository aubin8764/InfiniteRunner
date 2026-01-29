using System;
using Component.Data;
using UnityEngine;

namespace Component.StateMachine
{
    public class GameState : State
    {
        private int _currentLife;

        public GameState(StateMachine stateMachine, SOLevelParameters levelParameters) : base(stateMachine, levelParameters)
        {
        }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnCollision += HandleCollision;
            GameEventService.OnCollectiblePicked += HandleCollectiblePicked;

            _currentLife = LevelParameters.PlayerLife;
        }

        public override void Update()
        {
            // noop
        }

        public override void Exit()
        {
            GameEventService.OnCollision -= HandleCollision;
            GameEventService.OnGameState?.Invoke(false);
            GameEventService.OnCollectiblePicked -= HandleCollectiblePicked;
        }

        private void HandleCollision()
        {
            _currentLife--;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_currentLife);

            if (_currentLife <= 0)
            {
                StateMachine.ChangeState(new GameOverState(StateMachine, LevelParameters));
            }
        }

        private void HandleCollectiblePicked()
        {
            // Cannot exceed maximun life for the level.
            if(_currentLife == LevelParameters.PlayerLife)
            {
                return;
            }

            _currentLife++;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_currentLife);
        }
    }
}
