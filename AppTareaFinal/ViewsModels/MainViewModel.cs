using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using AppTareaFinal.DataAccess;
using AppTareaFinal.DTOs;
using AppTareaFinal.Utils;
using AppTareaFinal.Models;
using System.Collections.ObjectModel;
using AppTareaFinal.Views;

namespace AppTareaFinal.ViewsModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly TareaDbContext _dbContext;
        [ObservableProperty]
        private ObservableCollection<TareaDTO> listaTareas = new ObservableCollection<TareaDTO>();

        public MainViewModel(TareaDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await getAll()));

        }

        public async Task getAll()
        {
            var lista = await _dbContext.Tareas.ToListAsync();
          
            if (lista.Any())
            {
                listaTareas.Clear();
                foreach (var item in lista)
                {
                    listaTareas.Add(new TareaDTO
                    {
                        IdTarea = item.IdTarea,
                        NombreTarea = item.NombreTarea
                    });
                }
            }
            
        }


        [RelayCommand]
        private async Task Create()
        {
            var uri = $"{nameof(TareaPage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Edit(TareaDTO tareaDto)
        {
            var uri = $"{nameof(TareaPage)}?id={tareaDto.idTarea}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Delete(TareaDTO tareaDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar la tarea?", "Si", "No");

            if (answer)
            {
                var tarea = await _dbContext.Tareas
                    .FirstAsync(e => e.IdTarea == tareaDto.idTarea);

                _dbContext.Tareas.Remove(tarea);
                await _dbContext.SaveChangesAsync();
                ListaTareas.Remove(tareaDto);


            }

        }



    }
}
