using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TiposDeNubes.Models;


namespace TiposDeNubes.ViewModels
{
    public class NubeViewModel:INotifyPropertyChanged
    {
        //son las acciones que se utilizan en las vistas
        
        public NubeModel Nube { get; set; }
        public string Error { get; set; } = "";
        public ICommand VerRegistrarCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand VerEliminarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand RegresarCommand { get; set; }
        public ICommand VerEditarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public string Vista { get; set; } = "Ver";
       
        public NubeCRUD NubeCRUD { get; set; } = new NubeCRUD();
        public NubeViewModel()
        {
            VerRegistrarCommand = new RelayCommand(VerRegistrar);
            RegistrarCommand = new RelayCommand(Registrar);
            VerEliminarCommand = new RelayCommand<NubeModel>(VerEliminar);
            EliminarCommand = new RelayCommand(Eliminar);
            RegresarCommand = new RelayCommand(Regresar);
            VerEditarCommand = new RelayCommand(VerEditar);
            EditarCommand = new RelayCommand(Editar);
        }

        private void Editar()
        {
           
            NubeCRUD.Editar(Nube);
            Vista = "VerNubes";
            Actualizar();
            Regresar();
        }

        private void VerEditar()
        {
            NubeModel copia = new NubeModel()
            {
                Id = Nube.Id
            };
            //verificar que un objeto esté seleccionado
            if (Nube != null)
            {
                Vista = "Editar";
                Actualizar();
            }
        }
        private void Registrar()
        {
            if (Nube != null)
            {
                Error = "";
                NubeCRUD.Agregar(Nube);
                Vista = "VerNubes";
                Actualizar();
                //if(NubeModel.Validar)

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void VerRegistrar()
        {
            Vista = "Agregar";
            Nube = new();
           
            Actualizar();
        }
        public void Eliminar()
        {
            NubeCRUD.Eliminar(Nube);
            Vista = "VerNubes";
            Nube = null;
            Actualizar();

        }
        public void VerEliminar(NubeModel nube)
        {
            Nube = nube;
            if (Nube != null)
            {
                Vista = "Eliminar";
                Actualizar();
            }
        }
        public void Regresar()
        {
            Vista = "VerNubes";
            Nube = null;
            Actualizar();
        }
        public void Actualizar()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

     

    }
}
