public class MoveForwardCommand : Command
{
    private BaseShip shipObject;

    public MoveForwardCommand(BaseShip shipObject)
    {
        this.shipObject = shipObject;
    }


    public override void Execute()
    {
        shipObject.MoveVertical(1);
    }
}