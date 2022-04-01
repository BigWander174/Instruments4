namespace Instruments4.Model
{
    internal class Calculation
    {
        private double _budget;
        private int[] _elements;
        private List<int[]> _variants = new List<int[]>();
        private int[] _current;
        private int _step;

        public List<int[]> Variants => _variants;
        public void Work(string budget, DataGridView dataGridView, int step)
        {
            _budget = Convert.ToDouble(budget);
            _step = step;
            SetCurrent(dataGridView.Columns.Count);
            CreateElements(dataGridView);
            CreateVariants();
        }

        private void SetCurrent(int length)
        {
            _current = new int[length];
            for (int i = 0; i < _current.Length; i++)
            {
                _current[i] = -1;
            }
        }

        private void CreateElements(DataGridView dataGridView)
        {
            _elements = new int[dataGridView.Rows.Count + 1];

            _elements[0] = 0;
            for (int i = 1; i < _elements.Length; i++)
            {
                _elements[i] = _step * i;
            }
        }

        private void CreateVariants()
        {
            for (int i = 0; i < _current.Length; i++)
            {
                if (_current[i] == -1)
                {
                    foreach (var element in _elements)
                    {
                        if (CanPut(element))
                        {
                            _current[i] = element;
                            CreateVariants();
                            _current[i] = -1;
                        }
                    }

                    return;
                }
            }

            if (_current.Sum() == _budget)
            {
                int[] result = new int[_current.Length];
                Array.Copy(_current, result, _current.Length);
                _variants.Add(result);
            }
            
        }

        private bool CanPut(int element)
        {
            return _current.Sum() + element <= _budget;
        }
    }
}
