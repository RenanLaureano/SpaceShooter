public class MoveRightCommand : Command
{
    private BaseShip shipObject;

    public MoveRightCommand(BaseShip shipObject)
    {
        this.shipObject = shipObject;
    }


    public override void Execute()
    {
        shipObject.MoveHorizontal(1);
    }
}

