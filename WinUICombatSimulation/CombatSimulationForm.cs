using PF2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUICombatSimulation
{
    public partial class CombatSimulationForm : Form, PF2.ICombatSimulationUserInterface
    {
        PF2.PF2CombatSimulation simulation;
        Observer observer;
        string monster_name="";

        public CombatSimulationForm()
        {
            InitializeComponent();
            simulation = new PF2.PF2CombatSimulation();
            observer = new Observer(richTextBoxObserverUI);
        }

        public IObserver GetObserver()
        {
            return observer;
        }

        public void LogTurn(uint turn)
        {
            observer.Message("*** Turn: " + turn + " ***" + Environment.NewLine, Color.Cyan);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string jsonText = File.ReadAllText("monsters-pf2.json");

            observer.Message("Loading Monster Database...");
            PF2.MonsterFactory.LoadData(jsonText);
            observer.Message("Loaded " + PF2.MonsterFactory.GetCount() + " creatures!");
            
            monsterComboBox.DataSource = PF2.MonsterFactory.GetNames();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            monster_name = monsterComboBox.Text;
            richTextBoxObserverUI.Clear();
            observer.Message("Selected Monster: " + monster_name + Environment.NewLine, Color.BlueViolet);
            if (MonsterFactory.Contains(monster_name))
            {
                //Start simulation?
                simulation.Run(monster_name, this);
            } else
            {
                observer.Message("Invalid monster name", Color.Red);
            }
        }
    }
}
