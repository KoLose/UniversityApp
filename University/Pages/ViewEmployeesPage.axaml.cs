using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using University.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Pages;

public partial class ViewEmployeesPage : UserControl
{
    private List<Employee> _allEmployees = new();

    public ViewEmployeesPage()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        _allEmployees = App.DbContext.Employees
            .Include(e => e.Dept)       // Кафедра
            .Include(e => e.Chief)      // Руководитель (сам Employee)
            .Include(e => e.Role)       // Роль
            .ToList();

        ApplyFilters();
    }

    private void ApplyFilters()
    {
        string search = SearchBox.Text?.ToLower().Trim() ?? "";
        var filtered = _allEmployees.Where(e =>
            string.IsNullOrEmpty(search) ||
            e.FullName?.ToLower().Contains(search) == true
        ).ToList();

        EmployeesDataGrid.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilters();
    }
}