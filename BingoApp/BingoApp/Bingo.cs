using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BingoApp
{
    public partial class frmBingo : Form
    {
        enum GameStatusEnum { NotStarted, PickModeBoard, Playing, Winner };
        GameStatusEnum gamestatus = GameStatusEnum.NotStarted;
        enum GameModeEnum { Number, Letter};
        GameModeEnum gamemode = GameModeEnum.Number;

        string Winner = "";
        Color defaultbackcolor;

        List<TextBox> lsttxtPlayer1;
        List<TextBox> lsttxtPlayer2;

        List<List<TextBox>> lstwinningsetsP1;
        List<List<TextBox>> lstwinningsetsP2;

        List<RadioButton> lstoptYesNo;
        List<RadioButton> lstoptHowBoardsPicked;

        public frmBingo()
        {
            InitializeComponent();

            DisplayTextbtnPick();

            lsttxtPlayer1 = new() { txtBox1P1, txtBox2P1, txtBox3P1, txtBox4P1, txtBox5P1, txtBox6P1, txtBox7P1, txtBox8P1, txtBox9P1, txtBox10P1, txtBox11P1, txtBox12P1, txtBox13P1, txtBox14P1, txtBox15P1, txtBox16P1, txtBox17P1, txtBox18P1, txtBox19P1, txtBox20P1, txtBox21P1, txtBox22P1, txtBox23P1, txtBox24P1, txtBox25P1 };
            lsttxtPlayer2 = new() { txtBox1P2, txtBox2P2, txtBox3P2, txtBox4P2, txtBox5P2, txtBox6P2, txtBox7P2, txtBox8P2, txtBox9P2, txtBox10P2, txtBox11P2, txtBox12P2, txtBox13P2, txtBox14P2, txtBox15P2, txtBox16P2, txtBox17P2, txtBox18P2, txtBox19P2, txtBox20P2, txtBox21P2, txtBox22P2, txtBox23P2, txtBox24P2, txtBox25P2 };
            
            lsttxtPlayer1.ForEach(txt => txt.Enabled = false);
            lsttxtPlayer2.ForEach(txt => txt.Enabled = false);

            lstwinningsetsP1 = new()
            {
                new() {txtBox1P1, txtBox2P1, txtBox3P1, txtBox4P1, txtBox5P1},
                new() {txtBox6P1, txtBox7P1, txtBox8P1, txtBox9P1, txtBox10P1},
                new() {txtBox11P1, txtBox12P1, txtBox13P1, txtBox14P1, txtBox15P1},
                new() {txtBox16P1, txtBox17P1, txtBox18P1, txtBox19P1, txtBox20P1},
                new() {txtBox21P1, txtBox22P1, txtBox23P1, txtBox24P1, txtBox25P1},
                new() {txtBox1P1, txtBox6P1, txtBox11P1, txtBox16P1, txtBox21P1},
                new() {txtBox2P1, txtBox7P1, txtBox12P1, txtBox17P1, txtBox22P1},
                new() {txtBox3P1, txtBox8P1, txtBox13P1, txtBox18P1, txtBox23P1},
                new() {txtBox4P1, txtBox9P1, txtBox14P1, txtBox19P1, txtBox24P1},
                new() {txtBox5P1, txtBox10P1, txtBox15P1, txtBox20P1, txtBox25P1},
                new() {txtBox1P1, txtBox7P1, txtBox13P1, txtBox19P1, txtBox25P1},
                new() {txtBox5P1, txtBox9P1, txtBox13P1, txtBox17P1, txtBox21P1}
            };
            lstwinningsetsP2 = new()
            {
                new() {txtBox1P2, txtBox2P2, txtBox3P2, txtBox4P2, txtBox5P2},
                new() {txtBox6P2, txtBox7P2, txtBox8P2, txtBox9P2, txtBox10P2},
                new() {txtBox11P2, txtBox12P2, txtBox13P2, txtBox14P2, txtBox15P2},
                new() {txtBox16P2, txtBox17P2, txtBox18P2, txtBox19P2, txtBox20P2},
                new() {txtBox21P2, txtBox22P2, txtBox23P2, txtBox24P2, txtBox25P2},
                new() {txtBox1P2, txtBox6P2, txtBox11P2, txtBox16P2, txtBox21P2},
                new() {txtBox2P2, txtBox7P2, txtBox12P2, txtBox17P2, txtBox22P2},
                new() {txtBox3P2, txtBox8P2, txtBox13P2, txtBox18P2, txtBox23P2},
                new() {txtBox4P2, txtBox9P2, txtBox14P2, txtBox19P2, txtBox24P2},
                new() {txtBox5P2, txtBox10P2, txtBox15P2, txtBox20P2, txtBox25P2},
                new() {txtBox1P2, txtBox7P2, txtBox13P2, txtBox19P2, txtBox25P2},
                new() {txtBox5P2, txtBox9P2, txtBox13P2, txtBox17P2, txtBox21P2}
            };
            lstoptYesNo = new() { optYesP1, optYesP2, optNoP1, optNoP2 };
            lstoptHowBoardsPicked = new() { optIPickBoardP1, optIPickBoardP2, optComputerBoardP1, optComputerBoardP2 };

            defaultbackcolor = txtBox1P1.BackColor;

            btnStart.Click += BtnStart_Click;
            btnBoardsAreReady.Click += BtnBoardsAreReady_Click;
            btnPick.Click += BtnPick_Click;
            optNumbers.Click += OptNumbers_Click;
            optLetters.Click += OptLetters_Click;
            optComputerBoardP1.Click += OptComputerBoardP1_Click;
            optComputerBoardP2.Click += OptComputerBoardP2_Click;
            optIPickBoardP1.Click += OptIPickBoardP1_Click;
            optIPickBoardP2.Click += OptIPickBoardP2_Click;
            DisplayGameStatus();
        }

        private void DisplayGameStatus()
        {
            string msg = "Click Start to begin the Game.";
            string modemsg = gamemode == GameModeEnum.Number ? "Pick from Numbers 1-50." : "Pick any Letter.";

            switch (gamestatus)
            {
                case GameStatusEnum.PickModeBoard:
                    msg = "Choose Game Mode and Board. " + modemsg + " Click 'Boards Are ready' once both boards are filled in.";
                    break;
                case GameStatusEnum.Playing:
                    msg = "Playing";
                    break;
                case GameStatusEnum.Winner:
                    msg = "Winner is: " + Winner;
                    break;
            }
            lblmsg.Text = msg;
        }

        private void DisplayTextbtnPick()
        {
            btnPick.Text = "Pick a " + gamemode.ToString();
        }
        private void SetListReadOnly(List<TextBox> lst, bool TrueOrFalse)
        {
            lst.ForEach(txt => txt.ReadOnly = TrueOrFalse);
        }

        private void SetListChecked(List<RadioButton> lst, bool TrueOrFalse)
        {
            lst.ForEach(opt => opt.Checked = TrueOrFalse);
        }

        private void StartTheGame()
        {
            gamestatus = GameStatusEnum.PickModeBoard;
            DisplayGameStatus();
            EnableAndSetBackColorForBoxes(lsttxtPlayer1);
            SetBingoBox(lsttxtPlayer1);
            EnableAndSetBackColorForBoxes(lsttxtPlayer2);
            SetBingoBox(lsttxtPlayer2);
            SetListChecked(lstoptHowBoardsPicked, false);
            SetListChecked(lstoptYesNo, false);
            lstoptYesNo.ForEach(opt => opt.Enabled = false);
            lblChosenNumOrLetter.Text = "";
        }
        
        private void BoardsAreReady()
        {
            if ((lsttxtPlayer1.Where(txt => txt.Text == "").Count() == 0) && (lsttxtPlayer2.Where(txt => txt.Text == "").Count() == 0) && gamestatus == GameStatusEnum.PickModeBoard)
            {
                gamestatus = GameStatusEnum.Playing;
                DisplayGameStatus();

                SetListReadOnly(lsttxtPlayer1, true);
                SetListReadOnly(lsttxtPlayer2, true);
                lstoptYesNo.ForEach(opt => opt.Enabled = true);
            }
        }

        private void PickNumberLetter()
        {
            if (gamestatus == GameStatusEnum.Playing)
            {
                if (optNumbers.Checked == true)
                {
                    lblChosenNumOrLetter.Text = GetRandomNumber();
                }
                else if (optLetters.Checked == true)
                {
                    lblChosenNumOrLetter.Text = GetRandomLetter();
                }
            }
        }

        private string GetRandomNumber()
        {
            Random rnd = new();
            int n = rnd.Next(0, 51);
            string s = n.ToString();
            return s;
        }

        private string GetRandomLetter()
        {
            Random rnd = new();
            char randomLetter = (char)rnd.Next(65, 91);
            string s = randomLetter.ToString();
            return s;
        }

        private void EnableAndSetBackColorForBoxes(List<TextBox> lst)
        {
            lst.ForEach(txt => { txt.Enabled = true; txt.Text = ""; txt.BackColor = defaultbackcolor; });
        }

        private void SetBingoBox(List<TextBox> lst)
        {
            lst[12].Text = "Bingo";
            lst[12].BackColor = Color.Lime;
        }

        private void CheckBoard()
        {
            if (gamestatus == GameStatusEnum.Playing)
            {
                if (lsttxtPlayer1.Where(txt => txt.Text == lblChosenNumOrLetter.Text).Count() > 0)
                {
                    optYesP1.Checked = true;
                    var ListContainingNumLetterP1 = lsttxtPlayer1.Where(txt => txt.Text == lblChosenNumOrLetter.Text).ToList();
                    ListContainingNumLetterP1.ForEach(txt => txt.BackColor = Color.Lime);
                }
                else if (lsttxtPlayer1.Where(txt => txt.Text == lblChosenNumOrLetter.Text).Count() == 0)
                {
                    optNoP1.Checked = true;
                }

                if (lsttxtPlayer2.Where(txt => txt.Text == lblChosenNumOrLetter.Text).Count() > 0)
                {
                    optYesP2.Checked = true;
                    var ListContainingNumLetterP2 = lsttxtPlayer2.Where(txt => txt.Text == lblChosenNumOrLetter.Text).ToList();
                    ListContainingNumLetterP2.ForEach(txt => txt.BackColor = Color.Lime);
                }
                else if (lsttxtPlayer2.Where(txt => txt.Text == lblChosenNumOrLetter.Text).Count() == 0)
                {
                    optNoP2.Checked = true;
                }
            }
        }

        private void DetectWinner(List<TextBox> lst, string player)
        {
            if (gamestatus == GameStatusEnum.Playing)
            {
                if (lst.Where(txt => txt.BackColor == Color.Lime).Count() == lst.Count())
                {
                    gamestatus = GameStatusEnum.Winner;
                    Winner = player;
                    DisplayGameStatus();
                    lst.ForEach(txt => { txt.BackColor = Color.Turquoise; txt.ForeColor = Color.Purple; });
                }
            }
        }

        private void OptNumbers_Click(object? sender, EventArgs e)
        {
            gamemode = GameModeEnum.Number;
            DisplayTextbtnPick();
            DisplayGameStatus();
        }

        private void OptLetters_Click(object? sender, EventArgs e)
        {
            gamemode = GameModeEnum.Letter;
            DisplayTextbtnPick();
            DisplayGameStatus();
        }

        private bool IsGameStatusPick()
        {
            return gamestatus == GameStatusEnum.PickModeBoard;
        }

        private void OptComputerBoardP2_Click(object? sender, EventArgs e)
        {
            if (IsGameStatusPick())
            {
                if (gamemode == GameModeEnum.Number)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        lsttxtPlayer2[i].Text = GetRandomNumber();
                    }
                }
                else if (gamemode == GameModeEnum.Letter)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        lsttxtPlayer2[i].Text = GetRandomLetter();
                    }
                }
                txtBox13P2.Text = "Bingo!";
                SetListReadOnly(lsttxtPlayer2, true);
            }
        }

        private void OptComputerBoardP1_Click(object? sender, EventArgs e)
        {
            if (IsGameStatusPick())
            {
                if (gamemode == GameModeEnum.Number)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        lsttxtPlayer1[i].Text = GetRandomNumber();
                    }
                }
                else if (gamemode == GameModeEnum.Letter)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        lsttxtPlayer1[i].Text = GetRandomLetter();
                    }
                }
                txtBox13P1.Text = "Bingo!";
                SetListReadOnly(lsttxtPlayer1, true);
            }
        }

        private void OptIPickBoardP2_Click(object? sender, EventArgs e)
        {
            if (IsGameStatusPick())
            {
                lsttxtPlayer2.ForEach(txt => txt.Text = "");
            }
        }

        private void OptIPickBoardP1_Click(object? sender, EventArgs e)
        {
            if (IsGameStatusPick())
            {
                lsttxtPlayer1.ForEach(txt => txt.Text = "");
            }
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            StartTheGame();
        }

        private void BtnBoardsAreReady_Click(object? sender, EventArgs e)
        {
            BoardsAreReady();
        }

        private void BtnPick_Click(object? sender, EventArgs e)
        {
            PickNumberLetter();
            CheckBoard();
            lstwinningsetsP1.ForEach(lst => DetectWinner(lst, "Player 1"));
            lstwinningsetsP2.ForEach(lst => DetectWinner(lst, "Player 2"));
        }
    }
}