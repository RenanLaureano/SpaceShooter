public class MoveBackCommand : Command
{
    private BaseShip shipObject;

    public MoveBackCommand(BaseShip shipObject)
    {
        this.shipObject = shipObject;
    }


    public override void Execute()
    {
        shipObject.MoveVertical(-1);
    }
}