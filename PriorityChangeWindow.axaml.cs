using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication1;

public partial class PriorityChangeWindow : Window
{
    public PriorityChangeWindow()
    {
        InitializeComponent();
    }
    public PriorityChangeWindow(int maxPriorityAgent)
    {
        InitializeComponent();
        PriorityTextBox.Text = maxPriorityAgent.ToString();
    }

    private void SaveButtonClick(object? sender, RoutedEventArgs e)
    {
        int newPriority = int.Parse(PriorityTextBox.Text);
        this.Close(newPriority);
    }
}