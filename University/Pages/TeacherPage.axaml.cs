using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace University.Pages;

public partial class TeacherPage : Window
{
    public TeacherPage()
    {
        InitializeComponent();
    }

    private void ViewExamsButton_Click(object? sender, RoutedEventArgs e)
    {
        MainControl.Content = new ViewExamsTeacherPage();
    }
}