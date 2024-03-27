using AppTareaFinal.ViewsModels;
namespace AppTareaFinal.Views;

public partial class TareaPage : ContentPage
{
	public TareaPage(TareaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}