public class MoveLeftCommand : Command
{
    private BaseShip shipObject;

    public MoveLeftCommand(BaseShip shipObject)
    {
        this.shipObject = shipObject;
    }


    public override void Execute()
    {
        shipObject.MoveHorizontal(-1);
    }
}
