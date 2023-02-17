using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AutomaticBackupTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void AutomaticBackupCreation()
    {
        AutomaticBackup ab = new AutomaticBackup.Backup("C");

        Assert.AreEqual(@"X:\", ab.DriveLocation);
        Assert.AreEqual($@"{DateTime.Now.ToString("ddd-ddMMMyyyy")}\", ab.DailyFolder);
    }
}