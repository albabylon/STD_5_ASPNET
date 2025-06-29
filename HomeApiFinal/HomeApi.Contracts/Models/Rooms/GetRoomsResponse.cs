﻿namespace HomeApi.Contracts.Models.Rooms
{
    public class GetRoomsResponse
    {
        public int RoomAmount { get; set; }
        public RoomView [] Rooms { get; set; }
    }

    public class RoomView
    {
        public string Name { get; set; }
        public int Area { get; set; }
        public bool GasConnected { get; set; }
        public int Voltage { get; set; }
    }
}