public class StopHorizontalCommand : Command
{
    private BaseShip shipObject;

    public StopHorizontalCommand(BaseShip shipObject)
    {
        this.shipObject = shipObject;
    }

    public override void Execute()
    {
        shipObject.MoveHorizontal(0);
    }
}