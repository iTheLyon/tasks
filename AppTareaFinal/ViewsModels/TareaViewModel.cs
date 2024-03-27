using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using AppTareaFinal.DataAccess;
using AppTareaFinal.DTOs;
using AppTareaFinal.Utils;
using AppTareaFinal.Models;


namespace AppTareaFinal.ViewsModels
{
    public partial class TareaViewModel : ObservableObject , IQueryAttributable
    {
        private readonly TareaDbContext localContext;

        [ObservableProperty]
        private TareaDTO tareaDto = new TareaDTO();

        [ObservableProperty]
        private string pageTitle;

        private int IdTarea;

        [ObservableProperty]
        private bool loadingIsVisible = false;

        public TareaViewModel(TareaDbContext context)
        {
            localContext = context;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            IdTarea = id;
            if(IdTarea == 0)
            {
                pageTitle = "Nueva Tarea";
            }
            else
            {
                pageTitle = "Editar Tarea";
                LoadingIsVisible = true;
                await Task.Run(async () =>
                {
                    var obj = await localContext.Tareas.FirstAsync(t => t.IdTarea == IdTarea);
                    TareaDto.IdTarea = obj.IdTarea;
                    TareaDto.NombreTarea = obj.NombreTarea;
                    MainThread.BeginInvokeOnMainThread(() => { LoadingIsVisible = false; });
                });
            }            
        }

     
        [RelayCommand]
        private async Task save()
        {
            LoadingIsVisible = true;
        

            await Task.Run(async () =>
            {
                if (IdTarea == 0)
                {
                    var tbTarea= new Tarea
                    {
                        NombreTarea = TareaDto.NombreTarea,                    
                    };

                    localContext.Tareas.Add(tbTarea);
                    await localContext.SaveChangesAsync();

                    TareaDto.IdTarea = tbTarea.IdTarea;
               

                }
                else
                {
                    var found = await localContext.Tareas.FirstAsync(e => e.IdTarea == IdTarea);
                    found.NombreTarea = TareaDto.NombreTarea;                    

                    await localContext.SaveChangesAsync();

                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingIsVisible = false;
               
                    await Shell.Current.Navigation.PopAsync();
                });

            });
        }
    }

}
