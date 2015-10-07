using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace coe_fastTest
{

    public enum FunctionKeys
    {
        SKIPCOLOR = Keys.F5,
        RESETCOLORTIMER = Keys.F6,
        PAUSETIMER = Keys.F7,
        RESUMETIMER = Keys.F8,
        TIMERREDUCE = Keys.F9,
        TIMERINCREASE = Keys.F10

    }


    public enum ConvectionElements
    {
        FIRE,
        COLD,
        HOLY,
        POISON,
        ARCANE,
        LIGHTNING,
        PHYSICAL
    }

    public enum HeroClasses
    {
        WD,
        MONK
    }

  

    static class Program
    {




            /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(HeroClasses.MONK));
        }
    }
}
