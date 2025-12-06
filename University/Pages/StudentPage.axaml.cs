using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace University.Pages;

public partial class StudentPage : Window
{
    public StudentPage()
    {
        InitializeComponent();
    }

    private void ViewExamsButton_Click(object? sender, RoutedEventArgs e)
    {
        MainControl.Content = new ViewExamsPage();
    }

    private void ViewDisciplinesButton_Click(object? sender, RoutedEventArgs e)
    {
        MainControl.Content = new ViewDisciplinesPage();
    }

    private void ViewEmployeesButton_OnClick(object? sender, RoutedEventArgs e)
    {
        MainControl.Content = new ViewEmployeesPage();
    }

    private void ViewStudentsButton_Click(object? sender, RoutedEventArgs e)
    {
        MainControl.Content = new ViewStudentsPage();
    }
}