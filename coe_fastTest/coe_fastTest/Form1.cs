using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
namespace coe_fastTest
{
    public partial class Form1 : Form
    {


        // Keyhook constants
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;



        private const int COOLDOWN_INCREASE_VALUE = 100;

        private const int ELEMENT_UP_TIME = 4000;
        private static int stopTimeOffset = 4000;
        private static DateTime endTime ;
        private static int activeElementIdx = 0;
        private static Boolean m_Paused = true;
        private static Boolean m_Started = false;
        private static Boolean m_switchElements = false;

        private static List<ConvectionElements> activeElems;
        private static System.Windows.Forms.Timer timer;


        private HeroClasses hero;

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!m_Paused)
            {
                // Switchelements must be realised here in nonstatic context
                if (m_switchElements)
                {
                    activeElementIdx = (activeElementIdx + 1) % activeElems.Count;
                    panelCurrent.BackColor = getElemColor(activeElems[activeElementIdx]);
                    panelNext.BackColor = getElemColor(activeElems[(activeElementIdx + 1) % activeElems.Count]);
                    m_switchElements = false;
                }
                

                TimeSpan leftTime = endTime.Subtract(DateTime.Now);
                Debug.WriteLine("Timer Tick");
                if (leftTime.TotalSeconds < 0)
                {
                    Debug.WriteLine("Timer switching Element");
                    activeElementIdx = (activeElementIdx + 1) % activeElems.Count;
                    panelCurrent.BackColor = getElemColor(activeElems[activeElementIdx]);
                    panelNext.BackColor = getElemColor(activeElems[(activeElementIdx + 1) % activeElems.Count]);
                    endTime = DateTime.Now.AddMilliseconds(ELEMENT_UP_TIME);
                    Refresh();

                }
                else
                {
                    cooldownLabel.Text = leftTime.Seconds.ToString();
                    /* string countDownString = leftTime.Hours.ToString("00") + ":" +
                       leftTime.Minutes.ToString("00") + ":" +
                       leftTime.Seconds.ToString("00") + ":" +
                        (leftTime.Milliseconds / 10).ToString("00");*/
                    Refresh();
                }    
            }
            
        }



        private static Color getElemColor(ConvectionElements elem)
        {
            Color retVal = Color.AntiqueWhite;
            switch (elem)
            {
                case ConvectionElements.ARCANE:
                    retVal = Color.BlueViolet;
                    break;
                case ConvectionElements.PHYSICAL:
                    retVal = Color.Gray;
                    break;
                case ConvectionElements.FIRE:
                    retVal = Color.Firebrick;
                    break;
                case ConvectionElements.LIGHTNING:
                    retVal = Color.Blue;
                    break;
                case ConvectionElements.HOLY:
                    retVal = Color.Yellow;
                    break;
                case ConvectionElements.COLD:
                    retVal = Color.LightBlue;
                    break;
                case ConvectionElements.POISON:
                    retVal = Color.Green;
                    break;
                default:
                    break;
            }
            return retVal;
        }


        
        private  List<ConvectionElements> GetHeroElements(HeroClasses hero)
        {
            List<ConvectionElements> retVal = new List<ConvectionElements>();
            switch (hero)
            {
                case HeroClasses.MONK:
                    retVal.Add(ConvectionElements.COLD);
                    retVal.Add(ConvectionElements.FIRE);
                    retVal.Add(ConvectionElements.HOLY);
                    retVal.Add(ConvectionElements.LIGHTNING);
                    retVal.Add(ConvectionElements.PHYSICAL);
                    break;
                case HeroClasses.WD:
                    retVal.Add(ConvectionElements.COLD);
                    retVal.Add(ConvectionElements.FIRE);
                    retVal.Add(ConvectionElements.PHYSICAL);
                    retVal.Add(ConvectionElements.POISON);
                    break;
                default:

                    break;
            }
            return retVal;
        }




        private static void buttonEventListener(int p_Key)
        {
            switch (p_Key)
            {
                case (int) FunctionKeys.PAUSETIMER:
                    if (!m_Paused)
                    {
                        stopTimeOffset = (int) endTime.Subtract(DateTime.Now).TotalMilliseconds;
                        m_Paused = true;
                    }
                    break;
                case (int)FunctionKeys.RESETCOLORTIMER:
                    endTime = DateTime.Now.AddMilliseconds(ELEMENT_UP_TIME);
                    break;
                case (int)FunctionKeys.RESUMETIMER:
                    if (m_Paused)
                    {
                        endTime = DateTime.Now.AddMilliseconds(stopTimeOffset);
                        m_Paused = false;
                    }
                    break;
                case (int)FunctionKeys.SKIPCOLOR:
                    m_switchElements = true;
                    break;
                case (int)FunctionKeys.TIMERINCREASE:
                    endTime = endTime.Subtract(DateTime.Now).Milliseconds > (ELEMENT_UP_TIME - COOLDOWN_INCREASE_VALUE) ? DateTime.Now.AddMilliseconds(ELEMENT_UP_TIME) : endTime.AddMilliseconds(COOLDOWN_INCREASE_VALUE);
                    break;
                case (int)FunctionKeys.TIMERREDUCE:
                    endTime = endTime.Subtract(DateTime.Now).Milliseconds < COOLDOWN_INCREASE_VALUE ? DateTime.Now : endTime.AddMilliseconds(-COOLDOWN_INCREASE_VALUE);

                    break;
            }
        }
        
        public Form1(HeroClasses p_hero)
        {
            hero = p_hero;
            InitializeComponent();
            activeElems = GetHeroElements(p_hero);
            _hookID = SetHook(_proc);

            timer = new System.Windows.Forms.Timer();
            endTime = DateTime.Now.AddMilliseconds(ELEMENT_UP_TIME);

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 50;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cooldownLabel.Text = "4";
            panelCurrent.BackColor = getElemColor(activeElems[activeElementIdx]);
            panelNext.BackColor = getElemColor(activeElems[(activeElementIdx + 1) % activeElems.Count]);

            timer.Start(); 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }






        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                /*if (vkCode == (int)Keys.F11)
                    Console.WriteLine("F11");
                Console.WriteLine(vkCode);
                 */
                buttonEventListener(vkCode);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
