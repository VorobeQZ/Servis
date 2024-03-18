using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Servis;

public partial class Test : Window
{
    public Test()
    {
        InitializeComponent();
        this.Closing += Test_Closing;
        
    }
    private void Test_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true; // Запрещаем закрытие окна
        this.Hide();// Дополнительная логика, если необходимо
    }
    
    
}