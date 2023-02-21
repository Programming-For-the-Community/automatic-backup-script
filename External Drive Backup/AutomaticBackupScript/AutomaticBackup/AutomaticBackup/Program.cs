
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
        if (args.Length > 0)
        {
            ab = new Backup(args[0].ToUpper());
        }
        else
        {
            ab = new Backup("D");
        }

        bool hasDDrive = false;

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

            try
            {
                ab.CopyCustomPrograms();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

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
