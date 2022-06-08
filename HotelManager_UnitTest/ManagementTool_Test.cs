using static HotelManager.ManagementTool;
using NUnit.Framework;

namespace HotelManager_UnitTest
{
    public class ManagementTool_Test
    {
        [SetUp]
        public void Setup()
        {
            RemakeHotel();
        }

        [TestCase(
            "1A",
            TestName = "Check In Test",
            Description = "Upon check in, romm status changes from available to occupied")]
        public void CheckIn_Test(string roomName)
        {
            Assert.AreEqual(AvailableRooms.Count, 20);
            Assert.IsTrue(CheckInHotel());
            Assert.AreEqual(AvailableRooms.Count, 19);
            Assert.That(OccupiedRooms, Contains.Key(roomName));
        }

        [TestCase(
            "1A",
            TestName = "Check Out Test",
            Description = 
            "Room has to be checked in before checking out. " +
            "Upon checking out, romm status changes from occupied to vacant")]
        public void CheckOut_Test(string roomName)
        {
            Assert.IsTrue(CheckInHotel());
            Assert.IsTrue(CheckOutHotel(roomName));
            Assert.That(VacantRooms, Contains.Key(roomName));
        }

        [TestCase(
           "1A",
           TestName = "Clean Room Test",
           Description =
           "To make the room available, vacant room has to be cleaned")]
        public void CleanRoom_Test(string roomName)
        {
            Assert.IsTrue(CheckInHotel());
            Assert.IsTrue(CheckOutHotel(roomName));
            Assert.AreEqual(AvailableRooms.Count, 19);
            Assert.AreEqual(VacantRooms.Count, 1);
            Assert.IsTrue(CleanRoom(roomName));
            Assert.AreEqual(AvailableRooms.Count, 20);
            Assert.AreEqual(VacantRooms.Count, 0);
        }

        [TestCase(
           "1A",
           TestName = "Repair Room Test",
           Description =
           "Only vacant room can be repaired")]
        public void RepairRoom_Test(string roomName)
        {
            Assert.IsTrue(CheckInHotel());
            Assert.IsTrue(CheckOutHotel(roomName));
            Assert.AreEqual(VacantRooms.Count, 1);
            Assert.AreEqual(RepairingRooms.Count, 0);
            Assert.IsTrue(RepairRoom(roomName));
            Assert.AreEqual(VacantRooms.Count, 0);
            Assert.AreEqual(RepairingRooms.Count, 1);
        }

        [TestCase(
           "1A",
           TestName = "Repair Done Test",
           Description =
           "Done repairing the room. The room will become a vacant room")]
        public void RepairDone_Test(string roomName)
        {
            Assert.IsTrue(CheckInHotel());
            Assert.IsTrue(CheckOutHotel(roomName));
            Assert.IsTrue(RepairRoom(roomName));
            Assert.AreEqual(VacantRooms.Count, 0);
            Assert.AreEqual(RepairingRooms.Count, 1);
            Assert.IsTrue(RepairDone(roomName));
            Assert.AreEqual(VacantRooms.Count, 1);
            Assert.AreEqual(RepairingRooms.Count, 0);
        }

        [TestCase(
           TestName = "Shortest Distance Room Test",
           Description =
           "Always check in the available shortest distance room")]
        public void ShortestDistanceRoom_Test()
        {
            string room1 = "1A", room2 = "1B", room3 = "1C";
            
            Assert.IsTrue(CheckInHotel()); // 1A
            Assert.IsTrue(CheckInHotel()); // 1B
            Assert.IsTrue(CheckInHotel()); // 1C
            Assert.IsTrue(CheckOutHotel(room3));
            Assert.IsTrue(CheckOutHotel(room1));
            Assert.IsTrue(CheckOutHotel(room2));
            Assert.IsTrue(CleanRoom(room3));
            Assert.IsTrue(CleanRoom(room1));
            Assert.IsTrue(CleanRoom(room2));
            Assert.That(OccupiedRooms, Does.Not.ContainKey(room1));
            Assert.IsTrue(CheckInHotel());
            Assert.That(OccupiedRooms, Does.ContainKey(room1));
        }
    }
}