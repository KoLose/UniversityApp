using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using University.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Pages;

public partial class ViewStudentsHeadPage : UserControl
{
    private List<Student> _allStudents = new();

    public ViewStudentsHeadPage()
    {
        InitializeComponent();
        LoadStudents();
    }

    private void LoadStudents()
    {
        _allStudents = App.DbContext.Students
            .Include(s => s.Specialty)
            .ToList();
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        string search = SearchBox.Text?.ToLower().Trim() ?? "";
        var filtered = _allStudents.Where(s =>
            string.IsNullOrEmpty(search) ||
            s.FullName?.ToLower().Contains(search) == true
        ).ToList();
        StudentsDataGrid.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilter();
    }

    private async void AddStudentButton_Click(object? sender, RoutedEventArgs e)
    {
        var parent = this.VisualRoot as Window;
        if (parent == null) return;
        var win = new AddStudentWindow();
        await win.ShowDialog(parent);
        LoadStudents();
    }

    private void DeleteStudentButton_Click(object? sender, RoutedEventArgs e)
    {
        var btn = sender as Button;
        var student = btn?.DataContext as Student;
        if (student == null) return;

        App.DbContext.Students.Remove(student);
        App.DbContext.SaveChanges();
        LoadStudents();
    }
}