using System.Collections.Generic;
using System.Collections.ObjectModel;
using VSFGUI_Ava.Models;

namespace VSFGUI_Ava.ViewModels;

public class TestViewModel : ViewModelBase
{
    
    public TestViewModel(IEnumerable<OscInput> items)
    {
        Items = new ObservableCollection<OscInput>(items);
    }

    public ObservableCollection<OscInput> Items { get; }
}