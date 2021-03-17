using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Memory;

namespace UnityTester
{
    public partial class Form1 : Form
    {

        
//        ██████╗ ███████╗███╗   ██╗ ██████╗██╗███████╗██╗   ██╗
//        ██╔══██╗██╔════╝████╗  ██║██╔════╝██║██╔════╝╚██╗ ██╔╝
//        ██████╔╝█████╗  ██╔██╗ ██║██║     ██║█████╗   ╚████╔╝ 
//        ██╔══██╗██╔══╝  ██║╚██╗██║██║     ██║██╔══╝    ╚██╔╝  
//        ██║  ██║███████╗██║ ╚████║╚██████╗██║██║        ██║   
//        ╚═╝  ╚═╝╚══════╝╚═╝  ╚═══╝ ╚═════╝╚═╝╚═╝        ╚═╝   
//
//      Hopefully, whoevers looking at this code isn't a skid/leech. If so, go fuck yourself you lil cunt.
                                                      

        public Form1()
        {
            InitializeComponent();

            int walk = 0;
            RegisterHotKey(this.Handle, walk, (int)KeyModifier.Shift, Keys.D1.GetHashCode());

            int run = 1;
            RegisterHotKey(this.Handle, run, (int)KeyModifier.Shift, Keys.D2.GetHashCode());

            int tp = 2;
            RegisterHotKey(this.Handle, tp, (int)KeyModifier.Shift, Keys.D3.GetHashCode());

            
            int noclipUp = 3;
            RegisterHotKey(this.Handle, noclipUp, (int)KeyModifier.Shift, Keys.D4.GetHashCode());

            int noclipDown = 4;
            RegisterHotKey(this.Handle, noclipDown, (int)KeyModifier.Shift, Keys.D5.GetHashCode());
        }

        Mem mem = new Mem();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             disconnectBtn.Enabled = false;

