using AxGrid.FSM;
using AxGrid.Model;

[State(Keys.IdleState)]
public class IdleState : BaseFSMState
{
    [Enter]
    protected override void Enter()
    {
        Model.EventManager.Invoke(Keys.IdleState);
        Model.Set(Keys.OnStartButtonChanged, true);
    }
    
    [Bind(Keys.OnClickSomeButton)]
    protected override void Exit(string nameButton)
    {
        if (nameButton != Keys.StartButton) return;

        Model.Set(Keys.OnStartButtonChanged, false);
        Parent.Change(Keys.MovingState);
    }
}
