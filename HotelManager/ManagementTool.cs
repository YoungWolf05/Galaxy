using System;
using System.Collections.Generic;

namespace HotelManager
{
    public static class ManagementTool
    {
        private static HotelRoom[] RoomList = {
            new HotelRoom() { Name = "1A" ,DistanceScore = 0},
            new HotelRoom() { Name = "1B" ,DistanceScore = 100},
            new HotelRoom() { Name = "1C" ,DistanceScore = 200},
            new HotelRoom() { Name = "1D" ,DistanceScore = 300},
            new HotelRoom() { Name = "1E" ,DistanceScore = 400},
            new HotelRoom() { Name = "2E" ,DistanceScore = 500},
            new HotelRoom() { Name = "2D" ,DistanceScore = 600},
            new HotelRoom() { Name = "2C" ,DistanceScore = 700},
            new HotelRoom() { Name = "2B" ,DistanceScore = 800},
            new HotelRoom() { Name = "2A" ,DistanceScore = 900},
            new HotelRoom() { Name = "3A" ,DistanceScore = 1000},
            new HotelRoom() { Name = "3B" ,DistanceScore = 1100},
            new HotelRoom() { Name = "3C" ,DistanceScore = 1200},
            new HotelRoom() { Name = "3D" ,DistanceScore = 1300},
            new HotelRoom() { Name = "3E" ,DistanceScore = 1400},
            new HotelRoom() { Name = "4E" ,DistanceScore = 1500},
            new HotelRoom() { Name = "4D" ,DistanceScore = 1600},
            new HotelRoom() { Name = "4C" ,DistanceScore = 1700},
            new HotelRoom() { Name = "4B" ,DistanceScore = 1800},
            new HotelRoom() { Name = "4A" ,DistanceScore = 1900}
        };
        public static LinkedList<HotelRoom> AvailableRooms = new LinkedList<HotelRoom>(RoomList);
        public static Dictionary<string, HotelRoom> OccupiedRooms = new Dictionary<string, HotelRoom>();
        public static Dictionary<string, HotelRoom> VacantRooms = new Dictionary<string, HotelRoom>();
        public static Dictionary<string, HotelRoom> RepairingRooms = new Dictionary<string, HotelRoom>();
        public static bool CheckInHotel()
        {
            if (AvailableRooms.Count == 0)
                return false;
            if (AvailableRooms.First.Value.DistanceScore < AvailableRooms.Last.Value.DistanceScore)
            {
                OccupiedRooms.Add(AvailableRooms.First.Value.Name, AvailableRooms.First.Value);
                Console.WriteLine($"{AvailableRooms.First.Value.Name} assigned!");
                AvailableRooms.RemoveFirst();
            }
            else
            {
                OccupiedRooms.Add(AvailableRooms.Last.Value.Name, AvailableRooms.Last.Value);
                Console.WriteLine($"{AvailableRooms.Last.Value.Name} assigned!");
                AvailableRooms.RemoveLast();
            }
            return true;
        }
        public static bool CheckOutHotel(string roomName)
        {
            if (!OccupiedRooms.ContainsKey(roomName))
                return false;
            VacantRooms.Add(roomName, OccupiedRooms[roomName]);
            OccupiedRooms.Remove(roomName);
            return true;
        }
        
        public static bool CleanRoom(string roomName)
        {
            if (!VacantRooms.ContainsKey(roomName))
                return false;
            if (AvailableRooms.Count == 0)
                AvailableRooms.AddFirst(VacantRooms[roomName]);
            else if (VacantRooms[roomName].DistanceScore < AvailableRooms.First.Value.DistanceScore)
                AvailableRooms.AddFirst(VacantRooms[roomName]);
            else
                AvailableRooms.AddLast(VacantRooms[roomName]);

            VacantRooms.Remove(roomName);
            return true;
        }

        public static bool RepairRoom(string roomName)
        {
            if (!VacantRooms.ContainsKey(roomName))
                return false;
            RepairingRooms.Add(roomName, VacantRooms[roomName]);
            VacantRooms.Remove(roomName);
            return true;
        }

        public static bool RepairDone(string roomName)
        {
            if (!RepairingRooms.ContainsKey(roomName))
                return false;
            VacantRooms.Add(roomName, RepairingRooms[roomName]);
            RepairingRooms.Remove(roomName);
            return true;
        }
        public static void RemakeHotel()
        {
            AvailableRooms = new LinkedList<HotelRoom>(RoomList); ;
            OccupiedRooms.Clear();
            VacantRooms.Clear();
            RepairingRooms.Clear();
        }
        public static void PrintStatus()
        {
            Console.WriteLine("---------------------------------------");
            Console.Write("Available: ");
            foreach (HotelRoom room in AvailableRooms)
            {
                Console.Write(room.Name + " ");
            }
            Console.WriteLine();
            Console.Write("Occupied : ");
            foreach (var it in OccupiedRooms)
            {
                Console.Write(it.Value.Name + " ");
            }
            Console.WriteLine();
            Console.Write("Vacant   : ");
            foreach (var it in VacantRooms)
            {
                Console.Write(it.Value.Name + " ");
            }
            Console.WriteLine();
            Console.Write("Reparing : ");
            foreach (var it in RepairingRooms)
            {
                Console.Write(it.Value.Name + " ");
            }
            Console.WriteLine();
            Console.WriteLine("---------------------------------------");
        }
        public static void PrintAvailableRooms()
        {
            foreach (HotelRoom word in AvailableRooms)
            {
                Console.Write(word.Name + " ");
            }
            Console.WriteLine();
        }
    }

}
