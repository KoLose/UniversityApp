using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using University.Data;
using System.Linq;

namespace University.Pages;

public partial class AddEmployeeWindow : Window
{
    public AddEmployeeWindow()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        // Кафедры
        var depts = App.DbContext.Departments
            .Select(d => new { d.DepartmentId, d.Name })
            .ToList();
        DepartmentCombo.ItemsSource = depts;
        DepartmentCombo.DisplayMemberBinding = new Avalonia.Data.Binding("Name");

        // Роли
        var roles = App.DbContext.Roles
            .Select(r => new { r.RoleId, r.Name })
            .ToList();
        RoleCombo.ItemsSource = roles;
        RoleCombo.DisplayMemberBinding = new Avalonia.Data.Binding("Name");

        // Шефы (все сотрудники)
        var chiefs = App.DbContext.Employees
            .Select(e => new { e.EmployeeId, e.FullName })
            .ToList();
        ChiefCombo.ItemsSource = chiefs;
        ChiefCombo.DisplayMemberBinding = new Avalonia.Data.Binding("FullName");
    }

    private void CreateButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FullNameBox.Text) ||
            string.IsNullOrWhiteSpace(PositionBox.Text) ||
            string.IsNullOrWhiteSpace(SalaryBox.Text) ||
            string.IsNullOrWhiteSpace(LoginBox.Text) ||
            string.IsNullOrWhiteSpace(PasswordBox.Text) ||
            DepartmentCombo.SelectedItem == null ||
            RoleCombo.SelectedItem == null)
            return;

        if (!decimal.TryParse(SalaryBox.Text, out var salary))
            return;

        var dept = (dynamic)DepartmentCombo.SelectedItem;
        var role = (dynamic)RoleCombo.SelectedItem;
        var chief = (dynamic)ChiefCombo.SelectedItem; // может быть null

        var emp = new Employee
        {
            FullName = FullNameBox.Text.Trim(),
            Position = PositionBox.Text.Trim(),
            Salary = salary,
            Login = LoginBox.Text.Trim(),
            PasswordHash = PasswordBox.Text,
            DeptId = dept.DepartmentId,
            RoleId = role.RoleId,
            ChiefId = chief?.EmployeeId // null, если не выбран
        };

        App.DbContext.Employees.Add(emp);
        App.DbContext.SaveChanges();
        Close();
    }

    private void CancelButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) => Close();
}