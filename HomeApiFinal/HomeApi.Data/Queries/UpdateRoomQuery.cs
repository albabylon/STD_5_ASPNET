namespace HomeApi.Data.Queries
{
    public class UpdateRoomQuery
    {
        public string NewName { get; }
        public int NewArea { get; }
        public bool NewGasConnected { get; }
        public int NewVoltage { get; }

        public UpdateRoomQuery(string newName, int newArea, bool newGasConnected, int newVoltage)
        {
            NewVoltage = newVoltage;
            NewName = newName;
            NewArea = newArea;
            NewName = newName;
        }
    }
}
