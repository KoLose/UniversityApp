using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using University.Data;
using System.Linq;

namespace University.Pages;

public partial class AddStudentWindow : Window
{
    public AddStudentWindow()
    {
        InitializeComponent();
        LoadSpecialties();
    }

    private void LoadSpecialties()
    {
        var specialties = App.DbContext.Specialties
            .Select(s => new { s.SpecialtyId, s.Name })
            .ToList();
        SpecialtyCombo.ItemsSource = specialties;
        SpecialtyCombo.DisplayMemberBinding = new Avalonia.Data.Binding("Name");
    }

    private void CreateButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FullNameBox.Text) ||
            string.IsNullOrWhiteSpace(RegNumberBox.Text) ||
            string.IsNullOrWhiteSpace(LoginBox.Text) ||
            string.IsNullOrWhiteSpace(PasswordBox.Text) ||
            SpecialtyCombo.SelectedItem == null)
            return;

        var specialty = (dynamic)SpecialtyCombo.SelectedItem;

        var student = new Student
        {
            FullName = FullNameBox.Text.Trim(),
            RegistrationNumber = RegNumberBox.Text.Trim(),
            Login = LoginBox.Text.Trim(),
            PasswordHash = PasswordBox.Text,
            SpecialtyId = specialty.SpecialtyId
        };

        App.DbContext.Students.Add(student);
        App.DbContext.SaveChanges();
        Close();
    }

    private void CancelButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) => Close();
}