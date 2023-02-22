
namespace AutomaticBackup;
class Program
{
    /**
        <summary>
            Creates a backup of the C: drive at the location defined by <paramref name="args"/>[0]
        </summary>

        <param name="args">string arguments passed when the file is run</param>
    */
    static void Main(string[] args)
    {
        Backup ab;
        bool hasDDrive = false;
        int maxAgeInMonths = 1;

        // If provided a specific external drive, use that drive
        if (args.Length > 0)
        {
            ab = new Backup(args[0].ToUpper());
        }
        else
        {
            ab = new Backup("D");
        }

        try
        {
            hasDDrive = Directory.Exists(ab.DriveLocation);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        if (hasDDrive)
        {
            ab.CreateBackupsDir();
            ab.CreateDailyBackupFolder();

            // Remove backups that are more than one month old
            try
            {
                ab.RemoveOldBackups(maxAgeInMonths);
            }
            catch(DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            // Try to copy the custom programs folder
            try
            {
                ab.CopyCustomPrograms();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // Try to copy the Documents folder
            try
            {
                ab.CopyDocuments();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
