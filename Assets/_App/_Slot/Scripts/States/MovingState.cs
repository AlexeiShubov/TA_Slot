using AxGrid.FSM;
using AxGrid.Model;

[State(Keys.MovingState)]
public class MovingState : BaseFSMState
{
    [Enter]
    protected override void Enter()
    {
        Model.EventManager.Invoke(Keys.MovingState);
    }

    [Bind(Keys.OnClickSomeButton)]
    protected override void Exit(string nameButton)
    {
        if (nameButton != Keys.StopButton) return;
        
        Model.Set(Keys.OnStartButtonChanged, false);
        Model.Set(Keys.OnStopButtonChanged, false);
        Parent.Change(Keys.StoppingState);
    }
    
    [One(3)]
    private void EnableButton()
    {
        Model.Set(Keys.OnStopButtonChanged, true);
    }
}
