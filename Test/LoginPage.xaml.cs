using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Drawing;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        public void Login(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.Password;
            var connection_string = "Server=tcp:vchat.database.windows.net,1433;Initial Catalog=vchat;Persist Security Info=False;User ID=vchat;Password=vcfstudio1@adminchatsql;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var connection = new SqlConnection(connection_string);
            connection.Open();
            var sql = "SELECT[Firstname], [Lastname], [Email] FROM[dbo].[VChat_Users] WHERE[Username] = '" + username +  "'AND [Password] = '"  + password + "';" ;
            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                Message.Text = "Username or Password is incorrect";
            }
            else
            {
                frame.Navigate(typeof(MainPage), username);
            }
            connection.Close();
            command.Dispose();
            reader.Close();
        }
        public void Register(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(RagistrationPage));
        }
    }
}
