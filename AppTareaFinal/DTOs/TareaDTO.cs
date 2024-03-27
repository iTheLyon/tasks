using CommunityToolkit.Mvvm.ComponentModel;

namespace AppTareaFinal.DTOs
{
    public partial class TareaDTO : ObservableObject
    {
        [ObservableProperty]
        public int idTarea;
        [ObservableProperty]
        public string nombreTarea;
    }
}
