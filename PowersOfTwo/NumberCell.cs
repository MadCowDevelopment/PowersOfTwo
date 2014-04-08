namespace PowersOfTwo
{
    public class NumberCell : ViewModel
    {
        private int? _number;

        public int? Number
        {
            get
            {
                return _number;
            }

            set 
            { 
                _number = value;
                OnPropertyChanged();
            }
        }
    }
}
