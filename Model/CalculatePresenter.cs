namespace Instruments4.Model
{
    internal class CalculatePresenter
    {
        private DataGridView _destinate;
        private DataGridView _source;
        private List<int[]> _info;
        private int _step;

        public CalculatePresenter(DataGridView destinate, DataGridView source, List<int[]> info, int step)
        {
            _destinate = destinate;
            _source = source;
            _info = info;
            _step = step;
        }

        public int LastRowIndex => _destinate.Columns.Count - 1;

        public void Work()
        {
            FillInvestitions();
            FillSum();
            OrderByDescending();
        }

        private void FillInvestitions()
        {
            _destinate.Rows.Clear();
            for (int i = 0; i < _info.Count; i++)
            {
                _destinate.Rows.Add();

                for (int j = 0; j < _destinate.Columns.Count - 1; j++)
                {
                    _destinate[j, i].Value = _info[i][j];
                }
            }
        }

        private void FillSum()
        {
            int lastColumnIndex = _destinate.Columns.Count - 1;
            for (int i = 0; i < _destinate.Rows.Count; i++)
            {
                _destinate[lastColumnIndex, i].Value = GetSum(i);
            }
        }

        private int GetSum(int rowIndex)
        {
            int result = 0;
            for (int i = 0; i < _destinate.Columns.Count; i++)
            {
                var currentCellValue = Convert.ToInt32(_destinate[i, rowIndex].Value);
                if (currentCellValue != 0)
                {
                    var row = currentCellValue / _step - 1;
                    result += Convert.ToInt32(_source[i, row].Value);
                }
            }

            return result;
        }

        private void OrderByDescending()
        {
            _destinate.Sort(_destinate.Columns[LastRowIndex], System.ComponentModel.ListSortDirection.Descending);
        }
    }
}
