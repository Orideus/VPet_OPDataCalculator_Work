using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 虚拟桌宠模拟器_超模计算器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // _W：工作 _F：食物
        static double StrengthFood_W, StrengthDrink_W, Feeling_W, LevelLimit_W, MoneyBase_W, MoneyLevel_W, FinishBonus_W;
        static WorkType workType;
        static double Exp_F, Strength_F, StrengthDrink_F, StrengthFood_F, Feeling_F, Health_F, Likability_F, Price_F;

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            StrengthDrink_W = (double)numericUpDown2.Value;
            IsOP_W();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Feeling_W = (double)numericUpDown3.Value;
            IsOP_W();
        }

        private void numericUpDown15_ValueChanged(object sender, EventArgs e)
        {
            Price_F = (double)numericUpDown15.Value;
            IsOP_F();
        }

        private void numericUpDown13_ValueChanged(object sender, EventArgs e)
        {
            Strength_F = (double)numericUpDown13.Value;
            IsOP_F();
        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {
            StrengthDrink_F = (double)numericUpDown12.Value;
            IsOP_F();
        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {
            StrengthFood_F = (double)numericUpDown11.Value;
            IsOP_F();
        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            Feeling_F = (double)numericUpDown10.Value;
            IsOP_F();
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            Health_F = (double)numericUpDown9.Value;
            IsOP_F();
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            Likability_F = (double)numericUpDown8.Value;
            IsOP_F();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            LevelLimit_W = (double)numericUpDown4.Value;
            IsOP_W();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            MoneyBase_W = (double)numericUpDown5.Value;
            IsOP_W();
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            MoneyLevel_W = (double)numericUpDown6.Value;
            IsOP_W();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    workType = WorkType.Study;
                    break;
                case 1:
                    workType = WorkType.Play;
                    break;
                default:
                    workType = WorkType.Work;
                    break;
            }
            IsOP_W();
        }

        private void numericUpDown14_ValueChanged(object sender, EventArgs e)
        {
            Exp_F = (double)numericUpDown14.Value;
            IsOP_F();
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            FinishBonus_W = (double)numericUpDown7.Value;
            IsOP_W();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            StrengthFood_W = (double)numericUpDown1.Value;
            IsOP_W();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 2;
        }

        public void IsOP_W()
        {
            checkBoxOP_W.Checked = IsOverLoad_W();
        }
        public void IsOP_F()
        {
            checkBoxOP_F.Checked = IsOverLoad_F();
        }


        public enum WorkType { Work, Study, Play }

        /// <summary>
        /// 判断这个工作是否超模
        /// </summary>
        /// <param name="work">工作</param>
        /// <returns>是否超模</returns>
        public bool IsOverLoad_W()
        {//判断这个工作是否超模
            var spend = ((StrengthFood_W >= 0 ? 1 : -1) * Math.Pow(StrengthFood_W * 2 + 1, 2) / 6 +
                (StrengthDrink_W >= 0 ? 1 : -1) * Math.Pow(StrengthDrink_W * 2 + 1, 2) / 9 +
               (Feeling_W >= 0 ? 1 : -1) * Math.Pow((workType == WorkType.Play ? -1 : 1) * Feeling_W * 2 + 1, 2) / 12) *
                (Math.Pow(LevelLimit_W / 2 + 1, 0.5) / 4 + 1) - 0.5;
            textBox1.Text = spend.ToString();
            if (spend <= 0)
                return true;
            var get = (MoneyBase_W + MoneyLevel_W * 10) * (MoneyLevel_W + 1) * (1 + FinishBonus_W / 2);
            if (workType != WorkType.Work)
            {
                get /= 12;//经验值换算
            }
            var rel = get / spend;
            textBox2.Text = rel.ToString();
            return rel > 2;
            // 推荐rel为1.0-1.4之间 超过2.0就是超模
        }

        public bool IsOverLoad_F()
        {
            double ReasonablePrice = ((Exp_F / 3 + Strength_F / 5 + StrengthDrink_F / 3 + StrengthFood_F / 2 + Feeling_F / 5) / 3 + Health_F + Likability_F * 10);
            textBoxReasonblePriceFood.Text = ReasonablePrice.ToString();
            return Price_F < ((ReasonablePrice - 10) * 0.7);
        }
    }
}
