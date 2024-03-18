using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Servis;

public partial class Zakazs : Window
{
    public Zakazs()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM заказ;";
        ShowTable(fullTable);
        FillCmb();
    }
    private List<Zakaz> zakazes;
    private string connStr = "server=localhost;database=masterskaya;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private MySqlConnection conn;

public void ShowTable(string sql)
{

zakazes = new List<Zakaz>();
conn = new MySqlConnection(connStr);
conn.Open();
MySqlCommand command = new MySqlCommand(sql, conn);
MySqlDataReader reader = command.ExecuteReader();
{
while (reader.Read() && reader.HasRows)
{
var currentData = new Zakaz()
{
Код = reader.GetInt32("Код"),
Клиент = reader.GetInt32("Клиент"),
Услуга = reader.GetInt32("Услуга"),
Дата = reader.GetString("Дата")
};
zakazes.Add(currentData);
}

conn.Close();
DataGrid.ItemsSource = zakazes;
}
}
public void FillCmb()
{
zakazes = new List<Zakaz>();
conn = new MySqlConnection(connStr);
conn.Open();
MySqlCommand command = new MySqlCommand("SELECT * FROM заказ", conn);
MySqlDataReader reader = command.ExecuteReader();
while (reader.Read() && reader.HasRows)
{
var currentData = new Zakaz()
{
Код = reader.GetInt32("Код"),
Клиент = reader.GetInt32("Клиент"),
Услуга = reader.GetInt32("Услуга"),
Дата = reader.GetString("Дата")
};
zakazes.Add(currentData);
}
conn.Close();
var typecmb = this.Find<ComboBox>(name:"CmbNum");
typecmb.ItemsSource = zakazes;
}

private void TwoSearch_OnClick(object? sender, RoutedEventArgs e)
{
string twotxt = "SELECT * FROM заказ WHERE Клиент LIKE '%" + SearchName.Text + "%' AND Услуга LIKE '%" + SearchPrice.Text + "%'";
ShowTable(twotxt);
}

private void Back_OnClick(object? sender, RoutedEventArgs e)
{
Servis.Menu menu = new Servis.Menu ();
Close();
menu.Show();
}

private void Reset_OnClick(object? sender, RoutedEventArgs e)
{
string reset = "SELECT * FROM заказ;";
ShowTable(reset);
SearchName.Text = string.Empty;
SearchPrice.Text = string.Empty;
}

private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
{
var TypeCmB = (ComboBox)sender;
var currentData = TypeCmB.SelectedItem as Zakaz;
var fltr = zakazes
.Where(x => x.Код == currentData.Код)
.ToList();
DataGrid.ItemsSource = fltr;
}

private void DeleteData(object? sender, RoutedEventArgs e)
{
try
{
Zakaz currentData = DataGrid.SelectedItem as Zakaz;
if (currentData == null)
{
return;
}
conn = new MySqlConnection(connStr);
conn.Open();
string sql = "DELETE FROM заказ WHERE Код = " + currentData.Код;
MySqlCommand cmd = new MySqlCommand(sql, conn);
cmd.ExecuteNonQuery();
conn.Close();
zakazes.Remove(currentData);
ShowTable("SELECT * FROM заказ;");
}
catch (Exception ex)
{
Console.WriteLine(ex.Message);
}
}

private void AddData(object? sender, RoutedEventArgs e)
{
Zakaz zakaz = new Zakaz();
Servis.AddUpd_Zakazs addWindow = new Servis.AddUpd_Zakazs(zakaz, zakazes);
addWindow.Show();
this.Close();
}

private void EditData(object? sender, RoutedEventArgs e)
{
Zakaz currentData = DataGrid.SelectedItem as Zakaz;
if (DataGrid == null)
{
return;
}
Servis.AddUpd_Zakazs editWindow = new Servis.AddUpd_Zakazs(currentData, zakazes);
editWindow.Show();
this.Close();
}
}
