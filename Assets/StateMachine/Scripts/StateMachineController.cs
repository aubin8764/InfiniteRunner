using Component.SODB;
using UnityEngine;

namespace Component.StateMachine
{
    public class StateMachineController : MonoBehaviour
    {
        private StateMachine _stateMachine;

        private void Start()
        {
            var parameters = ScriptableObjectDataBase.GetByName("Level1");

            _stateMachine = new StateMachine();
            var initialState = new CountdownState(_stateMachine, parameters);

            _stateMachine.ChangeState(initialState);
        }

        public void Update() => _stateMachine.Update();
    }
}