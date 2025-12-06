using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using University.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Pages;

public partial class ViewStudentsPage : UserControl
{
    private List<Student> _allStudents = new();

    public ViewStudentsPage()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        _allStudents = App.DbContext.Students
            .Include(s => s.Specialty)
            .ToList();

        ApplyFilters();
    }

    private void ApplyFilters()
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
        ApplyFilters();
    }
}