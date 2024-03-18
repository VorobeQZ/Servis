using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace Servis;

public partial class AddUpd_Uslugi : Window
{
    private List<Usluga> usluga;
    private Usluga CurrentData;
    
    public AddUpd_Uslugi(Usluga currentData,List<Usluga> uslugas)
    {
        InitializeComponent();
        CurrentData = currentData;
        this.DataContext = CurrentData;
        usluga = uslugas;
    }
private MySqlConnection conn;
private string connStr = "server=localhost;database=masterskaya;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
private void Save_OnClick(object? sender, RoutedEventArgs e)
{
var save = usluga.FirstOrDefault(x => x.Код == CurrentData.Код);
if (save == null)
{
try
{
conn = new MySqlConnection(connStr);
conn.Open();
string add = "INSERT INTO услуга (Наименование,Стоимость,Ожидаемое_время_выполнения,Фото,Скидка,Итоговая_стоимость) VALUES ('"  + Наименование.Text + "', '" + Convert.ToDouble(Стоимость.Text )+ "', '" + Ожидаемое_время_выполнения.Text  + "','"+ Фото.Text + "','" + Convert.ToDouble(Скидка.Text ) + "','" + Convert.ToDouble(Итоговая_стоимость.Text )  + "');";
MySqlCommand cmd = new MySqlCommand(add, conn);
cmd.ExecuteNonQuery();
conn.Close();
}
catch (Exception exception)
{
Console.WriteLine("Error" + exception);
}
}
else
{
try
{
conn = new MySqlConnection(connStr);
conn.Open();
Итоговая_стоимость.Text = String.Empty;
string upd = "UPDATE услуга SET Наименование = '" + Наименование.Text + "', Стоимость = '" + Convert.ToDouble(Стоимость.Text ) + "', Ожидаемое_время_выполнения = '"+ Ожидаемое_время_выполнения.Text + "',Скидка = '"+ Convert.ToDouble(Скидка.Text) + "',Фото = '"+ Фото.Text + "',Итоговая_стоимость = '" + Convert.ToDouble(Итоговая_стоимость.Text ) + "' WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
MySqlCommand cmd = new MySqlCommand(upd, conn);
cmd.ExecuteNonQuery();
conn.Close();
}
catch (Exception exception)
{
Console.Write("Error" + exception);
}
}
}

private void GoBack(object? sender, RoutedEventArgs e)
{
Servis.Uslugi back = new Servis.Uslugi();
this.Close();
back.Show();
}
private async void File_Select(object sender, RoutedEventArgs e)
{
try
{
OpenFileDialog fileDialog = new OpenFileDialog();
fileDialog.Filters.Add(new FileDialogFilter()
{ Name = "Image Files", Extensions = { "jpg", "jpeg", "png", "gif" } });
string[]? fileNames = await fileDialog.ShowAsync(this);
if (fileNames != null && fileNames.Length > 0)
{
string imagePath = System.IO.Path.GetFileName(fileNames[0]);
Фото.Text = imagePath;
}
}
catch (Exception ex)
{
Console.WriteLine(ex.Message);
}
}
}
