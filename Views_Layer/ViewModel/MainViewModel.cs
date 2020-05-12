namespace ViewLayer.ViewModel
{
    class MainViewModel : BaseViewModel
    {
       // private readonly UsersService _userService = new UsersService();

        
        
        private string _someText = "Kutasino";



        public string SomeText
        {
            get => _someText;
            set
            {
                _someText = value;
                OnPropertyChanged("SomeText");
            }
        }
    }
}
