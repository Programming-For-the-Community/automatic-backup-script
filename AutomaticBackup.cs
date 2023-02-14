class AutomaticBackup {
    private string dDrive = @"D:\";
    static void Main(string[] args){
        AutomaticBackup ab = new AutomaticBackup();

        bool hasDDrive = false;

        try {
            hasDDrive = Directory.Exists(ab.dDrive);
        } catch (Exception e) {
            Console.WriteLine(e.Message);
        }

        if(hasDDrive){
            ab.checkForBackupsDir();
        }
    }

    private void checkForBackupsDir(){
        if(!Directory.Exists($"{dDrive}Backups")){
            Directory.CreateDirectory($"{dDrive}Backups");
        }
    }
}