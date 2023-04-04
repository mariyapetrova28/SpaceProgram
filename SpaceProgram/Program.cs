using SpaceProgram;
using SpaceProgram.Exceptions;
using SpaceProgram.Language;
using SpaceProgram.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;

//Variables needed
List<DataInputModel> records = new List<DataInputModel>();
List<DataOutputModel> reportRecords = new List<DataOutputModel>();
List<DataOutputModelDE> reportRecordsDE = new List<DataOutputModelDE>();
string filePath = "";
string senderEmail = "";
string senderEmailPassword = "";
string receiverEmail = "";
string cmd = "Yes";
string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
bool infoCollected = false;
string lang;

// UI Console.WriteLine($"{LangHelper.GetString("Hello")}\n{LangHelper.GetString("Name")}");
do
{
    Console.Write("Please enter the language you want to continue with(EN or DE)\nBitte geben Sie die Sprache ein, mit der Sie fortfahren möchten (EN oder DE)\nEnter: ");
    lang = Console.ReadLine();
}
while (!(lang).Equals("DE") && !lang.Equals("EN"));
if (lang.Equals("DE"))
{
    LanguageHelper.ChangeLanguage("de");
    cmd = "Ja";
}
Console.WriteLine($"{LanguageHelper.GetString("Hello")}\n{LanguageHelper.GetString("EnterDetails")}\n{LanguageHelper.GetString("Summary")}\n{LanguageHelper.GetString("Start")}");
while (cmd.Equals(LanguageHelper.GetString("Yes")))
{
    Console.Write($"{LanguageHelper.GetString("EnterFilePath")} ");
    filePath = Console.ReadLine();
    if (!System.IO.File.Exists(filePath))
    {
        Console.WriteLine($"{LanguageHelper.GetString("WrongFilePath")}");
        Console.Write($"{LanguageHelper.GetString("ContinueInsert")} ");
        cmd=Console.ReadLine();
    }
    else
    {
        while (cmd.Equals(LanguageHelper.GetString("Yes")))
        {
            Console.Write($"{LanguageHelper.GetString("SenderEmail")} ");
            senderEmail = Console.ReadLine();
            if (!senderEmail.EndsWith("@gmail.com"))
            {
                Console.WriteLine($"{LanguageHelper.GetString("EmailFormat")}");
                Console.Write($"{LanguageHelper.GetString("ContinueInsert")} ");
                cmd = Console.ReadLine();
            }
            else
            {
                Console.Write($"{LanguageHelper.GetString("PasswordEmail")} ");
                senderEmailPassword = Console.ReadLine();
                while (cmd.Equals(LanguageHelper.GetString("Yes")))
                {
                    Console.Write($"{LanguageHelper.GetString("SendToEmail")} ");
                    receiverEmail = Console.ReadLine();
                    if (!Regex.IsMatch(receiverEmail, emailPattern))
                    {
                        Console.WriteLine($"{LanguageHelper.GetString("EmailFormat")}");
                        Console.Write($"{LanguageHelper.GetString("ContinueInsert")} ");
                        cmd = Console.ReadLine();
                    }
                    else
                    {
                        cmd = "";
                        infoCollected= true;
                    }
                }
                
            }
        }
       
    }
    
}
if(infoCollected)
{
    //Reading the file
    OperationsWithFile operations = new OperationsWithFile();
    try
    {
        records = operations.ReadFile(filePath);
    }
    catch (FilePathNameException e)
    {
        Console.WriteLine(e.Message);
    }
    catch (RecordsAreZeroException e)
    {
        Console.WriteLine(e.Message);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

    // DISPLAYING RECORDS
    //try
    //{
    //    operations.DisplayAllRecords(records);
    //}
    //catch (IndexOutOfRangeException e)
    //{
    //    Console.WriteLine(e.Message);
    //}
    //catch (Exception e)
    //{
    //    Console.WriteLine(e.Message);
    //}

    //Creating WeatherReport.csv
    WeatherReportFile weatherReport = new WeatherReportFile();
    try
    {
        if (lang.Equals("EN"))
        {
            reportRecords = weatherReport.CreateWeatherReportRecords(records);
        }
        else
        {
            reportRecordsDE=weatherReport.CreateWeatherReportRecordsDE(records);
        }
        
    }
    catch (RecordsAreZeroException e)
    {
        Console.WriteLine(e.Message);
    }
    try
    {
        if (lang.Equals("EN"))
        {
            operations.WriteFile(reportRecords);
        }
        else
        {
            operations.WriteFile(reportRecordsDE);
        }
    }
    catch (CreateWeatherReportException e)
    {
        Console.WriteLine(e.Message);
    }

    //Email 
    EmailSender emailSender = new EmailSender();
    try
    {
        if (lang.Equals("EN"))
        {
            emailSender.SendEmail(senderEmail, senderEmailPassword, receiverEmail);
        }
        else
        {
            emailSender.SendEmailDE(senderEmail, senderEmailPassword, receiverEmail);
        }
        
    }
    catch (EmailFormatException e)
    {
        Console.WriteLine(e.Message);
    }
    catch (SmtpException e)
    {
        Console.WriteLine(e.Message);
    }
}
else
{
    Console.WriteLine($"{LanguageHelper.GetString("Bye")}");
}