             statsBtn.Enabled = false;
             itemsBtn.Enabled = false;
             ingameBtn.Enabled = false;
             teleportBtn.Enabled = false;
             visionsBtn.Enabled = false;
             ghostBtn.Enabled = false;
             trollBtn.Enabled = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                // 0 being the hotkey id registered.
                if (m.WParam.ToInt32() == 0 && disconnectBtn.Enabled)
                {
                    if (walkToggle.Checked)
                    {
                        walkToggle.Checked = false;
                    }
                    else
                    {
                        walkToggle.Checked = true;
                    }
                }
                else if (m.WParam.ToInt32() == 1 && disconnectBtn.Enabled)
                {
                    if (runToggle.Checked)
                    {
                        runToggle.Checked = false;
                    }
                    else
                    {
                        runToggle.Checked = true;
                    }
                }
                else if (m.WParam.ToInt32() == 2 && disconnectBtn.Enabled)
                {
                    TeleportY(3.1);
                }
            }
        }

        private void SetTheme(Color color)
        {
            foreach (var button in Controls.OfType<Button>())
                button.BackColor = color;
        }

        private void setActiveBtn(Bunifu.Framework.UI.BunifuFlatButton button, Bunifu.Framework.UI.BunifuFlatButton button2,
                                  Bunifu.Framework.UI.BunifuFlatButton button3, Bunifu.Framework.UI.BunifuFlatButton button4,
                                  Bunifu.Framework.UI.BunifuFlatButton button5, Bunifu.Framework.UI.BunifuFlatButton button6,
                                  Bunifu.Framework.UI.BunifuFlatButton button7, Bunifu.Framework.UI.BunifuFlatButton button8)
        {
            button.selected = true;
            button.Normalcolor = Color.DeepSkyBlue;

            button2.selected = false;
            button2.Normalcolor = Color.Transparent;

            button3.selected = false;
            button3.Normalcolor = Color.Transparent;

            button4.selected = false;
            button4.Normalcolor = Color.Transparent;

            button5.selected = false;
            button5.Normalcolor = Color.Transparent;

            button6.selected = false;
            button6.Normalcolor = Color.Transparent;

            button7.selected = false;
            button7.Normalcolor = Color.Transparent;

            button8.selected = false;
            button8.Normalcolor = Color.Transparent;

        }

        public void SetMoney(string amount)
        { 

            mem.WriteMemory(Offsets.Offsets.Money, "int", amount);

        }

        public void SetBasketball(string amount)
        {

            mem.WriteMemory(Offsets.Offsets.Basketball, "int", amount);

        }

        public void SetRank(string amount)
        {

            int rank = int.Parse(amount) * 100;
            mem.WriteMemory(Offsets.Offsets.Rank, "int", rank.ToString());

        }

        public void SetItems(int num)
        {
            mem.WriteMemory(Offsets.ItemOffsets.EMFReader, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Flash, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.PhotoCam, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Lighter, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Candle, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.UVLight, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Crucifix, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.VideoCam, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.SpiritBox, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Salt, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Smudge, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Tripod, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.StrongFlash, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.MotionSensor, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.SoundSensor, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.SanityPills, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Thermometer, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.GhostBook, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.InfraredSensor, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.ParabolicMic, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Glowstick, "int", num.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.HeadCam, "int", num.ToString());

        }

        public void AddItems(string offset, int num)
        {
            int current = mem.ReadInt(offset);
            int newAmount = current + num;
            mem.WriteMemory(offset, "int", newAmount.ToString());
        }

        public void AddAllItems(int num)
        {
            int emf = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newEmf = emf + num;

            int flash = mem.ReadInt(Offsets.ItemOffsets.Flash);
            int newFlash = flash + num;

            int photo = mem.ReadInt(Offsets.ItemOffsets.PhotoCam);
            int newPhoto = photo + num;

            int lighter = mem.ReadInt(Offsets.ItemOffsets.Lighter);
            int newLighter = lighter + num;

            int candle = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newCandle = candle + num;

            int uv = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newUV = uv + num;

            int crucifix = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newCrucifix = crucifix + num;

            int video = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newVideo = video + num;

            int spirit = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newSpirit = spirit + num;

            int salt = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newSalt = salt + num;

            int smudge = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newSmudge = smudge + num;

            int tripod = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newTripod = tripod + num;

            int strong = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newStrong = strong + num;

            int motion = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newMotion = motion + num;

            int sound = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newSound = sound + num;

            int sanity = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newSanity = sanity + num;

            int therm = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newTherm = therm + num;

            int book = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newBook = book + num;

            int infrared = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newInfrared = infrared + num;

            int parabolic = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newParabolic = parabolic + num;

            int glowstick = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newGlowstick = glowstick + num;

            int headcam = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newHeadcam = headcam + num;

            mem.WriteMemory(Offsets.ItemOffsets.EMFReader, "int", newEmf.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Flash, "int", newFlash.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.PhotoCam, "int", newPhoto.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Lighter, "int", newLighter.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Candle, "int", newCandle.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.UVLight, "int", newUV.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Crucifix, "int", newCrucifix.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.VideoCam, "int", newVideo.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.SpiritBox, "int", newSpirit.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Salt, "int", newSalt.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Smudge, "int", newSmudge.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Tripod, "int", newTripod.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.StrongFlash, "int", newStrong.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.MotionSensor, "int", newMotion.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.SoundSensor, "int", newSound.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.SanityPills, "int", newSanity.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Thermometer, "int", newTherm.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.GhostBook, "int", newBook.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.InfraredSensor, "int", newInfrared.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.ParabolicMic, "int", newParabolic.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Glowstick, "int", newGlowstick.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.HeadCam, "int", newHeadcam.ToString());
        }

        public void RemoveAllItems(int num)
        {
            int emf = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newEmf = emf - num;

            int flash = mem.ReadInt(Offsets.ItemOffsets.Flash);
            int newFlash = flash - num;

            int photo = mem.ReadInt(Offsets.ItemOffsets.PhotoCam);
            int newPhoto = photo - num;

            int lighter = mem.ReadInt(Offsets.ItemOffsets.Lighter);
            int newLighter = lighter - num;

            int candle = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newCandle = candle - num;

            int uv = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newUV = uv - num;

            int crucifix = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newCrucifix = crucifix - num;

            int video = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newVideo = video - num;

            int spirit = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newSpirit = spirit - num;

            int salt = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newSalt = salt - num;

            int smudge = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newSmudge = smudge - num;

            int tripod = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newTripod = tripod - num;

            int strong = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newStrong = strong - num;

            int motion = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newMotion = motion - num;

            int sound = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newSound = sound - num;

            int sanity = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newSanity = sanity - num;

            int therm = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newTherm = therm - num;

            int book = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newBook = book - num;

            int infrared = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newInfrared = infrared - num;

            int parabolic = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newParabolic = parabolic - num;

            int glowstick = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newGlowstick = glowstick - num;

            int headcam = mem.ReadInt(Offsets.ItemOffsets.EMFReader);
            int newHeadcam = headcam - num;

            mem.WriteMemory(Offsets.ItemOffsets.EMFReader, "int", newEmf.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Flash, "int", newFlash.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.PhotoCam, "int", newPhoto.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Lighter, "int", newLighter.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Candle, "int", newCandle.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.UVLight, "int", newUV.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Crucifix, "int", newCrucifix.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.VideoCam, "int", newVideo.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.SpiritBox, "int", newSpirit.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Salt, "int", newSalt.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Smudge, "int", newSmudge.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Tripod, "int", newTripod.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.StrongFlash, "int", newStrong.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.MotionSensor, "int", newMotion.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.SoundSensor, "int", newSound.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.SanityPills, "int", newSanity.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Thermometer, "int", newTherm.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.GhostBook, "int", newBook.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.InfraredSensor, "int", newInfrared.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.ParabolicMic, "int", newParabolic.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.Glowstick, "int", newGlowstick.ToString());
            mem.WriteMemory(Offsets.ItemOffsets.HeadCam, "int", newHeadcam.ToString());
        }

        public void TeleportCoords(double x, double y, double z)
        {
            mem.WriteMemory(Offsets.Offsets.XValue, "float", x.ToString());
            mem.WriteMemory(Offsets.Offsets.YValue, "float", y.ToString());
            mem.WriteMemory(Offsets.Offsets.ZValue, "float", z.ToString());
        }

        public void TeleportY(double y)
        {
            mem.WriteMemory(Offsets.Offsets.YValue, "float", y.ToString());
        }

        public void SetWalkSpeed()
        {
            mem.WriteMemory(Offsets.Offsets.WalkSpeed, "float", walkSpeedValue.Value.ToString());
        }

        public void SetRunSpeed()
        {
            mem.WriteMemory(Offsets.Offsets.RunSpeed, "float", runSpeedValue.Value.ToString());
        }

        public void SetVisions(double vision, double brightness)
        {
            mem.WriteMemory(Offsets.Offsets.Visions, "float", vision.ToString());
            mem.WriteMemory(Offsets.Offsets.BrightnessVisions, "float", brightness.ToString());
        }

        public void SetGrabDistance(double distance)
        {
            mem.WriteMemory(Offsets.Offsets.GrabDistance, "float", distance.ToString());
        }

        public void SetColourChanger()
        {
           mem.WriteMemory(Offsets.Offsets.ColourVision, "float", colourValue.Value.ToString());
        }

        public void SetSanity(double number)
        {
            mem.WriteMemory(Offsets.Offsets.Sanity, "float", number.ToString());
        }

        public void SetCamera(double cam1, double cam2, double ingame1, double ingame2)
        {
            mem.WriteMemory(Offsets.Offsets.cam1, "float", cam1.ToString());
            mem.WriteMemory(Offsets.Offsets.cam2, "float", cam2.ToString());
            mem.WriteMemory(Offsets.Offsets.camIngame1, "float", ingame1.ToString());
            mem.WriteMemory(Offsets.Offsets.camIngame2, "float", ingame2.ToString());
        }

        public void GetGhostInfo(string type, string shy, string e1, string e2, string e3)
        {
            ghostType.Text = type;
            ghostShy.Text = shy;

            evidence1.Text = e1;
            evidence2.Text = e2;
            evidence3.Text = e3;
        }



        private void startBtn_Click(object sender, EventArgs e)
        {
            int PID = mem.GetProcIdFromName("Phasmophobia");
            if (PID > 0)
            {
                mem.OpenProcess(PID);
                MessageBox.Show("You have successfully connected!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                startBtn.Enabled = false;
                disconnectBtn.Enabled = true;

                statsBtn.Enabled = true;
                itemsBtn.Enabled = true;
                ingameBtn.Enabled = true;
                teleportBtn.Enabled = true;
                visionsBtn.Enabled = true;
                ghostBtn.Enabled = true;
                trollBtn.Enabled = true;

            }
            else
            {
                MessageBox.Show("Please make sure Phasmophobia is running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void disconnectBtn_Click(object sender, EventArgs e)
        {
            int PID = mem.GetProcIdFromName("Phasmophobia");
            if (PID > 0)
            {
                mem.CloseProcess();
                MessageBox.Show("Trainer has disconnected!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                startBtn.Enabled = true;
                disconnectBtn.Enabled = false;

                statsBtn.Enabled = false;
                itemsBtn.Enabled = false;
                ingameBtn.Enabled = false;
                teleportBtn.Enabled = false;
                visionsBtn.Enabled = false;
                ghostBtn.Enabled = false;
                trollBtn.Enabled = false;
            }
            else
            {
                MessageBox.Show("Trainer has failed to disconnect, please exit game and trainer!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitToolBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();

            UnregisterHotKey(this.Handle, 0);
            UnregisterHotKey(this.Handle, 1);
            UnregisterHotKey(this.Handle, 2);
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            if (disconnectBtn.Enabled)
            {
                tabs.SelectedIndex = 0;

                setActiveBtn(homeBtn, statsBtn, itemsBtn, ingameBtn, teleportBtn, visionsBtn, ghostBtn, trollBtn);
            }
        }

        private void statsBtn_Click(object sender, EventArgs e)
        {
            if (disconnectBtn.Enabled)
            {
                tabs.SelectedIndex = 1;

                setActiveBtn(statsBtn, homeBtn, itemsBtn, ingameBtn, teleportBtn, visionsBtn, ghostBtn, trollBtn);
            }
        }

        private void lobbyBtn_Click(object sender, EventArgs e)
        {
            if (disconnectBtn.Enabled)
            {
                tabs.SelectedIndex = 2;

                setActiveBtn(itemsBtn, homeBtn, statsBtn, ingameBtn, teleportBtn, visionsBtn, ghostBtn, trollBtn);
            }
        }

        private void ingameBtn_Click(object sender, EventArgs e)
        {
            if (disconnectBtn.Enabled)
            {
                tabs.SelectedIndex = 3;

                setActiveBtn(ingameBtn, homeBtn, statsBtn, itemsBtn, teleportBtn, visionsBtn, ghostBtn, trollBtn);
            }
        }

        private void teleportBtn_Click(object sender, EventArgs e)
        {
            if (disconnectBtn.Enabled)
            {
                tabs.SelectedIndex = 4;

                setActiveBtn(teleportBtn, homeBtn, statsBtn, itemsBtn, ingameBtn, visionsBtn, ghostBtn, trollBtn);
            }
        }

        private void visionsBtn_Click(object sender, EventArgs e)
        {
            if (disconnectBtn.Enabled)
            {
                tabs.SelectedIndex = 5;

                setActiveBtn(visionsBtn, homeBtn, statsBtn, itemsBtn, ingameBtn, teleportBtn, ghostBtn, trollBtn);
            }
        }

        private void ghostBtn_Click(object sender, EventArgs e)
        {
            if (disconnectBtn.Enabled)
            {
                tabs.SelectedIndex = 6;

                setActiveBtn(ghostBtn, homeBtn, statsBtn, itemsBtn, ingameBtn, teleportBtn, visionsBtn, trollBtn);
            }
        }

        private void trollBtn_Click(object sender, EventArgs e)
        {
            if (disconnectBtn.Enabled)
            {
                tabs.SelectedIndex = 7;

                setActiveBtn(trollBtn, homeBtn, statsBtn, itemsBtn, ingameBtn, teleportBtn, visionsBtn, ghostBtn);
            }
        }

        private void setMoneyBtn_Click(object sender, EventArgs e)
        {
            if (moneyTxt.TextLength == 0)
            {
                MessageBox.Show("Please enter a number from 0 - 999999!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SetMoney(moneyTxt.Text);
                string message = string.Format("You now have {0}! Rejoin the lobby to see the changes.", moneyTxt.Text);
                MessageBox.Show(message, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void setMoneyBtn2_Click(object sender, EventArgs e)
        {
            SetMoney("1000");
            MessageBox.Show("You now have $1000! Rejoin the lobby to see the changes.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void setRankBtn_Click(object sender, EventArgs e)
        {
            if (rankTxt.TextLength == 0)
            {
                MessageBox.Show("Please enter a number from 0 - 999999!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SetRank(rankTxt.Text);
                string message = string.Format("You are now rank {0}! Rejoin the lobby to see the changes.", rankTxt.Text);
                MessageBox.Show(message, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void setRankBtn2_Click(object sender, EventArgs e)
        {
            SetRank("1000");
            MessageBox.Show("You are now rank 1000! Rejoin the lobby to see the changes.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void setBasketballBtn_Click(object sender, EventArgs e)
        {
            if (basketballTxt.TextLength == 0)
            {
                MessageBox.Show("Please enter a number from 0 - 999999!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SetBasketball(basketballTxt.Text);
                string message = string.Format("Your score is now {0}! Shoot the hoop to see the changes.", basketballTxt.Text);
                MessageBox.Show(message, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void setBasketballBtn2_Click(object sender, EventArgs e)
        {
            SetBasketball("1000");
            MessageBox.Show("Your score is now 1000! Shoot the hoop to see the changes.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void setItemsBtn10_Click(object sender, EventArgs e)
        {
            SetItems(10);
            MessageBox.Show("You now have 10 of every item! Add/buy an item to see it change!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void setItemsBtn50_Click(object sender, EventArgs e)
        {
            SetItems(50);
            MessageBox.Show("You now have 50 of every item! Add/buy an item to see it change!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void setItemsBtn999_Click(object sender, EventArgs e)
        {
            SetItems(999);
            MessageBox.Show("You now have 999 of every item! Add/buy an item to see it change!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void moneyTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void rankTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void basketballTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void bunifuGradientPanel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ridgeviewVan_Click(object sender, EventArgs e)
        {
            TeleportCoords(-2.008718252, 1.933461308, -2.746394634);
        }

        private void ridgeviewHouse_Click(object sender, EventArgs e)
        {
            TeleportCoords(1.908051252, 1.874999881, 2.110069513);
        }

        private void ridgeviewBasement_Click(object sender, EventArgs e)
        {
            TeleportCoords(-2.063954353, -1.968437433, 2.435604095);
        }

        private void tanglewoodVan_Click(object sender, EventArgs e)
        {
            TeleportCoords(2.278203487, 1.9331214542, 2.729306936);
        }

        private void tanglewoodHouse_Click(object sender, EventArgs e)
        {
            TeleportCoords(1.798781753, 1.832499862, 2.011455059);
        }

        private void tanglewoodBasement_Click(object sender, EventArgs e)
        {
            TeleportCoords(2.347207785, -1.98968482, -2.625979662);
        }

        private void bleasdaleVan_Click(object sender, EventArgs e)
        {
            TeleportCoords(-2.83970356, 1.876587987, -2.027475595);
        }

        private void bleasdaleHouse_Click(object sender, EventArgs e)
        {
            TeleportCoords(-1.547386289, 1.832499862, 1.240987897);
        }

        private void bleasedaleAttic_Click(object sender, EventArgs e)
        {
            TeleportCoords(2.395795345, 2.428437233, -2.040577173);
        }

        private void walkToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (walkToggle.Checked)
            {
                walkTimer.Enabled = true;
            }
            else
            {
                mem.WriteMemory(Offsets.Offsets.WalkSpeed, "float", "1.2");
                walkTimer.Enabled = false;
            }
        }

        private void runToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (runToggle.Checked)
            {
                runTimer.Enabled = true;
            }
            else
            {
                mem.WriteMemory(Offsets.Offsets.RunSpeed, "float", "1.6");
                runTimer.Enabled = false;
            }
        }

        private void walkTimer_Tick(object sender, EventArgs e)
        {
            SetWalkSpeed();
        }

        private void runTimer_Tick(object sender, EventArgs e)
        {
            SetRunSpeed();
        }

        private void fastWalkBtn_Click(object sender, EventArgs e)
        {
            walkToggle.Checked = true;
            walkSpeedValue.Value = 5;
        }

        private void veryFastWalkBtn_Click(object sender, EventArgs e)
        {
            walkToggle.Checked = true;
            walkSpeedValue.Value = 20;
        }

        private void extremelyFastWalkBtn_Click(object sender, EventArgs e)
        {
            walkToggle.Checked = true;
            walkSpeedValue.Value = 40;
        }

        private void fastRunBtn_Click(object sender, EventArgs e)
        {
            runToggle.Checked = true;
            runSpeedValue.Value = 5;
        }

        private void veryFastRunBtn_Click(object sender, EventArgs e)
        {
            runToggle.Checked = true;
            runSpeedValue.Value = 15;
        }

        private void extremelyFastRunBtn_Click(object sender, EventArgs e)
        {
            runToggle.Checked = true;
            runSpeedValue.Value = 40;
        }

        private void prisonVan_Click(object sender, EventArgs e)
        {
            TeleportCoords(3.28006196, 1.976587892, 1.918214679);
        }

        private void prisonEnter_Click(object sender, EventArgs e)
        {
            TeleportCoords(3.050585508, 1.9799999, -2.184454679);
        }

        private void prisonBBlock_Click(object sender, EventArgs e)
        {
            TeleportCoords(2.006438494, 1.97874999, -2.884879112);
        }

        private void prisonABlock_Click(object sender, EventArgs e)
        {
            TeleportCoords(2.11775732, 1.97874999, 2.993386507);
        }

        private void schoolVan_Click(object sender, EventArgs e)
        {
            TeleportCoords(2.368909359, 1.651351929, -2.789248705);
        }

        private void schoolEnter_Click(object sender, EventArgs e)
        {
            TeleportCoords(-1.729580641, 1.832499743, -1.90770781);
        }

        private void schoolLeft_Click(object sender, EventArgs e)
        {
            TeleportCoords(3.001981735, 2.364374876, 2.733529806);
        }

        private void schoolRight_Click(object sender, EventArgs e)
        {
            TeleportCoords(-3.001981735, 2.364374876, 2.733529806);
        }

        private void asylumVan_Click(object sender, EventArgs e)
        {
            TeleportCoords(2.250194311,   1.976587892, 2.158783197);
        }

        private void asylumEnter_Click(object sender, EventArgs e)
        {
            TeleportCoords(-2.013975859, 1.832499862, 2.107584476);
        }

        private void asylumRight_Click(object sender, EventArgs e)
        {
            TeleportCoords(-2.901167154, -2.165849924, -3.360104322);
        }

        private void asylumLeft_Click(object sender, EventArgs e)
        {
            TeleportCoords(-2.901167154, -2.165849924, 3.370104322);
        }

        private void visionOneBtn_Click(object sender, EventArgs e)
        {
            SetVisions(1.1, 0.8);
        }

        private void visionTwoBtn_Click(object sender, EventArgs e)
        {
            SetVisions(1.2, 0.8);
        }

        private void blueTrippyVision_Click(object sender, EventArgs e)
        {
            SetVisions(0.75, 0.8);
        }

        private void redVision_Click(object sender, EventArgs e)
        {
            SetVisions(1, 8);
        }

        private void blackWhiteVision_Click(object sender, EventArgs e)
        {
            SetVisions(1, 101);
        }

        private void warmVision_Click(object sender, EventArgs e)
        {
            SetVisions(1, 5);
        }

        private void invertedVision_Click(object sender, EventArgs e)
        {
            SetVisions(1, -5);
        }

        private void visionBlack_Click(object sender, EventArgs e)
        {
            SetVisions(1, 999);
        }

        private void visionNormalBtn_Click(object sender, EventArgs e)
        {
            SetVisions(1, 0.8);
        }

        private void colourToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (colourToggle.Checked)
            {
                colourTimer.Start();
            }
            else
            {
                mem.WriteMemory(Offsets.Offsets.ColourVision, "float", 0.1.ToString());
                colourTimer.Stop();
            }
        }

        private void colourTimer_Tick(object sender, EventArgs e)
        {
            SetColourChanger();
        }

        private void teleportRoofBtn_Click(object sender, EventArgs e)
        {
            TeleportY(3.1);
        }

        private void teleportSkyBtn_Click(object sender, EventArgs e)
        {
            TeleportY(4.5);
        }

        private void refreshGhostBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Ghost Name
                byte[] nameByte = mem.ReadBytes(Offsets.Offsets.ghostName, 10 * 3);
                string name = Encoding.Unicode.GetString(nameByte);
                ghostName.Text = name;

                // Ghost Age
                int age = mem.ReadInt(Offsets.Offsets.ghostAge, "");
                ghostAge.Text = age.ToString();

                // Ghost Type
                int type = mem.ReadInt(Offsets.Offsets.ghostType, "");
                switch (type)
                {
                    case 1:
                        GetGhostInfo("Spirit", "No", "Spirit Box", "Fingerprints", "Ghost Writing");
                        break;
                    case 2:
                        GetGhostInfo("Wraith", "No", "Fingerprints", "Freezing Temps", "Spirit Box");
                        break;
                    case 3:
                        GetGhostInfo("Phantom", "No", "EMF 5", "Ghost Orb", "Freezing Temps");
                        break;
                    case 4:
                        GetGhostInfo("Poltergeist", "No", "Spirit Box", "Fingerprints", "Ghost Orb");
                        break;
                    case 5:
                        GetGhostInfo("Banshee", "No", "EMF 5", "Fingerprints", "Freezing Temps");
                        break;
                    case 6:
                        GetGhostInfo("Jinn", "No", "Spirit Box", "Ghost Orb", "EMF 5");
                        break;
                    case 7:
                        GetGhostInfo("Mare", "No", "Spirit Box", "Ghost Orb", "Freezing Temps");
                        break;
                    case 8:
                        GetGhostInfo("Revenant", "No", "EMF 5", "Fingerprints", "Ghost Writing");
                        break;
                    case 9:
                        GetGhostInfo("Shade", "Yes", "EMF 5", "Ghost Orb", "Ghost Writing");
                        break;
                    case 10:
                        GetGhostInfo("Demon", "No", "Spirit Box", "Ghost Writing", "Freezing Temps");
                        break;
                    case 11:
                        GetGhostInfo("Yurei", "No", "Ghost Orb", "Ghost Writing", "Freezing Temps");
                        break;
                    case 12:
                        GetGhostInfo("Oni", "No", "EMF 5", "Spirit Box", "Ghost Writing");
                        break;
                    default:
                        GetGhostInfo("?", "?", "?", "?", "?");
                        break;
                }
            }
            catch
            {
                MessageBox.Show("You must be in a game to do that!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void changeNameBtn_Click(object sender, EventArgs e)
        {
            byte[] name = Encoding.Unicode.GetBytes(nameTxt.Text);

            mem.WriteBytes(Offsets.Offsets.Name, name);
        }

        private void grabToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (grabToggle.Checked)
            {
                grabTimer.Start();
            }
            else
            {
                grabTimer.Stop();
                SetGrabDistance(1.6);
            }
        }

        private void sanityToggle_CheckedChanged(object sender, EventArgs e)
        {
            if(sanityToggle.Checked)
            {
                sanityTimer.Start();
            }
            else
            {
                sanityTimer.Stop();
                SetSanity(0);
            }

        }

        private void grabTimer_Tick(object sender, EventArgs e)
        {
            SetGrabDistance(grabValue.Value);
        }

        private void shortGrabBtn_Click(object sender, EventArgs e)
        {
            grabToggle.Checked = true;
            grabValue.Value = 1;
        }

        private void farGrabBtn_Click(object sender, EventArgs e)
        {
            grabToggle.Checked = true;
            grabValue.Value = 5;
        }

        private void veryFarGrabBtn_Click(object sender, EventArgs e)
        {
            grabToggle.Checked = true;
            grabValue.Value = 10;
        }

        private void sanityTimer_Tick(object sender, EventArgs e)
        {
            SetSanity(-2);
        }

        private void ghostInfoTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // Ghost Name
                byte[] nameByte = mem.ReadBytes(Offsets.Offsets.ghostName, 10 * 3);
                string name = Encoding.Unicode.GetString(nameByte);
                ghostName.Text = name;

                // Ghost Age
                int age = mem.ReadInt(Offsets.Offsets.ghostAge);
                ghostAge.Text = age.ToString();

                // Ghost Type
                int type = mem.ReadInt(Offsets.Offsets.ghostType);
                switch (type)
                {
                    case 1:
                        GetGhostInfo("Spirit", "No", "Spirit Box", "Fingerprints", "Ghost Writing");
                        break;
                    case 2:
                        GetGhostInfo("Wraith", "No", "Fingerprints", "Freezing Temps", "Spirit Box");
                        break;
                    case 3:
                        GetGhostInfo("Phantom", "No", "EMF 5", "Ghost Orb", "Freezing Temps");
                        break;
                    case 4:
                        GetGhostInfo("Poltergeist", "No", "Spirit Box", "Fingerprints", "Ghost Orb");
                        break;
                    case 5:
                        GetGhostInfo("Banshee", "No", "EMF 5", "Fingerprints", "Freezing Temps");
                        break;
                    case 6:
                        GetGhostInfo("Jinn", "No", "Spirit Box", "Ghost Orb", "EMF 5");
                        break;
                    case 7:
                        GetGhostInfo("Mare", "No", "Spirit Box", "Ghost Orb", "Freezing Temps");
                        break;
                    case 8:
                        GetGhostInfo("Revenant", "No", "EMF 5", "Fingerprints", "Ghost Writing");
                        break;
                    case 9:
                        GetGhostInfo("Shade", "Yes", "EMF 5", "Ghost Orb", "Ghost Writing");
                        break;
                    case 10:
                        GetGhostInfo("Demon", "No", "Spirit Box", "Ghost Writing", "Freezing Temps");
                        break;
                    case 11:
                        GetGhostInfo("Yurei", "No", "Ghost Orb", "Ghost Writing", "Freezing Temps");
                        break;
                    case 12:
                        GetGhostInfo("Oni", "No", "EMF 5", "Spirit Box", "Ghost Writing");
                        break;
                    default:
                        GetGhostInfo("?", "?", "?", "?", "?");
                        break;
                }

                // Ghost Fav Room
                byte[] favRoomByte = mem.ReadBytes(Offsets.Offsets.ghostFavRoom, 10 * 3);
                string favRoom = Encoding.Unicode.GetString(favRoomByte);
                ghostFavRoom.Text = favRoom;

                // Ghost Speed
                float speed = mem.ReadFloat(Offsets.Offsets.ghostSpeed);
                ghostSpeed.Text = speed.ToString();

                // Ghost Can Hunt
                int canHunt = mem.ReadInt(Offsets.Offsets.ghostCanHunt);

                // Ghost Hunting
                int hunting = mem.ReadInt(Offsets.Offsets.ghostIsHunting);

                switch (canHunt)
                {
                    case 0:
                        ghostCanHunt.Text = "No";
                        break;
                    case 1:
                        ghostCanHunt.Text = "Yes";
                        break;
                    default:
                        ghostCanHunt.Text = "No";
                        break;
                }

                switch (hunting)
                {
                    case 0:
                        ghostHunting.Text = "No";
                        break;
                    case 1:
                        ghostHunting.Text = "Yes";
                        break;
                    default:
                        ghostHunting.Text = "No";
                        break;

                }
            }
            catch
            {
                ghostInfoTimer.Stop();
                MessageBox.Show("You must be in a game to do that!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void autoRefreshBtn_Click(object sender, EventArgs e)
        {
            ghostInfoTimer.Start();
        }

        private void disableRefreshBtn_Click(object sender, EventArgs e)
        {
            ghostInfoTimer.Stop();
        }

        private void refreshExtraBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Ghost Fav Room
                byte[] favRoomByte = mem.ReadBytes(Offsets.Offsets.ghostFavRoom, 10 * 3);
                string favRoom = Encoding.Unicode.GetString(favRoomByte);
                ghostFavRoom.Text = favRoom;

                // Ghost Speed
                float speed = mem.ReadFloat(Offsets.Offsets.ghostSpeed);
                ghostSpeed.Text = speed.ToString();

                // Ghost Can Hunt
                int canHunt = mem.ReadInt(Offsets.Offsets.ghostCanHunt, "");

                // Ghost Hunting
                int hunting = mem.ReadInt(Offsets.Offsets.ghostIsHunting, "");

                switch (canHunt)
                {
                    case 0:
                        ghostCanHunt.Text = "No";
                        break;
                    case 1:
                        ghostCanHunt.Text = "Yes";
                        break;
                    default:
                        ghostCanHunt.Text = "No";
                        break;
                }

                switch (hunting)
                {
                    case 256:
                        ghostHunting.Text = "No";
                        break;
                    case 257:
                        ghostHunting.Text = "Yes";
                        break;
                    default:
                        ghostHunting.Text = "No";
                        break;
                }
            }
            catch
            {
                MessageBox.Show("You must be in a game to do that!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ghostHuntToggle_CheckedChanged(object sender, EventArgs e)
        {
            if(ghostHuntToggle.Checked)
            {
                mem.WriteMemory(Offsets.Offsets.ghostIsHunting, "int", "1");
                mem.WriteMemory(Offsets.Offsets.ghostCanAttack, "int", "1");
                mem.WriteMemory(Offsets.Offsets.ghostCanHunt, "int", "1");
                mem.WriteMemory(Offsets.Offsets.ghostCanWander, "int", "1");

            }
            else
            {
                mem.WriteMemory(Offsets.Offsets.ghostIsHunting, "int", "0");
                mem.WriteMemory(Offsets.Offsets.ghostCanAttack, "int", "0");
                mem.WriteMemory(Offsets.Offsets.ghostCanHunt, "int", "0");
                mem.WriteMemory(Offsets.Offsets.ghostCanWander, "int", "0");
            }
        }

        private void addEMF_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.EMFReader, 5);
        }

        private void addFlashlight_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.Flash, 5);
        }

        private void addPhotoCam_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.PhotoCam, 5);
        }

        private void addLighter_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.Lighter, 5);
        }

        private void addCandle_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.Candle, 5);
        }

        private void addUV_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.UVLight, 5);
        }

        private void addCrucifix_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.Crucifix, 5);
        }

        private void addVideoCam_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.VideoCam, 5);
        }

        private void addSpiritBox_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.SpiritBox, 5);
        }

        private void addSalt_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.Salt, 5);
        }

        private void addSmudge_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.Smudge, 5);
        }

        private void addTripod_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.Tripod, 5);
        }

        private void addStrongFlash_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.StrongFlash, 5);
        }

        private void addMotionSensor_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.MotionSensor, 5);
        }

        private void addThermometer_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.Thermometer, 5);
        }

        private void addSanityPills_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.SanityPills, 5);
        }

        private void addGhostBook_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.GhostBook, 5);
        }

        private void addInfrared_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.InfraredSensor, 5);
        }

        private void addParabolicMic_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.ParabolicMic, 5);
        }

        private void addGlowstick_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.Glowstick, 5);
        }

        private void addHeadCam_Click(object sender, EventArgs e)
        {
            AddItems(Offsets.ItemOffsets.HeadCam, 5);
        }

        private void addEverything_Click(object sender, EventArgs e)
        {
            AddAllItems(5);
        }

        private void remEverything_Click(object sender, EventArgs e)
        {
            RemoveAllItems(5);
        }

        private void fullbrightToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (fullbrightToggle.Checked)
            {
                mem.WriteMemory(Offsets.Offsets.FullbrightIntensity, "float", 0.5.ToString());
                mem.WriteMemory(Offsets.Offsets.FullbrightRange, "float", 500.ToString());
                fullbrightTimer.Start();
            }
            else
            {
                fullbrightTimer.Stop();
                mem.WriteMemory(Offsets.Offsets.FullbrightIntensity, "float", 0.8.ToString());
                mem.WriteMemory(Offsets.Offsets.FullbrightRange, "float", 10.ToString());
                mem.WriteMemory(Offsets.Offsets.FullbrightSpotAngle, "float", 0.ToString());
                fullbrightValue.Value = 0;
            }
        }

        private void fullbrightTimer_Tick(object sender, EventArgs e)
        {
            mem.WriteMemory(Offsets.Offsets.TorchSpotAngle, "float", fullbrightValue.Value.ToString());
        }

        private void fovToggle_CheckedChanged(object sender, EventArgs e)
        {
            if(fovToggle.Checked)
            {
                fovTimer.Start();
            }
            else
            {
                fovTimer.Stop();
                mem.WriteMemory(Offsets.Offsets.FOV, "float", 74.ToString());
            }
        }

        private void fovTimer_Tick(object sender, EventArgs e)
        {
            mem.WriteMemory(Offsets.Offsets.FOV, "float", fovValue.Value.ToString());
        }

        private void camToggle_CheckedChanged(object sender, EventArgs e)
        {
            if(camToggle.Checked)
            {
                camTimer.Start();
            }
            else
            {
                camTimer.Stop();
                SetCamera(90, -90, 90, -90);
            }
        }

        private void camTimer_Tick(object sender, EventArgs e)
        {
            SetCamera(360, -360, 360, -360);
        }

        private void groupBox30_Enter(object sender, EventArgs e)
        {

        }
    }
}
