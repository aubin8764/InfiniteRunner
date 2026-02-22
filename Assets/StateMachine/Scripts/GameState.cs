using Component.Data;
using UnityEngine;

namespace Component.StateMachine
{
    public class GameState : State
    {
        private int _currentLife;
        private int _cristalCount;
        //private float _timeScore;
        private SOLevelParameters _currentLevelParameters;
        private float _currentSpeed;
        private int _cristalPickedToChangeColor;
        private ChunkController[] _chunkPrefab;

        public GameState(StateMachine stateMachine, SOLevelParameters levelParameters) : base(stateMachine, levelParameters)
        {
            _currentLevelParameters = levelParameters;
            _currentSpeed = levelParameters.Speed;
        }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnCristalCountUpdate?.Invoke(_cristalCount);
            GameEventService.OnCollision += HandleCollision;
            GameEventService.OnCristalPicked += HandleCristalPicked;
            GameEventService.OnEnergySpherePicked += HandleEnergySpherePicked;
            _cristalCount = 0;
            //_timeScore = 0;
            _currentLife = LevelParameters.PlayerLife;
            _cristalPickedToChangeColor = LevelParameters.CristalPickedToChangeColor;
        }

        public override void Update()
        {
            /*_timeScore += Time.deltaTime;
            GameEventService.OnTimeScoreUpdated?.Invoke(_timeScore);*/
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
            //Debug.Log("Le joueur entre en collision avec le cristal");
            _cristalCount++;
            GameEventService.OnCristalCountUpdate?.Invoke(_cristalCount);

            //Debug.Log($"Cristaux collectés : {_cristalCount}/{_cristalPickedToChangeColor}");

            if (_cristalCount >= _cristalPickedToChangeColor)
            {
                //Debug.Log("Changement de couleur !");
                ChangeColor();
                _cristalCount = 0;
            }
        }

        public void HandleEnergySpherePicked()
        {
            // Cannot exceed maximun life for the level.
            if (_currentLife == LevelParameters.PlayerLife)
            {
                return;
            }

            // Ajoute une vie au personnage
            _currentLife++;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_currentLife);
        }

        
        private void ChangeColor()
        {
            if (_cristalCount < _cristalPickedToChangeColor)
            {
                return;
            }

            if (_cristalCount == _cristalPickedToChangeColor)
            {
                // Récupérer le matériau du cristal
                Material cristalMaterial = LevelParameters.CristalMaterials;

                // Envoyer l'événement pour changer la couleur de tous les chunks
                GameEventService.OnChunkMaterialChanged?.Invoke(cristalMaterial);
            }
        }

        // private void (augmentation de la vitesse au fur et a mesur du temps et ajouté une limite maximun)
    }
}