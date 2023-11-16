using AxGrid.FSM;

public abstract class BaseFSMState : FSMState
{
    protected virtual void Enter(){}
    protected virtual void Exit(string nameButton){}
}
