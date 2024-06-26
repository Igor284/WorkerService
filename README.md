How to Work with It:

1.Open PowerShell as an administrator and execute the following command to create the service: sc.exe create WorkerService binpath= "c:\Your path to the file\WorkerService.exe" start= auto
2.Open the Services application on your system, find WorkerService, and start the service (right-click and select Start).
3.Go to C:\temp\workerservice, open LogFile.txt, and ensure the service is running and there are no errors in the logs.
4.In the WorkerService directory, open appsettings.json and configure your settings. You can do this while the service is running; just make sure to save the changes:
{
  "WorkerOptions": {
    "Format": "Json", // Select the format in which the data will be written to the file (XML, JSON, CSV) (Not case sensitive)
    "Interval": 5 // Set the interval in seconds
  }
}
Go to C:\temp\workerservice and open rates.txt. You should see the data from the National Bank of Ukraine in the format specified in the configuration. The data will be updated at the frequency set in the configuration.

If you want to remove the service:

  1.Open the Services application, find WorkerService, and stop the service (right-click and select Stop).
  2.Go to C:\temp\workerservice, open LogFile.txt, and ensure the service is stopped.
  3.Open PowerShell as an administrator and execute the following command to delete the service: sc.exe delete WorkerService
