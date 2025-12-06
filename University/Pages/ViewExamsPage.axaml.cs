using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using University.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Pages;

public partial class ViewExamsPage : UserControl
{
    private List<Exam> _allExams = new();

    public ViewExamsPage()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        _allExams = App.DbContext.Exams
            .Include(e => e.Subject)
            .Include(e => e.Student)
            .Include(e => e.Teacher)
            .ToList();
        
        var auditoriums = _allExams
            .Where(e => e.Auditorium != null)
            .Select(e => e.Auditorium)
            .Distinct()
            .OrderBy(a => a)
            .ToList();

        AuditoriumFilter.ItemsSource = auditoriums;

        ApplyFilters();
    }

    private void ApplyFilters()
    {
        string search = SearchBox.Text?.ToLower().Trim() ?? "";
        string selectedAud = AuditoriumFilter.SelectedItem as string;

        var filtered = _allExams.Where(e =>
        {
            bool matchesSearch = string.IsNullOrEmpty(search) ||
                                 e.Subject?.Name?.ToLower().Contains(search) == true;

            bool matchesAud = string.IsNullOrEmpty(selectedAud) ||
                              e.Auditorium == selectedAud;

            return matchesSearch && matchesAud;
        }).ToList();

        ExamsDataGrid.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilters();
    }

    private void AuditoriumFilter_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ApplyFilters();
    }
}