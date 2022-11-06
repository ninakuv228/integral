using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpfintegral
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            this.Title = "Example 2";
            this.Points = new ObservableCollection<DataPoint>();
        }

        public string Title { get; private set; }

        public ObservableCollection<DataPoint> Points { get; set; }
    }
}
