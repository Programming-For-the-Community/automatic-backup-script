using System;
using System.IO;
namespace AutomaticBackup;
/**
    <summary>
        Class <c cref="Backup">Backup</c> automatically backs up selected directories from the Documents and Desktop folders to an external drive
    </summary>
*/
public class Backup
{
    /// <value><c>backupsFolder</c> is the name of the master backup directory</value>
    private string backupsFolder = @"Laptop Backups\";

    /// <value><c>customPrograms</c> is the location of the directory to be copied from the Desktop folder</value>
    private string customPrograms = @"C:\Users\hahmcm\Desktop\Custom Programs";

    /// <value><c>documents</c> is the location of the Documents directory</value>
    private string documents = @"C:\Users\hahmcm\Documents";

    /// <value> Property <c>DriveLocation</c> holds the location of the drive to backup the files to</value>
    public string DriveLocation { get; set; }

    /// <value> Property <c>DailyFolder</c> holds the location of folder to backup to each day</value>
    public string DailyFolder { get; set; }

    /**
        <summary>
            Backup constructor takes a string, <paramref name="driveLocation"/> which is where the backup will be created
        </summary>

        <param name="driveLocation">string of the drive to backup the files to</param>
    */
    public Backup(string driveLocation)
    {
        DriveLocation = $@"{driveLocation}:\";
        DailyFolder = $@"{DateTime.Now.ToString("ddd-ddMMMyyyy")}\";
    }

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
            ab.CheckForBackupsDir();
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


    /**
        <summary>
            Copies the files of the <paramref name="sourceDir"/> to the <paramref name="destinationDir"/> with the option to recursively copy any subdirectories of the <paramref name="sourceDir"/> <br/>

            Method taken from: https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
        </summary>

        <param name="sourceDir">source directory to copy from</param>
        <param name="destinationDir">directory to copy to</param>
        <param name="recursive">boolean option to recursively copy any subdirectories</param>
    */
    static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
    {
        // Get information about the source directory
        var dir = new DirectoryInfo(sourceDir);

        // Check if the source directory exists
        if (!dir.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

        // Cache directories before we start copying
        DirectoryInfo[] dirs = dir.GetDirectories();

        // Create the destination directory
        Directory.CreateDirectory(destinationDir);

        // Get the files in the source directory and copy to the destination directory
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(targetFilePath);
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive)
        {
            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir, true);
            }
        }
    }

    /**
        <summary>
            Checks for the master backups directory and creates it if necessary
        </summary>
    */
    private void CheckForBackupsDir()
    {
        if (!Directory.Exists(DriveLocation + backupsFolder))
        {
            Directory.CreateDirectory(DriveLocation + backupsFolder);
        }
    }

    /**
        <summary>
            Checks for the daily backup directory and creates it if it does not exist
        </summary>
    */
    private void CreateDailyBackupFolder()
    {
        if (!Directory.Exists(DriveLocation + backupsFolder + DailyFolder))
        {
            Directory.CreateDirectory(DriveLocation + backupsFolder + DailyFolder);
        }
    }

    /**
        <summary>
            Copies selected directories from the Desktop directory to the backup directory
        </summary>
    */
    private void CopyCustomPrograms()
    {

        if (Directory.Exists(customPrograms))
        {
            CopyDirectory(customPrograms, DriveLocation + backupsFolder + DailyFolder + @"Desktop Backup\", true);
        }
    }

    /**
        <summary>
            Copies selected directories from the Documents directory to the backup directory
        </summary>
    */
    private void CopyDocuments()
    {
        if (Directory.Exists(documents))
        {
            string[] dirs = Directory.GetDirectories(documents);

            foreach (string dir in dirs)
            {
                string[] dirParts = dir.Split(@"\");
                if (!(dir.EndsWith("Visual Studio 2019") || dir.EndsWith("SW Log Files") || dir.EndsWith("My Shapes") || dir.EndsWith("SOLIDWORKSComposer") || dir.EndsWith(".metadata") || dir.EndsWith("ArduinoData") || dir.EndsWith("My Pictures") || dir.EndsWith("My Music") || dir.EndsWith("My Videos")))
                {
                    CopyDirectory(dir, DriveLocation + backupsFolder + DailyFolder + @$"Documents Backup\{dirParts[dirParts.Length - 1]}", true);
                }
            }
        }
    }
}