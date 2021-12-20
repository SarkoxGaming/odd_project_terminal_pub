using App_UI.Commands;
using App_UI.Models;
using App_UI.Services;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace App_UI.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        #region Membres
        private IDataService<Person> peopleDataService;
        MessageBoxDialogService confirmDeleteDialog;
        #endregion

        #region Expression Régulière

        public Regex RePostalCode { get; set; }
        public Regex ReEmail { get; set; }
        public Regex ReProvince { get; set; }
        public Regex RePhoneNumber { get; set; }

        #endregion

        #region Propriétés


        private ObservableCollection<Person> people;

        public ObservableCollection<Person> People
        {
            get { return people; }
            set
            {
                people = value;
                OnPropertyChanged();
            }
        }

        private Person selectedPerson;
        private string message;

        private Person originalPerson;

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                /// On fait une copie de la personne sélectionnée
                /// pour permettre de retourner à la valeur originale
                /// si on veut annuler
                originalPerson = value;
                selectedPerson = (Person)value?.Clone();

                IsSaveable = false;
                IsDirty = false;

                OnPropertyChanged();
                updateProperties();

                DeleteCommand?.RaiseCanExecuteChanged();
            }
        }

        public string FirstName
        {
            get => SelectedPerson?.FirstName;
            set
            {
                SelectedPerson.FirstName = value;
                SelectedPropertyChanged();
            }
        }

        public string LastName
        {
            get => SelectedPerson?.LastName;
            set
            {
                SelectedPerson.LastName = value;
                SelectedPropertyChanged();
            }
        }

        public string City
        {
            get => SelectedPerson?.City;
            set
            {
                SelectedPerson.City = value;
                SelectedPropertyChanged(); ;
            }
        }

        public string Province
        {
            get => SelectedPerson?.Province;
            set
            {
                /// Le CultureInfo sert à adapter la mise en majuscule 
                /// selon la culture de la machine.
                /// Pas obligatoire, mais VS ne chiale plus...
                SelectedPerson.Province = value.ToUpper(CultureInfo.CurrentCulture);
                SelectedPropertyChanged();
            }
        }

        public string PostalCode
        {
            get => SelectedPerson?.PostalCode;
            set
            {
                SelectedPerson.PostalCode = value.ToUpper(CultureInfo.CurrentCulture);
                SelectedPropertyChanged();             
            }
        }

        public string Phone
        {
            get => SelectedPerson?.Phone;
            set
            {
                SelectedPerson.Phone = value;
                SelectedPropertyChanged();
            }
        }

        public string Mobile
        {
            get => SelectedPerson?.Mobile;
            set
            {
                SelectedPerson.Mobile = value;
                SelectedPropertyChanged();

            }
        }

        public string Email
        {
            get { return SelectedPerson?.Email; }
            set
            {
                SelectedPerson.Email = value;
                SelectedPropertyChanged();

            }
        }

        public string BirthDay
        {
            get => SelectedPerson?.BirthDay.ToString();
            set
            {
                SelectedPerson.BirthDay = DateTime.Parse(value);
                SelectedPropertyChanged();
            }
        }

        /// <summary>
        /// Zone de message générique pour afficher du contenu dans l'interface
        /// </summary>
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MessageVisibility));
            }
        }

        public bool MessageVisibility
        {
            get => !string.IsNullOrEmpty(Message);
        }

        private bool isDirty = false;

        /// <summary>
        /// Sert à indiquer que les données ont été modifiées
        /// </summary>
        public bool IsDirty
        {
            get => isDirty;
            private set
            {
                isDirty = value;
                OnPropertyChanged();
            }
        }

        private bool isSaveable = false;

        /// <summary>
        /// Sert à indiquer si l'enregistrement est sauvegardable
        /// </summary>
        public bool IsSaveable
        {
            get => isSaveable;
            private set
            {
                isSaveable = value;

                /// RaiseCanExecuteChanged permet de mettre à jour
                /// les CanXYZ pour l'interface
                SaveCommand?.RaiseCanExecuteChanged();
                CancelCommand?.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }


        #endregion

        #region Commandes
        public DelegateCommand<string> ValidateDataCommand { get; set; }
        public DelegateCommand<string> SaveCommand { get; set; }
        public DelegateCommand<string> DeleteCommand { get; set; }
        public DelegateCommand<string> CancelCommand { get; set; }

        #endregion

        public UsersViewModel(IDataService<Person> _peopleDataService)
        {
            Name = nameof(UsersViewModel);
            peopleDataService = _peopleDataService;
            People = new ObservableCollection<Person>(peopleDataService.GetAll());
            SelectedPerson = People[0];

            /// TODO 03a : Compléter ValidateDataCommand


            ValidateDataCommand = new DelegateCommand<string>(ValidateData, CanValidate);
            SaveCommand = new DelegateCommand<string>(SaveData, CanSave);
            CancelCommand = new DelegateCommand<string>(CancelChange, CanCancel);
            DeleteCommand = new DelegateCommand<string>(DeleteData, CanDelete);

            initRegEx();
        }

        public void SetDeleteDialog(MessageBoxDialogService _confirmDeleteDialog)
        {            
            confirmDeleteDialog = _confirmDeleteDialog;
            
        }

        private void DeleteData(string obj)
        {
            if (SelectedPerson == null) return;

            if (confirmDeleteDialog.ShowDialog() == true)
            {
                People.Remove(originalPerson);
                SelectedPerson = People[0];
            }            
        }

        private bool CanDelete(string obj)
        {
            return SelectedPerson != null;
        }

        private bool CanCancel(string obj)
        {
            return IsSaveable;
        }

        private void CancelChange(string obj)
        {
            SelectedPerson = originalPerson;

            IsSaveable = false;
        }

        private bool CanSave(string obj)
        {
            return IsSaveable && SelectedPerson != null;
        }

        private bool CanValidate(string obj)
        {
            return !isDirty;
        }


        public void UpdateData(IDataService<Person> peopleDataService)
        {
            People = new ObservableCollection<Person>(peopleDataService.GetAll());
            SelectedPerson = People[0];
        }

        public void CreateEmptyUser()
        {
            var p = new Person();

            SelectedPerson = p;
        }

        /// <summary>
        /// TODO 03b : Initilialisation des expressions régulières
        /// </summary>
        private void initRegEx()
        {
            ReProvince = new Regex(@".");
            RePhoneNumber = new Regex(@".");
            RePostalCode = new Regex(@".");

            ReEmail = new Regex(@"([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x22([^\x0d\x22\x5c\x80-\xff]|\x5c[\x00-\x7f])*\x22)(\x2e([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x22([^\x0d\x22\x5c\x80-\xff]|\x5c[\x00-\x7f])*\x22))*\x40([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x5b([^\x0d\x5b-\x5d\x80-\xff]|\x5c[\x00-\x7f])*\x5d)(\x2e([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x5b([^\x0d\x5b-\x5d\x80-\xff]|\x5c[\x00-\x7f])*\x5d))*");
        }

        /// <summary>
        /// Si un éléments n'est pas valide, la propriété Message affiche un texte
        /// pour en aviser l'interface
        /// </summary>
        /// <param name="obj"></param>
        private void ValidateData(string obj)
        {
            Message = "";



            /// TODO 03d : Si c'est un nouvel enregistrement, tout plante. Corrigez la situation.

            
            try
            {
                if (!ReProvince.IsMatch(Province))
                {
                    Message += "Le code postal n'a pas le bon format." + Environment.NewLine;
                }

                if (!RePhoneNumber.IsMatch(Phone))
                {
                    Message += "Le numéro de telephone n'a pas le bon format." + Environment.NewLine;
                }

                if (!RePostalCode.IsMatch(PostalCode))
                {
                    Message += "Le code postal n'a pas le bon format." + Environment.NewLine;
                }

                if (!ReEmail.IsMatch(Email))
                {
                    Message += "Le courriel n'a pas le bon format." + Environment.NewLine;
                }
            } catch(ArgumentNullException e)
            {
                Message += "Nouveau formulaire vide";
            }

            /// TODO 03c : Valider les données de chaque propriété

            

            IsDirty = Message != "";
            IsSaveable = Message == "";
    
        }

        /// <summary>
        /// La méthode met l'information de l'objet sélectionné dans
        /// la collection
        /// Attention! Il ne s'agit de sauvegarder dans un fichier
        /// </summary>
        /// <param name="obj"></param>
        private void SaveData(string obj)
        {
            _ = PeopleDataService.Instance.Update(SelectedPerson);

            if (People.FirstOrDefault(p => p.Id == SelectedPerson.Id) == null)
            {
                People.Add(SelectedPerson);
            }
            
            OnPropertyChanged(nameof(People));
            IsSaveable = false;
        }

        /// <summary>
        /// Wrapper pour OnPropertyChanged pour changer aussi la propriété IsDirty
        /// lorsque la propriété est modifiée. Principalement pour l'enregistrement
        /// sélectionné
        /// </summary>
        /// <param name="propertyName">Nom de la propritété qui a changé</param>
        protected virtual void SelectedPropertyChanged([CallerMemberName] string propertyName = null)
        {
            IsDirty = true;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Mets à jour toutes les propriétés de l'enregistrement sélectionné
        /// </summary>
        private void updateProperties()
        {
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(City));
            OnPropertyChanged(nameof(Province));
            OnPropertyChanged(nameof(PostalCode));
            OnPropertyChanged(nameof(Phone));
            OnPropertyChanged(nameof(Mobile));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(BirthDay));
        }
    }
}
