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

        static double StrengthFood, StrengthDrink, Feeling, LevelLimit, MoneyBase, MoneyLevel, FinishBonus;

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            StrengthDrink = (double)numericUpDown2.Value;
            IsOP();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Feeling = (double)numericUpDown3.Value;
            IsOP();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            LevelLimit = (double)numericUpDown4.Value;
            IsOP();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            MoneyBase = (double)numericUpDown5.Value;
            IsOP();
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            MoneyLevel = (double)numericUpDown6.Value;
            IsOP();
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            FinishBonus = (double)numericUpDown7.Value;
            IsOP();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            StrengthFood = (double) numericUpDown1.Value;
            IsOP();
        }

        private void checkBoxIsPlay_CheckedChanged(object sender, EventArgs e)
        {
            isPlay = checkBoxIsPlay.Checked;
            IsOP();
        }

        static bool isPlay;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void IsOP()
        {
            checkBoxOP.Checked = IsOverLoad();
        }

        /// <summary>
        /// 判断这个工作是否超模
        /// </summary>
        /// <param name="work">工作</param>
        /// <returns>是否超模</returns>
        public bool IsOverLoad()
        {//判断这个工作是否超模
            var spend = ((StrengthFood >= 0 ? 1 : -1) * Math.Pow(StrengthFood * 2 + 1, 2) / 6 +
                (StrengthDrink >= 0 ? 1 : -1) * Math.Pow(StrengthDrink * 2 + 1, 2) / 9 +
               (Feeling >= 0 ? 1 : -1) * Math.Pow((isPlay ? -1 : 1) * Feeling * 2 + 1, 2) / 12) *
                (Math.Pow(LevelLimit / 2 + 1, 0.5) / 4 + 1) - 0.5;
            textBox1.Text = spend.ToString();
            if (spend <= 0)
                return true;
            var get = (MoneyBase + MoneyLevel * 10) * (MoneyLevel + 1) * (1 + FinishBonus / 2);
            if (isPlay)
            {
                get /= 12;//经验值换算
            }
            var rel = get / spend;
            textBox2.Text = rel.ToString();
            return rel > 2; 
            // 推荐rel为1.0-1.4之间 超过2.0就是超模
        }
    }
}
