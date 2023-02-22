using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AutomaticBackupTest
{

    [TestClass]
    public class AutomaticBackupTestCases
    {
        [TestMethod]
        public void AutomaticBackupCreation()
        {
            AutomaticBackup.Backup ab = new AutomaticBackup.Backup("C");

            Assert.AreEqual(@"C:\", ab.DriveLocation);
            Assert.AreEqual($@"{DateTime.Now.ToString("ddMMMyyyy")}\", ab.DailyFolder);
        }

        [TestMethod]
        public void AutomaticBackupBackupDirCreation()
        {
            AutomaticBackup.Backup ab = new AutomaticBackup.Backup("C");

            Assert.IsFalse(Directory.Exists($@"{ab.DriveLocation}Laptop Backups\"));
            Assert.IsTrue(ab.CreateBackupsDir());
            Assert.IsTrue(Directory.Exists($@"{ab.DriveLocation}Laptop Backups\"));
            Assert.IsFalse(ab.CreateBackupsDir());

            AutomaticBackup.Backup.DeleteDirectory($@"{ab.DriveLocation}Laptop Backups\", true);
            Assert.IsFalse(Directory.Exists($@"{ab.DriveLocation}Laptop Backups\"));
        }

        [TestMethod]
        public void AutomaticBackupDailyFolderCreation()
        {
            AutomaticBackup.Backup ab = new AutomaticBackup.Backup("C");

            Assert.IsFalse(Directory.Exists($@"{ab.DriveLocation}Laptop Backups\{ab.DailyFolder}"));
            Assert.IsTrue(ab.CreateDailyBackupFolder());
            Assert.IsTrue(Directory.Exists($@"{ab.DriveLocation}Laptop Backups\{ab.DailyFolder}"));
            Assert.IsFalse(ab.CreateDailyBackupFolder());

            AutomaticBackup.Backup.DeleteDirectory($@"{ab.DriveLocation}Laptop Backups\", true);
            Assert.IsFalse(Directory.Exists($@"{ab.DriveLocation}Laptop Backups\"));
        }


    }
}