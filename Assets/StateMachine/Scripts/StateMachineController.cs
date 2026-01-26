using Component.Data;
using UnityEngine;

namespace Component.StateMachine
{
    public class StateMachineController : MonoBehaviour
    {
        [SerializeField] private SOLevelParameters _levelParameters;
        private StateMachine _stateMachine;

        private void Start()
        {
            _stateMachine = new StateMachine();
            var initialState = new CountdownState(_stateMachine, _levelParameters);

            _stateMachine.ChangeState(initialState);
        }

        public void Update() => _stateMachine.Update();
    }
}