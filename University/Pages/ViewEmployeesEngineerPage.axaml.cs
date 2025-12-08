using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using University.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Pages;

public partial class ViewEmployeesEngineerPage : UserControl
{
    private List<Employee> _allEmployees = new();

    public ViewEmployeesEngineerPage()
    {
        InitializeComponent();
        LoadEmployees();
    }

    private void LoadEmployees()
    {
        _allEmployees = App.DbContext.Employees
            .Include(e => e.Dept)
            .Include(e => e.Chief)
            .Include(e => e.Role)
            .ToList();
        ApplyFilter();
    }

    private void ApplyFilter()
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
        ApplyFilter();
    }

    private async void AddEmployeeButton_Click(object? sender, RoutedEventArgs e)
    {
        var parent = this.VisualRoot as Window;
        if (parent == null) return;
        var win = new AddEmployeeWindow();
        await win.ShowDialog(parent);
        LoadEmployees();
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        App.DbContext.SaveChanges();
    }

    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        var btn = sender as Button;
        var emp = btn?.DataContext as Employee;
        if (emp == null) return;

        App.DbContext.Employees.Remove(emp);
        App.DbContext.SaveChanges();
        LoadEmployees();
    }
}