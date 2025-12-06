using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using University.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Pages;

public partial class ViewDisciplinesPage : UserControl
{
    private List<Subject> _allSubjects = new();
    private List<Department> _departments = new();

    public ViewDisciplinesPage()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        _allSubjects = App.DbContext.Subjects
            .Include(s => s.ExecutorDept)
            .ToList();

        _departments = App.DbContext.Departments.ToList();

        // Заполняем ComboBox кафедрами
        DepartmentFilter.ItemsSource = _departments.Select(d => d.Name).ToList();

        ApplyFilters();
    }

    private void ApplyFilters()
    {
        string search = SearchBox.Text?.ToLower().Trim() ?? "";
        string selectedDept = DepartmentFilter.SelectedItem as string;

        var filtered = _allSubjects.Where(s =>
        {
            bool matchesSearch = string.IsNullOrEmpty(search) ||
                                 s.Name?.ToLower().Contains(search) == true;

            bool matchesDept = string.IsNullOrEmpty(selectedDept) ||
                               s.ExecutorDept?.Name == selectedDept;

            return matchesSearch && matchesDept;
        }).ToList();

        DisciplinesDataGrid.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilters();
    }

    private void DepartmentFilter_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ApplyFilters();
    }
}