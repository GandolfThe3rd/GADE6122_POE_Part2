using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hero_Adventure
{
    public partial class Form1 : Form
    {
        private GameEngine engine;

        public Form1()
        {
            InitializeComponent();
            engine = new GameEngine(10);
            UpdateDisplay();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void UpdateDisplay()
        {
            lblDisplay.Text = engine.ToString();
            lblHeroStats.Text = engine.HeroStats;

            // Temporary
            lblCoords.Text = engine.coords;
        }



        private void btnUp_Click(object sender, EventArgs e)
        {
            engine.TriggerMovement(Direction.Up);
            UpdateDisplay();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            engine.TriggerMovement(Direction.Right);
            UpdateDisplay();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            engine.TriggerMovement(Direction.Down);
            UpdateDisplay();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            engine.TriggerMovement(Direction.Left);
            UpdateDisplay();
        }

        private void btnAttackUp_Click(object sender, EventArgs e)
        {
            engine.TriggerAttack(Direction.Up);
            UpdateDisplay();
        }

        private void btnAttackRight_Click(object sender, EventArgs e)
        {
            engine.TriggerAttack(Direction.Right);
            UpdateDisplay();
        }

        private void btnAttackDown_Click(object sender, EventArgs e)
        {
            engine.TriggerAttack(Direction.Down);
            UpdateDisplay();
        }

        private void btnAttackLeft_Click(object sender, EventArgs e)
        {
            engine.TriggerAttack(Direction.Left);
            UpdateDisplay();
        }
    }
}
