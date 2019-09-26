using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using System.IO;

namespace DarkDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /*static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

        static readonly string ApplicationName="Legislators";

        static readonly string SpreadSheetId = "1LAF4vjm9BpkRFgcUghiMlcZ_aYimJOHR2Mf039HVwnU";

        static readonly string sheet = "template";

        static SheetsService service;
        */
        [STAThread]

        static void Main()
        {
            //  GoogleCredential credential;
            /*using (var stream = new FileStream(@"C:\Users\Adina\Desktop\DarkDashboard-master\DarkDashboard-master\DarkDemo\DarkDemo\MoneyManagement-ff042d0d31ae.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
*/
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
      //      ReadEntries();
            Application.Run(new Form1());
        }
        /*      static void ReadEntries()
              {
                  var range = $"{sheet}!A1:D3";
                  var request = service.Spreadsheets.Values.Get(SpreadSheetId,range);

                  var response = request.Execute();
                  var values = response.Values;
                  if (values != null && values.Count > 0)
                  {
                      foreach (var row in values)
                      {
                          Console.WriteLine("{0} {1} | {2} | {3} ", row[3],row[2],row[1],row[0]);
                      }
                  }

              }
          */
    }
}
