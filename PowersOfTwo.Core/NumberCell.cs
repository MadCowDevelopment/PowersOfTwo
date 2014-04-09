namespace PowersOfTwo.Core
{
    public class NumberCell
    {
        #region Fields

        private int? _number;

        #endregion Fields

        #region Public Properties

        public int? Number
        {
            get
            {
                return _number;
            }

            set
            {
                _number = value;
            }
        }

        #endregion Public Properties
    }
}