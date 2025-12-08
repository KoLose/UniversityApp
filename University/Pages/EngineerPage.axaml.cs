using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace University.Pages;

public partial class EngineerPage : Window
{
    public EngineerPage()
    {
        InitializeComponent();
    }

    private void ViewEmployeesButton_Click(object? sender, RoutedEventArgs e)
    {
        MainControl.Content = new ViewEmployeesEngineerPage();
    }
}