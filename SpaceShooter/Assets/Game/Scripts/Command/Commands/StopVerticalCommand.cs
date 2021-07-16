public class StopVerticalCommand : Command
{
    private BaseShip shipObject;

    public StopVerticalCommand(BaseShip shipObject)
    {
        this.shipObject = shipObject;
    }


    public override void Execute()
    {
        shipObject.MoveVertical(0);
    }
}

