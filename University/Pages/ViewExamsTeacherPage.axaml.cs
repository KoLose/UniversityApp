using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using University.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Pages;

public partial class ViewExamsTeacherPage : UserControl
{
    private List<Exam> _allExams = new();

    public ViewExamsTeacherPage()
    {
        InitializeComponent();
        LoadExams();
    }

    private void LoadExams()
    {
        _allExams = App.DbContext.Exams
            .Include(e => e.Subject)
            .Include(e => e.Student)
            .Include(e => e.Teacher)
            .ToList();

        ApplyFilter();
    }

    private void ApplyFilter()
    {
        string search = SearchBox.Text?.ToLower().Trim() ?? "";
        var filtered = _allExams.Where(e =>
            string.IsNullOrEmpty(search) ||
            e.Student?.FullName?.ToLower().Contains(search) == true
        ).ToList();

        ExamsDataGrid.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilter();
    }

    private void ExamsDataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
    }

    //  Добавление
    private async void AddExamButton_Click(object? sender, RoutedEventArgs e)
    {
        var parent = this.VisualRoot as Window;
        if (parent == null) return;

        var addWindow = new AddExamWindow();
        await addWindow.ShowDialog(parent);
        LoadExams();
    }

    //  Сохранение (только своих)
    private async void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            int currentTeacherId = VariableData.SelectedEmployee?.EmployeeId ?? throw new InvalidOperationException("Преподаватель не авторизован");

            // Отменяем изменения в чужих записях
            foreach (var entry in App.DbContext.ChangeTracker.Entries<Exam>())
            {
                if (entry.State == EntityState.Modified && entry.Entity.TeacherId != currentTeacherId)
                {
                    entry.State = EntityState.Unchanged;
                }
            }

            App.DbContext.SaveChanges();
        }
        catch (Exception ex)
        {
        }
    }
    
    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var exam = button?.DataContext as Exam;
        if (exam == null) return;
        
            App.DbContext.Exams.Remove(exam);
            App.DbContext.SaveChanges();
            LoadExams();
        
    }
}