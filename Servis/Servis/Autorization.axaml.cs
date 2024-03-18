using System;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Servis;

public partial class Autorization : Window
{
    public Autorization()
    {
        InitializeComponent();
    }
    private MySqlConnection conn;
    private string connStr = "server=localhost;database=masterskaya;port=3306;User Id=admin;password=Qwertyu1!ZZZ";

    public void Authorization(object? sender, RoutedEventArgs e)
    {
        try
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            string check ="SELECT * FROM  логин WHERE  Логин = '" + Login.Text + "' AND Пароль ='" + Password.Text + "' LIMIT 1"; 
            MySqlCommand cmd = new MySqlCommand(check, conn);
            cmd.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1" )
            {
                var menu = new Servis.Menu();
                this.Hide();
                menu.Show(); 
            }
            else
            {
                Console.Write("Неверный логин или пароль");
            }
        }
        catch (Exception ex)
        {
            LogErr.IsVisible = true;
        }
        conn.Close();
    }
    
    public void Close(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}