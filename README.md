# automatic-backup-script
Backup scripts here backup local laptop files to an external drive if it is present as well as backing up the external drive to a cloud bucket

## External Drive Backup
Backs up selected directiories from C:/Users/hahmcm/Documents and C:/Users/hahmcm/Desktop to D:/Laptop Backups when a D: drive is connected

### Runninng the App
In the obj/net7.0/Release folder there is an appHost file which will run the application. By default it looks for the D: drive, but it can also take an argument when scheduling the job to specify the drive to look for.

## Cloud Storage Backup
TBD...
