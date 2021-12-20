using App_UI.Commands;
using App_UI.Services;
using App_UI.ViewModels;
using System.Windows;

namespace App_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ApplicationView : Window
    {
        ApplicationViewModel vm;


        public ApplicationView()
        {
            InitializeComponent();

            /// Initialisation des boîtes de dialogue
            /// 
            FileDialogService openFileDialog = new FileDialogService(true);
            FileDialogService saveFileDialog = new FileDialogService(false);

            /// Configuration de la boîte de dialogue pour confirmer la suppression
            MessageBoxDialogService confirmDeleteDialog = new MessageBoxDialogService();

            confirmDeleteDialog.Caption = "Avertissement!";
            confirmDeleteDialog.Message = "Êtes-vous certain de vouloir supprimer l'enregistrement?";
            confirmDeleteDialog.Buttons = MessageBoxButton.YesNo;

            /// Injection des services
            vm = new ApplicationViewModel(openFileDialog, saveFileDialog, confirmDeleteDialog);

            vm.RestartCommand = new DelegateCommand<string>(Restart);

            DataContext = vm;
        }

        private void Restart(string obj)
        {
            var toWhichLanguage = App_UI.Properties.Settings.Default.Language;

            string content;

            if (toWhichLanguage == "fr")
            {
                content = "Pour appliquer les changements, il faut redémarrer l'application.";
            }
            else if (toWhichLanguage == "en")
            {
                content = "To apply the changes, you must restart the application.";
            }
            else
            {
                content = "error language";
            }

            var result = MessageBox.Show(content, "Message", MessageBoxButton.OKCancel);


            if (result == MessageBoxResult.OK)
            {
                //
            }
        }
    }
}
