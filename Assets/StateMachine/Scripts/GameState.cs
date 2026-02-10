using Component.Data;
using Components.SODataBase;
using UnityEngine;

namespace Component.StateMachine
{
    public class GameState : State
    {
        private int _currentLife;
        private int _cristalCount;
        private float _timeScore;
        private SOLevelParameters _currentLevelParameters;

        public GameState(StateMachine stateMachine, SOLevelParameters levelParameters) : base(stateMachine, levelParameters)
        {
        }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnCristalCountUpdate?.Invoke(_cristalCount);
            GameEventService.OnCollision += HandleCollision;
            GameEventService.OnCristalPicked += HandleCristalPicked;
            GameEventService.OnEnergySpherePicked += HandleEnergySpherePicked;
            _cristalCount = 0;
            _timeScore = 0;
            _currentLife = LevelParameters.PlayerLife;
        }

        public override void Update()
        {
            // Noop
        }

        public override void Exit()
        {
            GameEventService.OnCollision -= HandleCollision;
            GameEventService.OnGameState?.Invoke(false);
            GameEventService.OnCristalPicked -= HandleCristalPicked;
            GameEventService.OnEnergySpherePicked -= HandleEnergySpherePicked;
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

        private void HandleCristalPicked()
        {
            _cristalCount++;
            GameEventService.OnCristalCountUpdate?.Invoke(_cristalCount);

            if(_currentLevelParameters.CristalPickedToLevelUp > 0)
            {
                if(_cristalCount >= _currentLevelParameters.CristalPickedToLevelUp)
                {
                    LevelUp();
                }
            }
        }

        public void HandleEnergySpherePicked()
        {
            // Cannot exceed maximun life for the level.
            if(_currentLife == LevelParameters.PlayerLife)
            {
                return;
            }

            _currentLife++;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_currentLife);
        }

        private void LevelUp()
        {

            if (!SaveService.TryLoad(out SaveData saveData))
            {
                saveData = new SaveData();
                saveData.LevelIndex = 1;
            }

            int nextLevel = saveData.LevelIndex + 1;

            SOLevelParameters LevelParameters = ScriptableObjectDataBase.Get<SOLevelParameters>("Level" + saveData.LevelIndex);

            if (LevelParameters != null)
            {
                saveData.LevelIndex = nextLevel;
                SaveService.Save(saveData);

                _currentLevelParameters = LevelParameters;
                _cristalCount = 0;

                GameEventService.OnLevelParametersUpdated?.Invoke(LevelParameters);
                GameEventService.OnCristalCountUpdate?.Invoke(_cristalCount);
                GameEventService.OnChunkColorUpdated?.Invoke(LevelParameters.GetRandomChunkMaterial());
                GameEventService.OnCristalColorUpdated?.Invoke(LevelParameters.GetRandomCristalMaterial());
            }
        }
    }
}
