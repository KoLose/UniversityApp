using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace University.Pages;

public partial class DepartmentHeadPage : Window
{
    public DepartmentHeadPage()
    {
        InitializeComponent();
    }

    private void StudentsButton_Click(object? sender, RoutedEventArgs e)
    {
        MainControl.Content = new ViewStudentsHeadPage();
    }

    private void ExamsButton_Click(object? sender, RoutedEventArgs e)
    {
        MainControl.Content = new ViewExamsHeadPage();
    }

    private void DisciplinesButton_Click(object? sender, RoutedEventArgs e)
    {
        MainControl.Content = new ViewDisciplinesHeadPage();
    }
}