using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkDemo
{
    public partial class Form1 : Form
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

        static readonly string ApplicationName = "Legislators";

        static readonly string SpreadSheetId = "1LAF4vjm9BpkRFgcUghiMlcZ_aYimJOHR2Mf039HVwnU";

        static readonly string sheet = "template";

        static SheetsService service;

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            GoogleCredential credential;
            using (var stream = new FileStream(@"C:\Users\Adina\Desktop\DarkDashboard-master\DarkDashboard-master\DarkDemo\DarkDemo\MoneyManagement-ff042d0d31ae.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            var range = $"{sheet}!A1:D3";
             var request = service.Spreadsheets.Values.Get(SpreadSheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    textBox1.Text = row[1].ToString();
                }
            }

        }
    }
}
