using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using University.Pages;

namespace University
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void LoginButton_OnClick(object? sender, RoutedEventArgs e)
        {
            // Проверка введены ли данные
            if (string.IsNullOrEmpty(LoginBox.Text) || string.IsNullOrEmpty(PasswordBox.Text))
            {
                return;
            }
                        
            // Проверка существует ли аккаунт сотрудника
            var employee = App.DbContext.Employees.FirstOrDefault(emp => emp.Login == LoginBox.Text && emp.PasswordHash == PasswordBox.Text);
            
            // Проверка существует ли аккаунт студента
            var student = App.DbContext.Students.FirstOrDefault(st => st.Login == LoginBox.Text && st.PasswordHash == PasswordBox.Text);
            
            // Если нет - выходим
            if (employee == null && student == null)
            {
                return;
            }
            
            if (student != null)
            {
                var studentPage = new StudentPage();
                studentPage.Show();
            }
            
            else if (employee.RoleId == 1)
            {
                var teacherPage = new TeacherPage();
                teacherPage.Show();
            }
            
            else if (employee.RoleId == 2)
            {
                var departmentHeadPage = new DepartmentHeadPage();
                departmentHeadPage.Show();
            }
            else if (employee.RoleId == 3)
            {
                var engineerPage = new EngineerPage();
                engineerPage.Show();
            }
        }
    }
}