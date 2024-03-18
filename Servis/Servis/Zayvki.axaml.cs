using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Servis;

public partial class Zayvki : Window
{
    private List<Zayvka> _rec;
    public Zayvki()
    {
        InitializeComponent();
    }
    private MySqlConnection conn;
    string connStr = "server=localhost;database=masterskaya;port=3306;User Id=root;password=Qwertyu1!ZZZ";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            string add = "INSERT INTO заявка (Фамилия,Имя,Отчество,Телефон,Проблема) VALUES ('" + Фамилия.Text + "', '" + Имя.Text + "', '" + Отчество.Text +"', '" + Телефон.Text +"', '" + Проблема.Text +"');";
            MySqlCommand cmd = new MySqlCommand(add, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        Uslugic back = new Uslugic();
        this.Hide();
        back.Show();
    }
}