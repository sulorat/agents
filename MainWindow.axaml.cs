using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.Helpers;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    public class Agent
    {
        public string Name { get; set; }
        public string Sales { get; set; }
        public string Discount { get; set; }
        public int DiscountAsInt { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AgentType { get; set; }
        public string Adress { get; set; } = "uiu";
        public string AgentINN { get; set; }= "klljj";
        public string AgentKPP { get; set; }= "tert";
        public string AgentsDirectorName { get; set; }= "fghsfgh";
        
        public Bitmap AgentPhoto { get; set; }
        public string Priority { get; set; }
        public int PriorityAsInt{ get; set; }
        public IBrush AgentColor
        {
            get => DiscountAsInt > 25 ? Brushes.LawnGreen : null;
        }
        
        public Agent() { }
        
        public Agent(string name, int sales,  string email, string discount, string phone, string agentType, int priority)
        {
            Name = name;
            Discount = (string.Format("{0}%",discount));
            DiscountAsInt = int.Parse(discount);
            Sales = string.Format("{0} продаж за год", sales);
            Phone = phone;
            AgentType = agentType;
            Email = email;
            AgentPhoto = ImageHelper.Load(new Uri("avares://AvaloniaApplication1/Assets/picture_заглушка.png"));
            Priority = string.Format("Приоритетность: {0}",priority);
            PriorityAsInt = priority;
        }
    }

    private string _searchText;
    public static string[] Types =  {"Первый","Второй","Третий" };
    public string[] filterValues = { "Все","Первый","Второй","Третий" };
    public string[] SortValues = { "Все","Наименование по возр.","Наименование по убыв.","Скидка по возр.","Скидка по убыв.", "Приоритет по возр.", "Приоритет по убыв." };
    public ObservableCollection<Agent> displayAgents;
    private int SortIndex;
    private int filterIndex;
    private double pageCount;
    private int CurrentPage = 1;
    private Agent? selectedItem;
    private List<Agent> selectedItems = new();
    private int maxPriorityAgent;

    public List<Agent> _agents = new()
    {
        new Agent("Serega",12, "legend@email","12", "876569706",Types[0],10),
        new Agent("Misha",11,"mishanya@email","10" ,"765865900",Types[1],10),
        new Agent("Dyadya",11,"neveroyatniy@email","11" ,"56456456",Types[0],10),
        new Agent("Ivan",11,"dadaya@email","13" ,"90897976",Types[1],10),
        new Agent("Ya",11,"aktoeshe@email","17" ,"8768969686",Types[2],10),
        new Agent("Dimon",11,"ichto@email","20" ,"675746575",Types[2],10),
        new Agent("Seva",11,"nu_da@email","19" ,"98797897",Types[1],10),
        new Agent("Vlad",11,"haha@email","27" ,"7967867856",Types[1],10),
        new Agent("Sashka",11,"hehe@email","30" ,"687864",Types[0],10),
        new Agent("Alik",11,"hihi@email","36" ,"54765757",Types[1],10),
        new Agent("Egor",11,"hyhy@email","40" ,"57575464",Types[0],10),
        new Agent("Ti",11,"huhu@email","56" ,"345345345",Types[1],10),
        new Agent("Nikto",16,"hoho@email","65","9890908908",Types[2],10)
    };
    public MainWindow()
    {
        
        InitializeComponent();

        displayAgents = new ObservableCollection<Agent>(_agents);
        
        FilterComboBox.ItemsSource = filterValues;
        SortComboBox.ItemsSource = SortValues;
        AgentsListBox.ItemsSource = displayAgents;
        DisplayAgents();
    }

    public void DisplayAgents()
    {
        
        var displayAgentsList = new List<Agent>(_agents);
        
        if (SortIndex != 0)
        {
            switch (SortIndex)
            {
                case 1:
                    displayAgentsList=displayAgentsList.OrderBy(agent => agent.Name).ToList();
                    break;
                case 2:
                    displayAgentsList = displayAgentsList.OrderByDescending(agent => agent.Name).ToList();
                    break;
                case 3:
                    displayAgentsList = displayAgentsList.OrderBy(agent => agent.DiscountAsInt).ToList();
                    break;
                case 4:
                    displayAgentsList = displayAgentsList.OrderByDescending(agent => agent.DiscountAsInt).ToList();
                    break;
                case 5:
                    displayAgentsList = displayAgentsList.OrderBy(agent => agent.Priority).ToList();
                    break;
                case 6:
                    displayAgentsList = displayAgentsList.OrderByDescending(agent => agent.Priority).ToList();
                    break;
                    
            }
        }   
        if(filterIndex!=0)
        {
            switch (filterIndex)
            {
                case 1:
                    displayAgentsList = displayAgentsList.Where(agent => agent.AgentType == "Первый").ToList();
                    break;
                case 2:
                    displayAgentsList = displayAgentsList.Where(agent => agent.AgentType == "Второй").ToList();
                    break;
                case 3:
                    displayAgentsList = displayAgentsList.Where(agent => agent.AgentType == "Третий").ToList();
                    break;
            }
        }
        if (!string.IsNullOrEmpty(_searchText))
        {
            displayAgentsList = displayAgentsList.Where(agent =>
                agent.Name.Contains(_searchText) || agent.Phone.Contains(_searchText) ||
                agent.Email.Contains(_searchText)).ToList();
        }
        pageCount = Math.Ceiling((double)displayAgentsList.Count/10);
        if (displayAgentsList.Count > 10)
        {
            displayAgentsList = displayAgentsList.Skip(10 * (CurrentPage - 1)).Take(10).ToList();
        }
        
        if (displayAgents.Count!=0 && displayAgents !=null)
        {
            displayAgents.Clear();
        }
        foreach (var agent in displayAgentsList)
        {
            displayAgents.Add(agent);
        }

        PagesCountTextBlock.Text = string.Format("Стараница {0} из {1}",CurrentPage,pageCount);
    }


    private void SearchAgents(object? sender, TextChangedEventArgs e)
    {
        _searchText = (sender as TextBox).Text;
        DisplayAgents();
    }

    private void SortSelectionChandged(object? sender, SelectionChangedEventArgs e)
    {
        SortIndex = (sender as ComboBox).SelectedIndex;
        DisplayAgents();
    }

    private void FilterSelectionChandged(object? sender, SelectionChangedEventArgs e)
    {
        filterIndex = (sender as ComboBox).SelectedIndex;
        DisplayAgents();
    }

    private void NextPage(object? sender, RoutedEventArgs e)
    {
        if (CurrentPage < pageCount)
        {
            CurrentPage++;
            DisplayAgents();
        }
    }
    private void PreviousPage(object? sender, RoutedEventArgs e)
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            DisplayAgents();
        }
    }

    private void SelectionListBox(object? sender, SelectionChangedEventArgs e)
    {
        selectedItems = (sender as ListBox).SelectedItems.OfType<Agent>().ToList();
        selectedItem = (Agent)(sender as ListBox).SelectedItem;
        EditButton.IsVisible = true;
        if (AgentsListBox.SelectedItems.Count > 1)
        {
            maxPriorityAgent = selectedItems.Max(agent => agent.PriorityAsInt);
            ChangePriorityButoon.IsVisible = true;
            EditButton.IsVisible = false;
        }
        else
        {
            ChangePriorityButoon.IsVisible = false;
        }
    }

    private async void ChangePriorityButtonClick(object? sender, RoutedEventArgs e)
    {
        var dialog  = new PriorityChangeWindow(maxPriorityAgent);
        var result = await dialog.ShowDialog<int>(this);
        foreach (var agent in selectedItems)
        {
            if (result != 0)
            {
                var agentsInList = _agents.Find(x => x.Phone == agent.Phone);
                agentsInList.PriorityAsInt = result;
                agentsInList.Priority = ($"Приоритетность: {agentsInList.PriorityAsInt}");
            }
        }
        
        DisplayAgents();
    }

    private async void EditButtonClick(object? sender, RoutedEventArgs e)
    {
        var dialog = new AddOrEditWindow(selectedItem, true);
        var agent = await dialog.ShowDialog<bool>(this);
        if (agent)
        {
            _agents.Remove(selectedItem);
        }
        DisplayAgents();
    }
}