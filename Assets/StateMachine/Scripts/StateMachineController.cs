using Component.Data;
using Components.SODataBase;
using UnityEngine;

namespace Component.StateMachine
{
    public class StateMachineController : MonoBehaviour
    {
        private StateMachine _stateMachine;

        private void Start()
        {
            int levelIndex = 1;
            if (SaveService.TryLoad(out SaveData saveData))
            {
                levelIndex = saveData.LevelIndex;
            }

            SOLevelParameters parameters;

            parameters = ScriptableObjectDataBase.Get<SOLevelParameters>("Level" + levelIndex);


            _stateMachine = new StateMachine();
            var initialState = new CountdownState(_stateMachine, parameters);

            _stateMachine.ChangeState(initialState);
        }

            public void Update() => _stateMachine.Update();
        }
    }