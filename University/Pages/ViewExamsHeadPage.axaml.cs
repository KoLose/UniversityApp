using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using University.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Pages;

public partial class ViewExamsHeadPage : UserControl
{
    private List<Exam> _allExams = new();

    public ViewExamsHeadPage()
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

    private async void AddExamButton_Click(object? sender, RoutedEventArgs e)
    {
        var parent = this.VisualRoot as Window;
        if (parent == null) return;
        var win = new AddExamWindow();
        await win.ShowDialog(parent);
        LoadExams();
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        App.DbContext.SaveChanges();
    }

    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        var btn = sender as Button;
        var exam = btn?.DataContext as Exam;
        if (exam == null) return;

        App.DbContext.Exams.Remove(exam);
        App.DbContext.SaveChanges();
        LoadExams();
    }
}