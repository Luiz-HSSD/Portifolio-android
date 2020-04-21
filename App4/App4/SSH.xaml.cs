using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SSH : ContentPage
    {
        public static string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ssh.txt");
        public static string cep_origem = "";
        public SSH()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            if (!File.Exists(fileName))
                File.WriteAllText(fileName, JsonConvert.SerializeObject(new SSHObject(){IP="",User="",Password="",Command="" }));
            SSHObject ssho= JsonConvert.DeserializeObject<SSHObject>(File.ReadAllText(fileName));
            ip.Text = ssho.IP;
            user.Text = ssho.User;
            password.Text = ssho.Password;
            comando.Text = ssho.Command;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            File.WriteAllText(fileName, Newtonsoft.Json.JsonConvert.SerializeObject(new SSHObject() { IP = ip.Text, User = user.Text, 
                Password = password.Text, Command = comando.Text }));
            using (var client = new SshClient(ip.Text, user.Text, password.Text))
            {
                client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(10);
                client.Connect();

                client.RunCommand(comando.Text);
                client.Disconnect();
            }
        }
    }
}