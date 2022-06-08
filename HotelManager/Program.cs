using System;
namespace HotelManager
{
   
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Console.WriteLine("Welcome to the Hotel");
            while (input != "6")
            {
                Console.WriteLine("0 => available, 1 => checkin, 2 => checkout, 3 => cleanroom, 4 => repairroom, 5 => repairdone, 6 => exit");
                input = Console.ReadLine();
                if (input == "0")
                {
                    ManagementTool.PrintAvailableRooms();
                }
                else if (input == "1")
                {
                    if (!ManagementTool.CheckInHotel())
                        Console.WriteLine("No available room for check in");
                    ManagementTool.PrintStatus();
                }
                else if (input == "2")
                {
                    while(ManagementTool.OccupiedRooms.Count > 0)
                    {
                        Console.WriteLine("Name of room to be checkout? else x");
                        string room = Console.ReadLine();
                        if (room == "x")
                            break;
                        if (ManagementTool.CheckOutHotel(room))
                        {
                            Console.WriteLine($"Room {room} Checked out");
                        }   
                        else
                            Console.WriteLine("Invalid room name. Please try again");
                        ManagementTool.PrintStatus();
                    }
                }
                else if (input == "3")
                {
                    while (ManagementTool.VacantRooms.Count > 0)
                    {
                        Console.WriteLine("Name of room to be cleaned? else x");
                        string room = Console.ReadLine();
                        if (room == "x")
                            break;
                        if (ManagementTool.CleanRoom(room))
                        {
                            Console.WriteLine($"Room {room} Cleaned");
                        }
                        else
                            Console.WriteLine("Invalid room name. Please try again");
                        ManagementTool.PrintStatus();
                    }
                }
                else if (input == "4")
                {
                    while (ManagementTool.VacantRooms.Count > 0)
                    {
                        Console.WriteLine("Name of room to be repaired? else x");
                        string room = Console.ReadLine();
                        if (room == "x")
                            break;
                        if (ManagementTool.RepairRoom(room))
                        {
                            Console.WriteLine($"Room {room} repairing");
                        }
                        else
                            Console.WriteLine("Invalid room name. Please try again");
                        ManagementTool.PrintStatus();
                    }
                }
                else if (input == "5")
                {
                    while (ManagementTool.RepairingRooms.Count > 0)
                    {
                        Console.WriteLine("Name of room done reparing? else x");
                        string room = Console.ReadLine();
                        if (room == "x")
                            break;
                        if (ManagementTool.RepairDone(room))
                        {
                            Console.WriteLine($"Room {room} done repairing");
                        }
                        else
                            Console.WriteLine("Invalid room name. Please try again");
                        ManagementTool.PrintStatus();
                    }                    
                }
            }
        }
        

    }
}
