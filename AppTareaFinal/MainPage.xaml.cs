//using AndroidX.Lifecycle;
using AppTareaFinal.ViewsModels;

namespace AppTareaFinal
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel ViewModel { get; set; }

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            ViewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Este método se llama cada vez que la página se muestra
            ViewModel.getAll();
        }
    }

}
