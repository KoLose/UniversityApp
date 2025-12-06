using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using University.Data;
using System.Linq;

namespace University.Pages;

public partial class AddExamWindow : Window
{
    public AddExamWindow()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        // Преподаватели (только сотрудники с ролью = Teacher, но можно упростить)
        var teachers = App.DbContext.Employees
            .Where(e => e.RoleId == 1) // ← если 1 = Teacher
            .Select(e => new { e.EmployeeId, e.FullName })
            .ToList();
        TeacherCombo.ItemsSource = teachers;
        TeacherCombo.DisplayMemberBinding = new Avalonia.Data.Binding("FullName");

        // Студенты
        var students = App.DbContext.Students
            .Select(s => new { s.StudentId, s.FullName })
            .ToList();
        StudentCombo.ItemsSource = students;
        StudentCombo.DisplayMemberBinding = new Avalonia.Data.Binding("FullName");

        // Дисциплины
        var subjects = App.DbContext.Subjects
            .Select(s => new { s.SubjectId, s.Name })
            .ToList();
        SubjectCombo.ItemsSource = subjects;
        SubjectCombo.DisplayMemberBinding = new Avalonia.Data.Binding("Name");

        // Оценки
        GradeCombo.ItemsSource = new[] { 2, 3, 4, 5 };
    }

    private void CreateButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Проверка заполнения полей
        if (TeacherCombo.SelectedItem == null)
        {
            return;
        }
        if (StudentCombo.SelectedItem == null)
        {
            return;
        }
        if (SubjectCombo.SelectedItem == null)
        {
            return;
        }
        if (string.IsNullOrWhiteSpace(AuditoriumBox.Text))
        {
            return;
        }
        if (GradeCombo.SelectedItem == null)
        {
            return;
        }

        // Получаем значения
        var teacher = (dynamic)TeacherCombo.SelectedItem;
        var student = (dynamic)StudentCombo.SelectedItem;
        var subject = (dynamic)SubjectCombo.SelectedItem;
        var auditorium = AuditoriumBox.Text.Trim();
        var grade = (int)GradeCombo.SelectedItem;

        // 🔒 Проверка длины аудитории
        if (auditorium.Length > 20)
        {
            return;
        }
  
    
        var exam = new Exam
        {
            ExamDate = DateOnly.FromDateTime(DateTime.Today),
            TeacherId = teacher.EmployeeId,
            StudentId = student.StudentId,
            SubjectId = subject.SubjectId,
            Auditorium = auditorium,
            Grade = grade
        };

        App.DbContext.Exams.Add(exam);
        App.DbContext.SaveChanges();

        Close();
    }

    private void CancelButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }
}