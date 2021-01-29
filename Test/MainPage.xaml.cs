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
using System.Data.SqlClient;
using System.Diagnostics;
using Windows.UI;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static string session
        {
            get; set;
        }
        public string connection_string { get; set; }
        public SqlConnection connection { get; set; }
        public SqlCommand command { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            session = e.Parameter.ToString();
            connection_string = "Server=tcp:vchat.database.windows.net,1433;Initial Catalog=vchat;Persist Security Info=False;User ID=vchat;Password=vcfstudio1@adminchatsql;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            connection = new SqlConnection(connection_string);
            command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
        }
        public MainPage()
        {

            this.InitializeComponent();
            list.Items.Clear();

            connection_string = "Server=tcp:vchat.database.windows.net,1433;Initial Catalog=vchat;Persist Security Info=False;User ID=vchat;Password=vcfstudio1@adminchatsql;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            connection = new SqlConnection(connection_string);
            command = new SqlCommand();
            connection.Open();
            command.Connection = connection;

            var sql = "SELECT [Firstname], [Lastname], [Username] FROM [dbo].[VChat_Users]";
            command.CommandText = sql;
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var text = reader.GetValue(0).ToString().ToUpperInvariant() + " " + reader.GetValue(1).ToString().ToUpperInvariant();
                var output = new TextBlock();
                output.Text = text;
                output.Foreground = new SolidColorBrush(Colors.White);
                output.Name = reader.GetValue(2).ToString();
                output.Width = 350;
                output.Margin = new Thickness(0, 0, 0, 0);
                output.PointerPressed += ChatBox;
                list.Items.Add(output);
            }
            reader.Close();
            command.Dispose();


        }

        private void ChatList(object sender, RoutedEventArgs e)
        {
            list.Items.Clear();

            var sql = "SELECT [Firstname], [Lastname], [Username] FROM [dbo].[VChat_Users]";
            command.CommandText = sql;
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                /* var text = reader.GetValue(0).ToString().ToUpperInvariant() + " " + reader.GetValue(1).ToString().ToUpperInvariant();
                var output = new TextBlock();
                output.Text = text;
                output.Foreground = new SolidColorBrush(Colors.White);
                output.Name = reader.GetValue(2).ToString();
                output.PointerPressed += ChatBox; */
                // var output = "<ListViewItem> <Grid HorizontalAlignment='Center' Margin='5'> <Grid.ColumnDefinitions> <ColumnDefinition Width='50'/> <ColumnDefinition Width='150'/> <ColumnDefinition Width='50*'/> </Grid.ColumnDefinitions> <Border Width='40' Height='40' CornerRadius='25' BorderBrush='White' BorderThickness='0.6'/> <Border Width='10' Height='10' VerticalAlignment='Bottom' Margin='5' HorizontalAlignment='Right' CornerRadius='15' Background='#FF87EE87'/><StackPanel Grid.Column='1'> <TextBlock Foreground='White' Text='Himanshu' Margin='10 0'/> <TextBlock Foreground='White' Text='VCF' Margin='10 0' TextTrimming='CharacterEllipsis' Opacity='0.6' FontSize='11'/> </StackPanel><Border Grid.Column='2' Width='20' Height='20' CornerRadius='15' Background='White' HorizontalAlignment='Center' VerticalAlignment='Bottom' Margin='0,0,0,10'> <TextBlock Foreground='Black' FontSize='11' Text='5' HorizontalAlignment='Center' VerticalAlignment='Center'/> </Border> </Grid> </ListViewItem>";

                Button new1 = new Button();

                new1.Content = reader.GetValue(0).ToString().ToUpper() + " " + reader.GetValue(1).ToString().ToUpper();
                new1.Name = reader.GetValue(2).ToString();
                new1.Width = 300;
                new1.Background = new SolidColorBrush(Colors.Transparent);
                new1.Click += ChatBox;
                list.Items.Add(new1);

            }
            reader.Close();
            command.Dispose();


        }



        private void ProfileSection(ref Button user)
        {
            var sql = "SELECT [Firstname], [Lastname], [Username], [Email], [Phone], [Address] FROM [dbo].[VChat_Users] WHERE(Username = '" + user.Name + "')";
            command.CommandText = sql;
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Fullname.Text = (reader.GetValue(0).ToString() + " " + reader.GetValue(1).ToString()).ToUpper();
                Username.Text = reader.GetValue(2).ToString();
                Email.Text = reader.GetValue(3).ToString();
                Phone.Text = reader.GetValue(4).ToString();
                Address.Text = reader.GetValue(5).ToString();
            }
            reader.Close();
            command.Dispose();
        }

        private void ChatBox(object sender, RoutedEventArgs e)
        {
            var user = (Button)sender;
            var sql = "SELECT [Firstname], [Lastname] FROM [dbo].[VChat_Users] WHERE(Username = '" + user.Name + "')";
            command.CommandText = sql;
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Recevername.Text = (reader.GetValue(0).ToString() + " " + reader.GetValue(1).ToString()).ToUpper();
            }

            reader.Close();
            command.Dispose();
            ProfileSection(ref user);
        }

        /**
        private void PeopleList(object sender, RoutedEventArgs e)
        {
            list.Items.Clear();

            var sql = "SELECT [Firstname], [Lastname], [Username] FROM [dbo].[VChat_Users]";
            command.CommandText = sql;
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var text = reader.GetValue(0).ToString().ToUpperInvariant() + " " + reader.GetValue(1).ToString().ToUpperInvariant();
                var output = new TextBlock();
                output.Text = text;
                output.Foreground = new SolidColorBrush(Colors.White);
                output.Name = reader.GetValue(2).ToString();
                output.PointerPressed += ChatBox;
                output.PointerPressed += ProfileSection;
                list.Items.Add(output);
            }
            reader.Close();
            command.Dispose();

        }
        **/

        /**
         private void ContectList(object sender, RoutedEventArgs e)
         {
             list.Items.Clear();
             var Conn_Str = "Server=tcp:vchat.database.windows.net,1433;Initial Catalog=vchat;Persist Security Info=False;User ID=vchat;Password=vcfstudio1@adminchatsql;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
             var connection = new SqlConnection(Conn_Str);
             connection.Open();

             var sql = "SELECT [Firstname], [Lastname] FROM [dbo].[VChat_Users]";
             var command = new SqlCommand(sql, connection);
             var reader = command.ExecuteReader();

             while (reader.Read())
             {
                 var text = reader.GetValue(0).ToString().ToUpperInvariant() + " " + reader.GetValue(1).ToString().ToUpperInvariant();
                 var output = new TextBlock();
                 output.Text = text;
                 output.Foreground = new SolidColorBrush(Colors.White);
                 list.Items.Add(output);
             }
             reader.Close();
             command.Dispose();
             connection.Close();
         }
        **/
    }
}
