using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AutomaticBackupTest;


[TestClass]
public class AutomaticBackupTestCases
{
    /**
     * <summary>
     *      Tests the creation of the Backup object
     * </summary>
     */
    [TestMethod]
    public void AutomaticBackupCreation()
    {
        AutomaticBackup.Backup ab = new AutomaticBackup.Backup("C");

        Assert.AreEqual(@"C:\", ab.DriveLocation);
        Assert.AreEqual($@"{DateTime.Now.ToString("ddMMMyyyy")}\", ab.DailyFolder);
    }

    /**
     * <summary>
     *      Tests the creation of the backup directory
     * </summary>
     */
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

    /**
     * <summary>
     *      Tests the creation of the daily backup folder
     * </summary>
     */
    [TestMethod]
    public void AutomaticBackupDailyFolderCreation()
    {
        AutomaticBackup.Backup ab = new AutomaticBackup.Backup("C");

        Assert.IsFalse(Directory.Exists($@"{ab.DriveLocation}Laptop Backups"));
        Assert.IsTrue(ab.CreateDailyBackupFolder());
        Assert.IsTrue(Directory.Exists($@"{ab.DriveLocation}Laptop Backups\{ab.DailyFolder}"));
        Assert.IsFalse(ab.CreateDailyBackupFolder());

        AutomaticBackup.Backup.DeleteDirectory($@"{ab.DriveLocation}Laptop Backups\", true);
        Assert.IsFalse(Directory.Exists($@"{ab.DriveLocation}Laptop Backups\"));
    }

    /**
     * <summary>
     *      Tests the removal of old backups
     * </summary>
     */
    [TestMethod]
    public void AutomaticBackupDeleteOldDirectories()
    {
        AutomaticBackup.Backup ab = new AutomaticBackup.Backup("C");
        string dirToCreate = @"C:\Laptop Backups\21Feb2023";

        if(!Directory.Exists(dirToCreate)) 
            Directory.CreateDirectory(dirToCreate);

        Directory.SetCreationTime(dirToCreate, DateTime.Now.AddMonths(-5));

        Assert.IsTrue(Directory.Exists(dirToCreate));
        ab.RemoveOldBackups(3);
        Assert.IsFalse(Directory.Exists(dirToCreate));

        AutomaticBackup.Backup.DeleteDirectory($@"{ab.DriveLocation}Laptop Backups\", true);
        Assert.IsFalse(Directory.Exists($@"{ab.DriveLocation}Laptop Backups\"));
    }


}
