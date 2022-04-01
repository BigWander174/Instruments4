using Instruments4.Model;

namespace Instruments4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var box = (NumericUpDown)sender;
            mainView.ColumnCount = (int)box.Value;
            calculateView.ColumnCount = (int)box.Value + 1;
            SetColumnHeaders();
        }

        private void SetColumnHeaders()
        {
            for (int i = 0; i < mainView.Columns.Count; i++)
            {
                mainView.Columns[i].HeaderCell.Value = $"F{i + 1}(x)";
                calculateView.Columns[i].HeaderCell.Value = $"F{i + 1}(x)";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            int step = (int) numericUpDown2.Value;

            try
            {
                var value = Convert.ToInt32(box.Text);
                mainView.RowCount = value / step;
            }
            catch (ArgumentOutOfRangeException)
            {
                mainView.RowCount = 1;
            }
            catch (FormatException)
            {
                mainView.RowCount = 1;
            }

            catch (DivideByZeroException)
            {
                mainView.RowCount = 1;
            }

            SetRowHeaders(step);
        }

        private void SetRowHeaders(int step)
        {
            for (int i = 0; i < mainView.Rows.Count; i++)
            {
                mainView.Rows[i].HeaderCell.Value = Convert.ToString(step * (i + 1));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var step = (int)numericUpDown2.Value;
            var calculation = new Calculation();
            calculation.Work(budgetBox.Text, mainView, step);
            var filler = new CalculatePresenter(calculateView, mainView, calculation.Variants, step);
            filler.Work();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int step = (int)numericUpDown2.Value;

            try
            {
                var value = Convert.ToInt32(budgetBox.Text);
                mainView.RowCount = value / step;
            }
            catch (ArgumentOutOfRangeException)
            {
                mainView.RowCount = 1;
            }
            catch (FormatException)
            {
                mainView.RowCount = 1;
            }

            catch (DivideByZeroException)
            {
                mainView.RowCount = 1;
            }

            SetRowHeaders(step);
        }
    }
}