using Microsoft.VisualBasic;
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
        enum GameModeEnum { Number, Letter };
        GameModeEnum gamemode = GameModeEnum.Number;

        string Winner = "";
        Color defaultbackcolor;

        List<TextBox> lsttxtPlayer1;
        List<TextBox> lsttxtPlayer2;

        List<List<TextBox>> lstwinningsetsP1;
        List<List<TextBox>> lstwinningsetsP2;

        List<RadioButton> lstoptHowBoardsPicked;
        List<RadioButton> lstoptNumLetter;

        List<List<TextBox>> lstalltxtboxes;
        List<List<RadioButton>> lstallradiobuttons;
        List<Button> lstallbuttons;

        public frmBingo()
        {
            InitializeComponent();

            DisplayTextbtnPick();

            lsttxtPlayer1 = new() { txtBox1P1, txtBox2P1, txtBox3P1, txtBox4P1, txtBox5P1, txtBox6P1, txtBox7P1, txtBox8P1, txtBox9P1, txtBox10P1, txtBox11P1, txtBox12P1, txtBox13P1, txtBox14P1, txtBox15P1, txtBox16P1, txtBox17P1, txtBox18P1, txtBox19P1, txtBox20P1, txtBox21P1, txtBox22P1, txtBox23P1, txtBox24P1, txtBox25P1 };
            lsttxtPlayer2 = new() { txtBox1P2, txtBox2P2, txtBox3P2, txtBox4P2, txtBox5P2, txtBox6P2, txtBox7P2, txtBox8P2, txtBox9P2, txtBox10P2, txtBox11P2, txtBox12P2, txtBox13P2, txtBox14P2, txtBox15P2, txtBox16P2, txtBox17P2, txtBox18P2, txtBox19P2, txtBox20P2, txtBox21P2, txtBox22P2, txtBox23P2, txtBox24P2, txtBox25P2 };

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
            lstoptHowBoardsPicked = new() { optIPickBoardP1, optIPickBoardP2, optComputerBoardP1, optComputerBoardP2 };
            lstoptNumLetter = new() { optNumbers, optLetters };

            lstalltxtboxes = new() { lsttxtPlayer1, lsttxtPlayer2 };
            lstallradiobuttons = new() { lstoptHowBoardsPicked, lstoptNumLetter };
            lstallbuttons = new() { btnStart, btnBoardsAreReady, btnPick };

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
        
        private void EnableDisableButtons(Button btn, bool TrueOrFalse)
        {
            btn.Enabled = TrueOrFalse;
        }

        private void EnableDisableTextBoxes(List<TextBox> lst, bool TrueOrFalse)
        {
            lst.ForEach(txt => txt.Enabled = TrueOrFalse);
        }

        private void EnableDisableRadioButtons(List<RadioButton> lst, bool TrueOrFalse)
        {
            lst.ForEach(opt => opt.Enabled = TrueOrFalse);
        }
        private void SetListReadOnly(List<TextBox> lst, bool TrueOrFalse)
        {
            lst.ForEach(txt => txt.ReadOnly = TrueOrFalse);
        }

        private void DisplayGameStatus()
        {
            string msg = "Click Start to begin the Game.";
            string modemsg = gamemode == GameModeEnum.Number ? "Pick from Numbers 1-50." : "Pick any letter from A-Z.";

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
        
        private void StartTheGame()
        {
            gamestatus = GameStatusEnum.PickModeBoard;
            DisplayGameStatus();

            EnableDisableButtons(btnBoardsAreReady, true);
            EnableDisableButtons(btnPick, false);
            lstalltxtboxes.ForEach(lst => 
            { 
                EnableDisableTextBoxes(lst, true);
                lst.ForEach(txt => SetDefaultBackColorAndBlank(txt));
                SetBingoBox(lst);
            });
            lstallradiobuttons.ForEach(lst => EnableDisableRadioButtons(lst, true));
            optNumbers.Checked = true;
            gamemode = GameModeEnum.Number;
            DisplayTextbtnPick();
            optIPickBoardP1.Checked = true;
            SetListReadOnly(lsttxtPlayer1, false);
            SetBingoBox(lsttxtPlayer1);
            optIPickBoardP2.Checked = true;
            SetListReadOnly(lsttxtPlayer2, false);
            SetBingoBox(lsttxtPlayer2);
            lblChosenNumOrLetter.Text = "";
        }
        
        private void BoardsAreReady()
        {
            if ((lsttxtPlayer1.Where(txt => txt.Text == "").Count() == 0) && (lsttxtPlayer2.Where(txt => txt.Text == "").Count() == 0) && gamestatus == GameStatusEnum.PickModeBoard)
            {
                gamestatus = GameStatusEnum.Playing;
                DisplayGameStatus();
                lstalltxtboxes.ForEach(lst => SetListReadOnly(lst, true));
                EnableDisableButtons(btnPick, true);
                lstallradiobuttons.ForEach(lst => EnableDisableRadioButtons(lst, false));
            }
        }

        private void PickNumberLetter()
        {
            if (IsGameStatus(GameStatusEnum.Playing))
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

        private void SetDefaultBackColorAndBlank(TextBox txt)
        {
            txt.Text = ""; 
            txt.BackColor = defaultbackcolor;
        }

        private void SetBingoBox(List<TextBox> lst)
        {
            lst[12].Text = "Bingo!";
            lst[12].BackColor = Color.Lime;
            lst[12].ReadOnly = true;
        }

        private void CheckBoard()
        {
            if (IsGameStatus(GameStatusEnum.Playing))
            {
                if (lsttxtPlayer1.Where(txt => txt.Text.ToUpper() == lblChosenNumOrLetter.Text).Count() > 0)
                {
                    var ListContainingNumLetterP1 = lsttxtPlayer1.Where(txt => txt.Text.ToUpper() == lblChosenNumOrLetter.Text).ToList();
                    ListContainingNumLetterP1.ForEach(txt => txt.BackColor = Color.Lime);
                }

                if (lsttxtPlayer2.Where(txt => txt.Text.ToUpper() == lblChosenNumOrLetter.Text).Count() > 0)
                {
                    var ListContainingNumLetterP2 = lsttxtPlayer2.Where(txt => txt.Text.ToUpper() == lblChosenNumOrLetter.Text).ToList();
                    ListContainingNumLetterP2.ForEach(txt => txt.BackColor = Color.Lime);
                }
            }
        }

        private void DetectWinner(List<TextBox> lst, string player)
        {
            if (IsGameStatus(GameStatusEnum.Playing))
            {
                if (lst.Where(txt => txt.BackColor == Color.Lime).Count() == lst.Count())
                {
                    gamestatus = GameStatusEnum.Winner;
                    Winner = player;
                    DisplayGameStatus();
                    lst.ForEach(txt => txt.BackColor = Color.Turquoise);
                    EnableDisableButtons(btnBoardsAreReady, false);
                    EnableDisableButtons(btnPick, false);
                    lstallradiobuttons.ForEach(lst => EnableDisableRadioButtons(lst, false));
                }
            }
        }
//AS Move code out of all event handlers into procedures and call it from event handlers.
//AS The following two event handlers should be moved into 1 procedure and pass in a param to know if you are referring to p1 or p2, that way the code is only written once.

        private void ClickNumberLetter(GameModeEnum gamemodeclicked)
        {
            if (IsGameStatus(GameStatusEnum.PickModeBoard))
            {
                gamemode = gamemodeclicked;
                DisplayTextbtnPick();
                DisplayGameStatus();
            }
        }

        private void OptNumbers_Click(object? sender, EventArgs e)
        {
            ClickNumberLetter(GameModeEnum.Number);
        }
        
        private void OptLetters_Click(object? sender, EventArgs e)
        {
            ClickNumberLetter(GameModeEnum.Letter);
        }
        
        private bool IsGameStatus(GameStatusEnum Gstatus)
        {
            return gamestatus == Gstatus;
        }

//AS Same for the following 2 procedures.
        private void OptComputerBoardClicked(List<TextBox> lsttxt)
        {
            if (IsGameStatus(GameStatusEnum.PickModeBoard))
            {
                if (gamemode == GameModeEnum.Number)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        lsttxt[i].Text = GetRandomNumber();
                    }
                }
                else if (gamemode == GameModeEnum.Letter)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        lsttxt[i].Text = GetRandomLetter();
                    }
                }
                lsttxt[12].Text = "Bingo!";
                SetListReadOnly(lsttxt, true);
            }
        }

        private void OptComputerBoardP2_Click(object? sender, EventArgs e)
        {
            OptComputerBoardClicked(lsttxtPlayer2);
//AS It already said bingo from the beginning no?
//CMH It does say it in the beginning but when you press this button it clears out all the boxes so then it has to say Bingo again.
        }

        private void OptComputerBoardP1_Click(object? sender, EventArgs e)
        {
            OptComputerBoardClicked(lsttxtPlayer1);
        }
        
//AS Same for the following 2 procedures.
        private void OptIPickBoardClicked(List<TextBox> lsttxt)
        {
            if (IsGameStatus(GameStatusEnum.PickModeBoard))
            {
                lsttxt.ForEach(txt => txt.Text = "");
                SetListReadOnly(lsttxt, false);
                SetBingoBox(lsttxt);
            }
        }

        private void OptIPickBoardP2_Click(object? sender, EventArgs e)
        {
            OptIPickBoardClicked(lsttxtPlayer2);
        }

        private void OptIPickBoardP1_Click(object? sender, EventArgs e)
        {
            OptIPickBoardClicked(lsttxtPlayer1);
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