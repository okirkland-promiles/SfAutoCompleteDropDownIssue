using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SfAutoCompleteDropDownIssue
{
    public class SearchItem
    {
        public string? Text { get; set; }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private const int MIN_CHARS = 2;

        // PropertyChanged Handling

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Ctor()

        public MainViewModel()
        {
            searchItems = new ObservableCollection<SearchItem>();
        }

        // Properties

        private ObservableCollection<SearchItem> searchItems;
        public ObservableCollection<SearchItem> SearchItems
        {
            get { return searchItems; }
            set { searchItems = value; OnPropertyChanged(nameof(SearchItems)); }
        }

        private string? searchText;
        public string? SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                DoSearch();
            }
        }

        private bool isSearching = false;
        public bool IsSearching
        {
            get => isSearching;
            set { isSearching = value; OnPropertyChanged(nameof(IsSearching)); }
        }

        // Search

        private void DoSearch()
        {
            if (!string.IsNullOrEmpty(searchText) && searchText.Length >= MIN_CHARS && SearchItems.Count == 0)
            {
                DoMockSearchAsync();
            }
            else if (string.IsNullOrEmpty(searchText) || (searchText.Length < MIN_CHARS && SearchItems.Count > 0))
            {
                SearchItems.Clear();
            }
        }

        private async void DoMockSearchAsync()
        {
            IsSearching = true;

            // simulate web api call
            await Task.Delay(1500);  
            var mockItems = new string[]
            {
                "Item1", "Item2", "Item3", "Item4", "Item5", "Item6", "Item7", "Item8", "Item9", "Item10",
                "Item11", "Item12", "Item13", "Item14", "Item15", "Item16", "Item17", "Item18", "Item19", "Item20"
            };

            // add items to collection
            SearchItems.Clear();
            foreach (string item in mockItems)
            {
                SearchItems.Add(new SearchItem { Text = item });
            }

            IsSearching = false;
        }
    }
}
