using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Servis;

public partial class Menu : Window
{
    public Menu()
    {
        InitializeComponent();
    }
    public void Usluga(object? sender, RoutedEventArgs e)
    {
        var uslugi = new Servis.Uslugi();
        Close();
        uslugi.Show(); 
    }
        
    public void Zakaz(object? sender, RoutedEventArgs e)
    {
        var zakazs = new Servis.Zakazs();
        Close();
        zakazs.Show(); 
    }
    

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}