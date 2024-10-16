using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;

namespace AvaloniaApplication1;

public partial class AddOrEditWindow : Window
{
    public MainWindow.Agent _Agent;
    public AddOrEditWindow()
    {
        InitializeComponent();
        SaveOrEditButton.Content = "Добавить";
        SelectImageButton.Content = "Добавить фото";
    }
    
    public AddOrEditWindow(MainWindow.Agent? agent, bool isEditing)
    {
        InitializeComponent();
        SelectImageButton.Content = "Изменить фото";
        RemoveButton.IsVisible = true;
        _Agent = agent;
        SaveOrEditButton.Content = "Сохранить";
        AgentName.Text = agent.Name;
        AgentType.Text = agent.AgentType;
        AgentPriority.Text = agent.PriorityAsInt.ToString();
        AgentAdress.Text = agent.Adress;
        AgentINN.Text = agent.AgentINN;
        AgentKPP.Text = agent.AgentKPP;
        AgentsDirectorName.Text = agent.AgentsDirectorName;
        AgentPhone.Text = agent.Phone;
        AgentEmail.Text = agent.Email;
        AgentImage.Source = agent.AgentPhoto;
    }

    private void SaveOrEditButtonClick(object? sender, RoutedEventArgs e)
    {
        _Agent.Name = AgentName.Text;
        _Agent.AgentType = AgentType.Text;
        _Agent.Adress = AgentAdress.Text;
        _Agent.PriorityAsInt = int.Parse(AgentPriority.Text);
        _Agent.AgentINN = AgentINN.Text;
        _Agent.AgentKPP = AgentKPP.Text;
        _Agent.AgentsDirectorName = AgentsDirectorName.Text;
        _Agent.Phone = AgentPhone.Text;
        _Agent.Email = AgentEmail.Text;
        Close(false);
    }

    private void RemoveButtonClick(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }

    private async void SelectImageButton_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Choose Product Image",
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Image Files", Extensions = { "png", "jpg", "jpeg" } }
            }
        };
        string[] result = await dialog.ShowAsync(this);

        if (result != null && result.Length > 0)
        {
            _Agent.AgentPhoto = new Bitmap(result[0]);
        }
    }
}