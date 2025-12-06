using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using University.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Pages;

public partial class ViewDisciplinesHeadPage : UserControl
{
    private List<Subject> _allSubjects = new();

    public ViewDisciplinesHeadPage()
    {
        InitializeComponent();
        LoadSubjects();
    }

    private void LoadSubjects()
    {
        _allSubjects = App.DbContext.Subjects
            .Include(s => s.ExecutorDept)
            .ToList();
        DisciplinesDataGrid.ItemsSource = _allSubjects;
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        App.DbContext.SaveChanges();
    }
}