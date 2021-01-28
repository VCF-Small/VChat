using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Data.SqlClient;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RagistrationPage : Page
    {
        public string connection_string { get; set; }
        public SqlConnection connection { get; set; }
        public SqlCommand command { get; set; }

        public bool username_avl = false;
        public string check_sql { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            connection_string = "Server=tcp:vchat.database.windows.net,1433;Initial Catalog=vchat;Persist Security Info=False;User ID=vchat;Password=vcfstudio1@adminchatsql;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            connection = new SqlConnection(connection_string);
            command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
        }
        public RagistrationPage()
        {
            this.InitializeComponent();
        }
        public void Login(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(LoginPage));
        }
        public void Register(object sender, RoutedEventArgs e)
        {
            var username = UsernameText.Text;
            

            check_sql = "SELECT [Id] FROM [dbo].[VChat_Users] WHERE [Username] = '" + username + "'";
            command.CommandText = check_sql;
            var reader = command.ExecuteReader();
            
            if (reader.HasRows)
            {
                Message.Text = "Username Already Exists";
                Message.Foreground = new SolidColorBrush(Colors.Red);
                username_avl = false;
            }
            
            else
            {
                var firstname = FirstnameText.Text;
                var lastname = LastnameText.Text;
                var email = EmailText.Text;
                var phone = PhoneText.Text;
                var address = AddressText.Text;
                var password = PasswordText.Password;
                var confirmpass = ConfirmpasswordText.Password;
                reader.Close();
                if (username != "" && firstname != "" && lastname != "" && email != "" && phone != "" && address != "" && password != "" && (password == confirmpass))
                {
                    var reg_sql = "INSERT INTO [dbo].[VChat_Users] ([Username], [Password], [Firstname], [Lastname], [Email], [Phone], [Address]) VALUES('" + username + "', '" + password + "','" + firstname + "', '" + lastname + "','" + email + "','" + phone + "', '" + address + "')";
                    command.CommandText = reg_sql;


                    if (command.ExecuteNonQuery() > 0)
                    {
                        var frn_list = "CREATE TABLE [dbo].[" + username + "_friendlist]([Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), [Friendusername] NVARCHAR(50) NOT NULL)";
                        command.CommandText = frn_list;
                        if (command.ExecuteNonQuery() > 0)
                        {
                            username_avl = false;
                        }

                    }
                    Message.Text = "Registered Successfully!! Go To Login Page By Clicking Below";
                    Message.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    Message.Text = "Plese fill all feilds and Password and Confirm Password should match";
                    Message.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
            reader.Close();
        }
        public void Check_Username(object sender, RoutedEventArgs e)
        {
            var username = UsernameText.Text;
            check_sql = "SELECT [Id] FROM [dbo].[VChat_Users] WHERE [Username] = '" + username + "'";
            command.CommandText = check_sql;
            
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Message.Text = "Username Already Exists";
                Message.Foreground = new SolidColorBrush(Colors.Red);
                username_avl = false;
            }
            else
            {
                Message.Text = "Username Available";
                Message.Foreground = new SolidColorBrush(Colors.Green);
                username_avl = true;

            }
            reader.Close();
        }
    }
}
