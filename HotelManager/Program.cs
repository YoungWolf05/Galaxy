using System;
using System.Collections.Generic;

namespace HotelManager
{
    public class HotelRoom
    {
        public string Name;
        public int DistanceScore;
    }
    internal class Program
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
        private static LinkedList<HotelRoom> AvailableRooms = new LinkedList<HotelRoom>(RoomList);
        private static Dictionary<string, HotelRoom> OccupiedRooms = new Dictionary<string, HotelRoom>();
        private static Dictionary<string, HotelRoom> VacantRooms = new Dictionary<string, HotelRoom>();
        private static Dictionary<string, HotelRoom> RepairingRooms = new Dictionary<string, HotelRoom>();

        static void Main(string[] args)
        {
            string input = string.Empty;
            Console.WriteLine("Welcome to the Hotel");
            while (input != "exit")
            {
                Console.WriteLine("0 => available, 1 => checkin, 2 => checkout, 3 => cleanroom, 4 => repairroom, 5 => repairdone, 6 => exit");
                input = Console.ReadLine();
                if (input == "0")
                {
                    PrintAvailableRooms();
                }
                else if (input == "1")
                {
                    if (!CheckInHotel())
                        Console.WriteLine("No available room for check in");
                    PrintStatus();
                }
                else if (input == "2")
                {
                    while(OccupiedRooms.Count > 0)
                    {
                        Console.WriteLine("Name of room to be checkout? else x");
                        string room = Console.ReadLine();
                        if (room == "x")
                            break;
                        if (CheckOutHotel(room))
                        {
                            Console.WriteLine($"Room {room} Checked out");
                        }   
                        else
                            Console.WriteLine("Invalid room name. Please try again");
                        PrintStatus();
                    }
                }
                else if (input == "3")
                {
                    while (VacantRooms.Count > 0)
                    {
                        Console.WriteLine("Name of room to be cleaned? else x");
                        string room = Console.ReadLine();
                        if (room == "x")
                            break;
                        if (CleanRoom(room))
                        {
                            Console.WriteLine($"Room {room} Cleaned");
                        }
                        else
                            Console.WriteLine("Invalid room name. Please try again");
                        PrintStatus();
                    }
                }
                else if (input == "4")
                {
                    while (VacantRooms.Count > 0)
                    {
                        Console.WriteLine("Name of room to be repaired? else x");
                        string room = Console.ReadLine();
                        if (room == "x")
                            break;
                        if (RepairRoom(room))
                        {
                            Console.WriteLine($"Room {room} repairing");
                        }
                        else
                            Console.WriteLine("Invalid room name. Please try again");
                        PrintStatus();
                    }
                }
                else if (input == "5")
                {
                    while (RepairingRooms.Count > 0)
                    {
                        Console.WriteLine("Name of room done reparing? else x");
                        string room = Console.ReadLine();
                        if (room == "x")
                            break;
                        if (RepairDone(room))
                        {
                            Console.WriteLine($"Room {room} done repairing");
                        }
                        else
                            Console.WriteLine("Invalid room name. Please try again");
                        PrintStatus();
                    }                    
                }
            }
        }
        private static bool CheckInHotel()
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
        private static bool CheckOutHotel(string roomName)
        {
            if (!OccupiedRooms.ContainsKey(roomName))
                return false;
            VacantRooms.Add(roomName, OccupiedRooms[roomName]);
            OccupiedRooms.Remove(roomName);
            return true;
        }

        private static bool CleanRoom(string roomName)
        {
            if (!VacantRooms.ContainsKey(roomName))
                return false;
            if (AvailableRooms.Count == 0 )
                AvailableRooms.AddFirst(VacantRooms[roomName]);
            else if (VacantRooms[roomName].DistanceScore < AvailableRooms.First.Value.DistanceScore)
                AvailableRooms.AddFirst(VacantRooms[roomName]);
            else
                AvailableRooms.AddLast(VacantRooms[roomName]);

            VacantRooms.Remove(roomName);
            return true;
        }

        private static bool RepairRoom(string roomName)
        {
            if (!VacantRooms.ContainsKey(roomName))
                return false;
            RepairingRooms.Add(roomName, VacantRooms[roomName]);
            VacantRooms.Remove(roomName);
            return true;
        }

        private static bool RepairDone(string roomName)
        {
            if (!RepairingRooms.ContainsKey(roomName))
                return false;
            VacantRooms.Add(roomName, RepairingRooms[roomName]);
            RepairingRooms.Remove(roomName);
            return true;
        }
        private static void PrintStatus()
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
        private static void PrintAvailableRooms()
        {
            foreach (HotelRoom word in AvailableRooms)
            {
                Console.Write(word.Name + " ");
            }
            Console.WriteLine();
        }

    }
}
